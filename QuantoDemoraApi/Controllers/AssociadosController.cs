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
    public class AssociadosController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger("Associados Controller");
        private readonly IAssociadosRepository _associadosRepository;
        public AssociadosController(IAssociadosRepository associadosRepository)
        {
            _associadosRepository = associadosRepository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Associado>>> GetAsync()
        {
            try
            {
                var lista = await _associadosRepository.GetAllAsync();
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
        [HttpGet("{associadoId}")]
        public async Task<IActionResult> GetIdAsync(int associadoId)
        {
            try
            {
                Associado associado = await _associadosRepository.GetByIdAsync(associadoId);
                return Ok(associado);
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