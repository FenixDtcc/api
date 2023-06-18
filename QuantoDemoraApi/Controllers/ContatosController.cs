using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;

namespace QuantoDemoraApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class ContatosController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger("Contatos Controller");
        private readonly IContatosRepository _contatosRepository;
        public ContatosController(IContatosRepository contatosRepository)
        {
            _contatosRepository = contatosRepository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Contato>>> GetAsync()
        {
            try
            {
                var lista = await _contatosRepository.GetAllAsync();
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
        [HttpGet("{hospitalId}")]
        public async Task<IActionResult> GetIdAsync(int hospitalId)
        {
            try
            {
                var lista = await _contatosRepository.GetByIdAsync(hospitalId);
                return Ok(lista);
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (InvalidOperationException ex)
            {
                _logger.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return NotFound(ex.Message);
            }
        }
    }
}