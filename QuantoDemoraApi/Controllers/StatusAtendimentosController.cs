using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;

namespace QuantoDemoraApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class StatusAtendimentosController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger("StatusAtendimentos Controller");
        private readonly IStatusAtendimentosRepository _statusAtendimentosRepository;
        public StatusAtendimentosController(IStatusAtendimentosRepository statusAtendimentosRepository)
        {
            _statusAtendimentosRepository = statusAtendimentosRepository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<StatusAtendimento>>> GetAsync()
        {
            try
            {
                var lista = await _statusAtendimentosRepository.GetAllAsync();  
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
        [HttpGet("{statusAtendimentoId}")]
        public async Task<IActionResult> GetIdAsync(int statusAtendimentoId)
        {
            try
            {
                StatusAtendimento statusAtendimento = await _statusAtendimentosRepository.GetByIdAsync(statusAtendimentoId);    
                return Ok(statusAtendimento);
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