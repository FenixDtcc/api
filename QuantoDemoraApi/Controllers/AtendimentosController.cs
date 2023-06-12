using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository;
using QuantoDemoraApi.Repository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantoDemoraApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AtendimentosController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger("Atendimentos Controller");
        private readonly IAtendimentosRepository _atendimentosRepository;
        private readonly IEspecialidadesRepository _especialidadesRepository;

        public AtendimentosController(IAtendimentosRepository atendimentosRepository, IEspecialidadesRepository especialidadesRepository)
        {
            _atendimentosRepository = atendimentosRepository;
            _especialidadesRepository = especialidadesRepository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Atendimento>>> GetAsync()
        {
            try
            {
                var lista = await _atendimentosRepository.GetAllAsync();
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
        public async Task<IActionResult> GetAtendimentoPorEspecialidadeHospitalIdAsync(int hospitalId)
        {
            try
            {
                var listaAtendimento = await _atendimentosRepository.GetAtendimentoPorEspecialidadeByHospitalIdAsync(hospitalId);
                var listaEspecialidade = (List<Especialidade>)await _especialidadesRepository.GetAllAsync();

                return Ok(listaEspecialidade);
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