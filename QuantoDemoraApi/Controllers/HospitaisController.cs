using Microsoft.AspNetCore.Mvc;
using QuantoDemoraApi.Models;
using log4net;
using QuantoDemoraApi.Repository.Interfaces;

namespace QuantoDemoraApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class HospitaisController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger("Hospitais Controller");
        private readonly IHospitaisRepository _hospitaisRepository;
        public HospitaisController(IHospitaisRepository hospitaisRepository)
        {
            _hospitaisRepository = hospitaisRepository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetAsync()
        {
            try
            {
                var lista = await _hospitaisRepository.GetAllAsync();
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
                Hospital hospital = await _hospitaisRepository.GetByIdAsync(hospitalId);    
                if (hospital == null)
                {
                    return NotFound();
                }
                return Ok(hospital);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}