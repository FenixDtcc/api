using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Utils;
using Microsoft.AspNetCore.Authorization;
using QuantoDemoraApi.Repository.Interfaces;
using log4net;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace QuantoDemoraApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class UsuariosController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger("Usuarios Controller");

        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUsuariosRepository _usuariosRepository;

        public UsuariosController(DataContext context, IHttpContextAccessor httpContextAccessor, IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
            _context = context;
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
                if (usuario == null)
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
                if (usuario == null)
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
                return Created("Cadastro Admin", ua.IdUsuario);
            }
            catch (Microsoft.AspNetCore.Http.BadHttpRequestException ex)
            {
                _logger.Error(ex);
                return BadRequest();
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
        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar(Usuario u)
        {
            try
            {
                await _usuariosRepository.CadastrarAsync(u);
                return Created("Cadastro Usuario", u.IdUsuario);
            }
            catch (Microsoft.AspNetCore.Http.BadHttpRequestException ex)
            {
                _logger.Error(ex);
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("Autenticar")]
        public async Task<IActionResult> Autenticar(Usuario creds)
        {
            try
            {
                await _usuariosRepository.AutenticarAsync(creds);
                return Ok(creds);
            }
            catch(Microsoft.AspNetCore.Http.BadHttpRequestException ex)
            {
                _logger.Error(ex);
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("AlterarEmail")]
        public async Task<IActionResult> AlterarEmail(Usuario u)
        {
            try
            {
                Usuario usuario = await _usuariosRepository.AlterarEmailAsync(u);
                if (usuario == null)
                {
                    return BadRequest();
                }
                return Ok(usuario.IdUsuario);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [AllowAnonymous]
        [HttpPut("AlterarSenha")]
        public async Task<IActionResult> AlterarSenha(Usuario creds)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(creds.Email.ToLower()));

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
                    int linhasAfetadas = await _context.SaveChangesAsync();
                    return Ok(linhasAfetadas);
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

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{usuarioId}")]
        public async Task<IActionResult> Deletar(int usuarioId)
        {
            try
            {
                Usuario usuario = await _usuariosRepository.DeletarAsync(usuarioId);
                if (usuario == null)
                {
                    return BadRequest();
                }
                // return Ok(usuario.IdUsuario);
                // OU
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}