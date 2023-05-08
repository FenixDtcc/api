using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Utils;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using QuantoDemoraApi.Repository.Interfaces;
using log4net;

namespace QuantoDemoraApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class UsuariosController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger("Usuarios Controller");

        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUsuariosRepository _usuariosRepository;

        public UsuariosController(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAsync()
        {
            try
            {
                var lista = await _usuariosRepository.GetAllAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> GetIdAsync(int usuarioId)
        {
            try
            {
                Usuario usuario = await _usuariosRepository.GetByIdAsync(usuarioId);
                if(usuario == null)
                {
                    return NotFound();
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("NomeUsuario/{nomeUsuario}")]
        public async Task<IActionResult> GetUserNameAsync(string nomeUsuario)
        {
            try
            {
                Usuario usuario = await _usuariosRepository.GetByNameAsync(nomeUsuario);
                if(usuario == null)
                {
                    return NotFound();
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("CadastrarAdmin")]
        public async Task<IActionResult> CadastrarAdmin(Usuario ua)
        {
            try
            {
                await _usuariosRepository.CadastrarAdminAsync(ua);

                if(ua == null)
                {
                    return BadRequest();
                }
                return Created("Cadastro Admin", ua.IdUsuario);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
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

                // Validar
                if (associado != null && await _usuariosRepository.VerificarNomeUsuarioExistente(u.NomeUsuario) == true)
                    throw new Exception("O nome de usuário escolhido já está em uso");

                // Validar
                if (associado != null && await _usuariosRepository.VerificarEmailExistente(u.Email) == true)
                    throw new Exception("O e-mail informado já está em uso");

                Criptografia.CriarPasswordHash(u.PasswordString, out byte[] hash, out byte[] salt);
                u.PasswordString = string.Empty;
                u.PasswordHash = hash;
                u.PasswordSalt = salt;
                u.DtCadastro = LocalDateTime.HorarioBrasilia();
                u.TpUsuario = "Comum";
                u.IdAssociado = associado.IdAssociado;


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

                // Validar
                if (await _usuariosRepository.VerificarEmailExistente(u.Email) == true)
                    throw new Exception("O e-mail informado já está em uso");

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
        public async Task<IActionResult> Deletar(int usuarioId)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.IdUsuario == usuarioId);

                _context.Usuarios.Remove(usuario);
                
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string CriarToken(Usuario u)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, u.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, u.NomeUsuario),
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