using log4net;
using Microsoft.AspNetCore.Mvc;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;

namespace QuantoDemoraApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class LogradourosController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger("Logradouros Controller");
        private readonly ILogradourosRepository _logradourosRepository;
        public LogradourosController(ILogradourosRepository logradourosRepository)
        {
            _logradourosRepository = logradourosRepository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Logradouro>>> GetAsync()
        {
            try
            {
                var lista = await _logradourosRepository.GetAllAsync();
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
        [HttpGet("{logradouroId}")]
        public async Task<IActionResult> GetIdAsync(int logradouroId)
        {
            try
            {
                Logradouro logradouro = await _logradourosRepository.GetByIdAsync(logradouroId);    
                if (logradouro == null)
                {
                    return NotFound();
                }
                return Ok(logradouro);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}