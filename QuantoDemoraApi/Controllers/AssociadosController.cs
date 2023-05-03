using System.Security.Claims;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;

namespace QuantoDemoraApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AssociadosController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger("Associados Controller");

        private readonly DataContext _context;
        private readonly IAssociadosRepository _associadosRepository;
        public AssociadosController(DataContext context, IAssociadosRepository associadosRepository)
        {
            _associadosRepository = associadosRepository;
            _context = context;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Associado>>> Get()
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
                if (associado is null)
                {
                    return NotFound();
                }
                return Ok(associado);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}