using log4net;
using Microsoft.AspNetCore.Mvc;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;

namespace QuantoDemoraApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class EspecialidadesController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger("Especialidades Controller");
        private readonly IEspecialidadesRepository _especialidadesRepository;
        public EspecialidadesController(IEspecialidadesRepository especialidadesRepository)
        {
            _especialidadesRepository = especialidadesRepository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("Listar")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var lista = await _especialidadesRepository.GetAllAsync();
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
        [HttpGet("{especialidadeId}")]
        public async Task<IActionResult> GetIdAsync(int especialidadeId)
        {
            try
            {
                Especialidade especialidade = await _especialidadesRepository.GetByIdAsync(especialidadeId);
                if (especialidade == null)
                {
                    return NotFound();
                }
                return Ok(especialidade);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}