using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;
using QuantoDemoraApi.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuantoDemoraApi.Repository
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private static readonly ILog _logger = LogManager.GetLogger("Usuarios Repository");
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public UsuariosRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            try
            {
                List<Usuario> lista = await _context.Usuarios.ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<Usuario> GetByIdAsync(int usuarioId)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.IdUsuario == usuarioId);
                return usuario;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<Usuario> GetByNameAsync(string nomeUsuario)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.NomeUsuario.ToLower() == nomeUsuario.ToLower());
                return usuario;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<Usuario> CadastrarAdminAsync(Usuario ua)
        {
            try
            {
                // Retorna 500, porque?
                if (await VerificarNomeUsuarioExistente(ua.NomeUsuario) == true)
                    throw new Exception("O nome de usuário escolhido já está em uso");

                Criptografia.CriarPasswordHash(ua.PasswordString, out byte[] hash, out byte[] salt);
                ua.PasswordString = string.Empty;
                ua.PasswordHash = hash;
                ua.PasswordSalt = salt;
                ua.Email = "quantodemora@gmail.com";
                ua.Cpf = "000.000.001-91";
                ua.DtCadastro = LocalDateTime.HorarioBrasilia();
                ua.TpUsuario = "Admin";

                await _context.Usuarios.AddAsync(ua);
                await _context.SaveChangesAsync();
                return ua;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<Usuario> CadastrarAsync(Usuario u)
        {
            Associado associado = new Associado();

            try
            {
                // TODAS AS VALIDAÇÕES RETORNAM 500, PORQUE?
                associado = await _context.Associados.FirstOrDefaultAsync(x => x.Cpf.Replace(".", "").Replace("-", "")
                                                                    .Equals(u.Cpf.Replace(".", "").Replace("-", "")));
                if (associado is null)
                    throw new Exception("O CPF informado não consta na Base de Dados do Plano de Saúde!");

                bool usuarioCadastrado = await _context.Usuarios.AnyAsync(x => x.Cpf.Replace(".", "").Replace("-", "")
                                                                    .Equals(u.Cpf.Replace(".", "").Replace("-", "")));

                if (associado != null && usuarioCadastrado)
                    throw new Exception("O CPF informado já está cadastrado como usuário");

                // Retorna 500, porque?
                /*if (associado != null && await VerificarEmailExistente(u.Email) == true)
                    throw new Exception("O e-mail informado já está em uso");*/

                Criptografia.CriarPasswordHash(u.PasswordString, out byte[] hash, out byte[] salt);
                u.PasswordString = string.Empty;
                u.PasswordHash = hash;
                u.PasswordSalt = salt;
                u.DtCadastro = LocalDateTime.HorarioBrasilia();
                u.TpUsuario = "Comum";
                u.IdAssociado = associado.IdAssociado;

                await _context.Usuarios.AddAsync(u);
                await _context.SaveChangesAsync();
                return u;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<Usuario> AutenticarAsync(Usuario creds)
        {
            try
            {
                /*List<Usuario> listaUsuarios = await _context.Usuarios.ToListAsync();

                if(listaUsuarios.Exists(x => x.TpUsuario.Contains("Admin") == creds.TpUsuario.Contains("Admin")))
                {
                    Usuario usuarioAdmin = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.NomeUsuario.ToLower().Equals(creds.NomeUsuario.ToLower()));
                }*/

                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.NomeUsuario.ToLower().Equals(creds.NomeUsuario.ToLower()));

                // Porque nas validações está retornando sempre código 500 ao invés da mensagem de erro do throw?
                if (usuario == null)
                {
                    throw new Exception("Usuário não encontrado!");
                }
                else if (!Criptografia.VerificarPasswordHash(creds.PasswordString, usuario.PasswordHash, usuario.PasswordSalt))
                {
                    throw new Exception("Senha incorreta!");
                }
                else
                {
                    usuario.DtAcesso = LocalDateTime.HorarioBrasilia();
                    _context.Usuarios.Update(usuario);
                    await _context.SaveChangesAsync();

                    usuario.PasswordHash = null;
                    usuario.PasswordSalt = null;
                    usuario.Token = CriarToken(usuario);
                    return usuario;
                }
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<Usuario> AlterarEmailAsync(Usuario u)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.IdUsuario == u.IdUsuario);

                // Retorna 500, porque?
                if (await VerificarEmailExistente(u.Email) == true)
                    throw new Exception("O e-mail informado já está em uso");

                usuario.Email = u.Email;

                var attach = _context.Attach(usuario);
                attach.Property(x => x.IdUsuario).IsModified = false;
                attach.Property(x => x.Email).IsModified = true;

                int linhasAfetadas = await _context.SaveChangesAsync();
                return usuario;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<Usuario> AlterarSenhaAsync(Usuario creds)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(creds.Email.ToLower()));

                // Retorna 500, porque?
                if (usuario == null)
                {
                    throw new Exception("Usuário não encontrado");
                }
                else
                {
                    Criptografia.CriarPasswordHash(creds.PasswordString, out byte[] hash, out byte[] salt);
                    usuario.PasswordHash = hash;
                    usuario.PasswordSalt = salt;

                    _context.Usuarios.Update(usuario);
                    await _context.SaveChangesAsync();
                    return usuario;
                }
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<Usuario> AtualizarLocalizacaoAsync(Usuario u)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.IdUsuario == u.IdUsuario);

                usuario.Latitude = u.Latitude;
                usuario.Longitude = u.Longitude;

                var attach = _context.Attach(usuario);
                attach.Property(x => x.IdUsuario).IsModified = false;
                attach.Property(x => x.Latitude).IsModified = true;
                attach.Property(x => x.Longitude).IsModified = true;

                int linhasAfetadas = await _context.SaveChangesAsync();
                return usuario;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<Usuario> DeletarAsync(int usuarioId)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.IdUsuario == usuarioId);

                _context.Usuarios.Remove(usuario);

                await _context.SaveChangesAsync();

                return usuario;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        // Verificar porque não está dando certo a validação abaixo nos métodos
        private async Task<bool> VerificarNomeUsuarioExistente(string nomeUsuario)
        {
            if (await _context.Usuarios.AnyAsync(x => x.NomeUsuario.ToLower() == nomeUsuario.ToLower()))
            {
                return true;
            }
            return false;
        }

        // Verificar porque não está dando certo a validação abaixo nos métodos
        private async Task<bool> VerificarEmailExistente(string emailUsuario)
        {
            if (await _context.Usuarios.AnyAsync(x => x.Email.ToLower() == emailUsuario.ToLower()))
            {
                return true;
            }
            return false;
        }

        private string CriarToken(Usuario u)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, u.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, u.Email),
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("ConfiguracaoToken:Chave").Value));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(600),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}