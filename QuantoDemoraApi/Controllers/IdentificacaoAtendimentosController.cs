using log4net;
using Microsoft.AspNetCore.Mvc;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;

namespace QuantoDemoraApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class IdentificacaoAtendimentosController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger("IdentificacaoAtendimentos Controller");
        private readonly IIdentificacaoAtendimentosRepository _identificacaoAtendimentosRepository;
        public IdentificacaoAtendimentosController(IIdentificacaoAtendimentosRepository identificacaoAtendimentosRepository)
        {
            _identificacaoAtendimentosRepository = identificacaoAtendimentosRepository; 
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("Listar")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var lista = await _identificacaoAtendimentosRepository.GetAllAsync();
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
        [HttpGet("{identificacaoAtendimentoId}")]
        public async Task<IActionResult> GetIdAsync(int identificacaoAtendimentoId)
        {
            try
            {
                IdentificacaoAtendimento identificacaoAtendimento = await _identificacaoAtendimentosRepository.GetByIdAsync(identificacaoAtendimentoId);
                if (identificacaoAtendimento == null)
                {
                    return NotFound();
                }
                return Ok(identificacaoAtendimento);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

