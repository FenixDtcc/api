using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Utils;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace QuantoDemoraApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsuariosController(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        private string CriarToken(Usuario u)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, u.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, u.NomeUsuario)
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("ConfiguracaoToken:Chave").Value));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // AGUARDANDO RETORNO DO PROFESSOR LUIZ
        [HttpGet("GetByAssociado")]
        public async Task<IActionResult> GetByAssociadoAsync()
        {
            try
            {
                string cpf = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

                List<Associado> lista = await _context.Associados
                    .Where(a => a.Cpf.Equals(cpf)).ToListAsync();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // AGUARDANDO RETORNO DO PROFESSOR LUIZ
        private int ObterAssociadoId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Usuario> lista = await _context.Usuarios.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> GetId(int usuarioId)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.IdUsuario == usuarioId);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("NomeUsuario/{nomeUsuario}")]
        public async Task<IActionResult> GetUserName(string nomeUsuario)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.NomeUsuario.ToLower() == nomeUsuario.ToLower());
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("CadastrarAdmin")]
        public async Task<IActionResult> CadastrarAdmin(Usuario ua)
        {
            try
            {
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

                return Ok(ua.IdUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar(Usuario u)
        {
            Associado associado = new Associado();

            try
            {
                associado = await _context.Associados.FirstOrDefaultAsync(x => x.Cpf.Replace(".", "").Replace("-", "")
                                                                    .Equals(u.Cpf.Replace(".", "").Replace("-", "")));
                if (associado is null)
                    throw new Exception("O CPF informado não consta na Base de Dados do Plano de Saúde!");

                bool usuarioCadastrado = await _context.Usuarios.AnyAsync(x => x.Cpf.Replace(".", "").Replace("-", "")
                                                                    .Equals(u.Cpf.Replace(".", "").Replace("-", "")));

                if (associado != null && usuarioCadastrado)
                    throw new Exception("O CPF informado já está cadastrado como usuário");

                Criptografia.CriarPasswordHash(u.PasswordString, out byte[] hash, out byte[] salt);
                u.PasswordString = string.Empty;
                u.PasswordHash = hash;
                u.PasswordSalt = salt;
                u.DtCadastro = LocalDateTime.HorarioBrasilia();
                u.TpUsuario = "Comum";

                // AGUARDANDO RETORNO DO PROFESSOR LUIZ
                u.Associado = _context.Associados.FirstOrDefault(a => a.IdAssociado == ObterAssociadoId());

                await _context.Usuarios.AddAsync(u);
                await _context.SaveChangesAsync();
                return Ok(u.IdUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + " - Detalhes do Erro: " + ex.InnerException);
            }
        }

        [AllowAnonymous]
        [HttpPost("Autenticar")]
        public async Task<IActionResult> Autenticar(Usuario creds)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.NomeUsuario.ToLower().Equals(creds.NomeUsuario.ToLower()));

                if (usuario is null)
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
                    return Ok(usuario);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // O PROFESSOR LUIZ AINDA VAI ENSINAR A UTILIZAÇÃO DESSE RECURSO
        [HttpPut("AtualizarLocalizacao")]
        public async Task<IActionResult> AtualizarLocalizacao(Usuario u)
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
                return Ok(linhasAfetadas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("AlterarEmail")]
        public async Task<IActionResult> AlterarEmail(Usuario u)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.IdUsuario == u.IdUsuario);

                usuario.Email = u.Email;

                var attach = _context.Attach(usuario);
                attach.Property(x => x.IdUsuario).IsModified = false;
                attach.Property(x => x.Email).IsModified = true;

                int linhasAfetadas = await _context.SaveChangesAsync();
                return Ok(linhasAfetadas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("AlterarSenha")]
        public async Task<IActionResult> AlterarSenha(Usuario creds)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.Cpf.ToLower().Equals(creds.Cpf.ToLower()));

                if (usuario is null)
                {
                    throw new System.Exception("Usuário não encontrado");
                }
                else
                {
                    Criptografia.CriarPasswordHash(creds.PasswordString, out byte[] hash, out byte[] salt);
                    usuario.PasswordHash = hash;
                    usuario.PasswordSalt = salt;

                    _context.Usuarios.Update(usuario);
                    int linhasAfetadas = await _context.SaveChangesAsync();
                    return Ok(linhasAfetadas);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{usuarioId}")]
        public async Task<IActionResult> Delete(int usuarioId)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.IdUsuario == usuarioId);

                _context.Usuarios.Remove(usuario);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(linhasAfetadas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}