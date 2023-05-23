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

                if (usuario == null)
                    throw new Exception("Usuário não encontrado, favor conferir o id informado.");

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

                if (usuario == null)
                    throw new Exception("Usuário não encontrado, favor conferir o nome de usuário informado.");

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
                if (await VerificarNomeUsuarioExistente(ua.NomeUsuario) is true)
                    throw new Exception("O nome de usuário escolhido já está em uso.");

                if (ua.PasswordString.Length < 6)
                    throw new Exception("A senha deve conter no mínimo 6 caracteres.");

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
                associado = await _context.Associados.FirstOrDefaultAsync(x => x.Cpf.Replace(".", "").Replace("-", "")
                                                                    .Equals(u.Cpf.Replace(".", "").Replace("-", "")));

                if (associado == null)
                    throw new Exception("O CPF informado não consta na Base de Dados do Plano de Saúde.");

                bool usuarioCadastrado = await _context.Usuarios.AnyAsync(x => x.Cpf.Replace(".", "").Replace("-", "")
                                                                    .Equals(u.Cpf.Replace(".", "").Replace("-", "")));

                if (associado != null && usuarioCadastrado)
                    throw new Exception("O CPF informado já está cadastrado como usuário.");

                if (associado != null && await VerificarNomeUsuarioExistente(u.NomeUsuario) is true)
                    throw new Exception("O nome de usuário informado já está em uso.");

                if (associado != null && await VerificarEmailExistente(u.Email) is true)
                    throw new Exception("O e-mail informado já está em uso.");

                if (associado != null && u.PasswordString.Length < 6)
                    throw new Exception("A senha deve conter no mínimo 6 caracteres.");

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
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.NomeUsuario.ToLower().Equals(creds.NomeUsuario.ToLower()));

                if (usuario == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }
                else if (!Criptografia.VerificarPasswordHash(creds.PasswordString, usuario.PasswordHash, usuario.PasswordSalt))
                {
                    throw new Exception("Senha incorreta.");
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

        /* TENTATIVA DE CRIAR UM ÚNICO MÉTODO PARA TODAS AS ALTERAÇÕES, NÃO FUNCIONOU BEM AS VALIDAÇÕES.
        public async Task<int> AlterarCadastroAsync(Usuario u)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.IdUsuario == u.IdUsuario);

                if (usuario == null)
                    throw new Exception("Usuário não encontrado.");

                if (u.NomeUsuario == usuario.NomeUsuario)
                    throw new Exception("Escolha um nome de usuário diferente do atual.");

                if (await VerificarNomeUsuarioExistente(u.NomeUsuario) is true)
                    throw new Exception("O nome de usuário informado já está em uso.");

                usuario.NomeUsuario = u.NomeUsuario;

                if (u.Email == usuario.Email)
                    throw new Exception("Escolha um e-mail diferente do atual.");

                if (await VerificarEmailExistente(u.Email) is true)
                    throw new Exception("O e-mail informado já está em uso.");

                usuario.Email = u.Email;

                var attach = _context.Attach(usuario);
                attach.Property(x => x.IdUsuario).IsModified = false;
                attach.Property(x => x.NomeUsuario).IsModified = true;
                attach.Property(x => x.Email).IsModified = true;

                if (u.PasswordString.Length < 6)
                {
                    throw new Exception("A senha deve conter no mínimo 6 caracteres.");
                }
                else
                {
                    Criptografia.CriarPasswordHash(u.PasswordString, out byte[] hash, out byte[] salt);
                    usuario.PasswordHash = hash;
                    usuario.PasswordSalt = salt;

                    _context.Usuarios.Update(usuario);
                }

                int linhasAfetadas = await _context.SaveChangesAsync();
                return linhasAfetadas;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }*/

        public async Task<int> AlterarEmailAsync(Usuario u)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.IdUsuario == u.IdUsuario);

                if (usuario == null)
                    throw new Exception("Usuário não encontrado.");

                if (u.Email == usuario.Email)
                    throw new Exception("Escolha um e-mail diferente do atual.");

                if (await VerificarEmailExistente(u.Email) is true)
                    throw new Exception("O e-mail informado já está em uso.");

                usuario.Email = u.Email;

                var attach = _context.Attach(usuario);
                attach.Property(x => x.IdUsuario).IsModified = false;
                attach.Property(x => x.Email).IsModified = true;

                // _context.Usuarios.Update(usuario); Sem Attach

                int linhasAfetadas = await _context.SaveChangesAsync();
                return linhasAfetadas;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<int> AlterarNomeAsync(Usuario u)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.IdUsuario == u.IdUsuario);

                if (usuario == null)
                    throw new Exception("Usuário não encontrado.");

                if (u.NomeUsuario == usuario.NomeUsuario)
                    throw new Exception("Escolha um nome de usuário diferente do atual.");

                if (await VerificarNomeUsuarioExistente(u.NomeUsuario) is true)
                    throw new Exception("O nome de usuário informado já está em uso.");

                usuario.NomeUsuario = u.NomeUsuario;

                var attach = _context.Attach(usuario);
                attach.Property(x => x.IdUsuario).IsModified = false;
                attach.Property(x => x.NomeUsuario).IsModified = true;

                // _context.Usuarios.Update(usuario); Sem Attach

                int linhasAfetadas = await _context.SaveChangesAsync();
                return linhasAfetadas;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<int> AlterarSenhaAsync(Usuario u)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.IdUsuario == u.IdUsuario);

                if (usuario == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }
                else if (u.PasswordString.Length < 6)
                {
                    throw new Exception("A senha deve conter no mínimo 6 caracteres.");
                }
                else
                {
                    Criptografia.CriarPasswordHash(u.PasswordString, out byte[] hash, out byte[] salt);
                    usuario.PasswordHash = hash;
                    usuario.PasswordSalt = salt;

                    _context.Usuarios.Update(usuario);
                    int linhasAfetadas = await _context.SaveChangesAsync();
                    return linhasAfetadas;
                }
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        // O PROFESSOR LUIZ AINDA VAI ENSINAR A UTILIZAÇÃO DESSE RECURSO
        public async Task<int> AtualizarLocalizacaoAsync(Usuario u)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.IdUsuario == u.IdUsuario);

                if (usuario == null)
                    throw new Exception("Usuário não encontrado.");

                usuario.Latitude = u.Latitude;
                usuario.Longitude = u.Longitude;

                var attach = _context.Attach(usuario);
                attach.Property(x => x.IdUsuario).IsModified = false;
                attach.Property(x => x.Latitude).IsModified = true;
                attach.Property(x => x.Longitude).IsModified = true;

                int linhasAfetadas = await _context.SaveChangesAsync();
                return linhasAfetadas;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<int> DeletarAsync(int usuarioId)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.IdUsuario == usuarioId);

                if (usuario == null)
                    throw new Exception("Usuário não encontrado.");

                _context.Usuarios.Remove(usuario);
                int linhasAfetadas = await _context.SaveChangesAsync();
                return linhasAfetadas;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        private async Task<bool> VerificarNomeUsuarioExistente(string nomeUsuario)
        {
            if (await _context.Usuarios.AnyAsync(x => x.NomeUsuario.ToLower() == nomeUsuario.ToLower()))
            {
                return true;
            }
            return false;
        }

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