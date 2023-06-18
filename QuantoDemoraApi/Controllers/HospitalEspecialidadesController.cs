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
    public class HospitalEspecialidadesController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger("HospitalEspecialidades Controller");
        private readonly IHospitalEspecialidadesRepository _hospitalEspecialidades;
        public HospitalEspecialidadesController(IHospitalEspecialidadesRepository hospitalEspecialidades)
        {
            _hospitalEspecialidades = hospitalEspecialidades;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetAsync()
        {
            try
            {
                var lista = await _hospitalEspecialidades.GetAllAsync();
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
                Hospital hospital = await _hospitalEspecialidades.GetByIdAsync(hospitalId);
                return Ok(hospital);
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