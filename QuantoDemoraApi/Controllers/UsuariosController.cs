using Microsoft.AspNetCore.Mvc;
using QuantoDemoraApi.Models;
using Microsoft.AspNetCore.Authorization;
using QuantoDemoraApi.Repository.Interfaces;
using log4net;

namespace QuantoDemoraApi.Controllers
{
    // [Authorize] - Desabilitei para poder testar as requisições
    [ApiController]
    [Route("[Controller]")]
    public class UsuariosController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger("Usuarios Controller");

        private readonly IUsuariosRepository _usuariosRepository;

        public UsuariosController(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
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
                Usuario usuario = await _usuariosRepository.CadastrarAdminAsync(ua);
                return Created("Cadastro Admin", ua.IdUsuario);
            }
            catch (BadHttpRequestException ex)
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
                Usuario usuario = await _usuariosRepository.CadastrarAsync(u);
                return Created("Cadastro Usuario", u.IdUsuario);
            }
            catch (BadHttpRequestException ex)
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
                Usuario usuario = await _usuariosRepository.AutenticarAsync(creds);
                return Ok(usuario);
            }
            catch (BadHttpRequestException ex)
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("AlterarEmail")]
        public async Task<IActionResult> AlterarEmail(Usuario u)
        {
            try
            {
                Usuario usuario = await _usuariosRepository.AlterarEmailAsync(u);
                return Ok(u.IdUsuario);
            }
            catch (BadHttpRequestException ex)
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("AlterarSenha")]
        public async Task<IActionResult> AlterarSenha(Usuario creds)
        {
            try
            {
                Usuario usuario = await _usuariosRepository.AlterarSenhaAsync(creds);
                return Ok(usuario.IdUsuario);
            }
            catch (BadHttpRequestException ex)
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

        // O PROFESSOR LUIZ AINDA VAI ENSINAR A UTILIZAÇÃO DESSE RECURSO
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("AtualizarLocalizacao")]
        public async Task<IActionResult> AtualizarLocalizacao(Usuario u)
        {
            try
            {
                Usuario usuario = await _usuariosRepository.AtualizarLocalizacaoAsync(u);
                return Ok(u.IdUsuario);
            }
            catch (BadHttpRequestException ex)
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

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{usuarioId}")]
        public async Task<IActionResult> Deletar(int usuarioId)
        {
            try
            {
                Usuario usuario = await _usuariosRepository.DeletarAsync(usuarioId);
                // return Ok(usuario.IdUsuario);
                // OU
                return NoContent();
            }
            catch (BadHttpRequestException ex)
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
    }
}