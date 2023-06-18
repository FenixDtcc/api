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
    public class TiposContatoController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger("TiposContato Controller");
        private readonly ITiposContatoRepository _tiposContatoRepository;
        public TiposContatoController(ITiposContatoRepository tiposContatoRepository)
        {
            _tiposContatoRepository = tiposContatoRepository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("Listar")]
        public async Task<ActionResult<TipoContato>> GetAsync()
        {
            try
            {
                var lista = await _tiposContatoRepository.GetAllAsync();
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
        [HttpGet("{tipoContatoId}")]
        public async Task<IActionResult> GetIdAsync(int tipoContatoId)
        {
            try
            {
                TipoContato tipoContato = await _tiposContatoRepository.GetByIdAsync(tipoContatoId);
                return Ok(tipoContato);
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