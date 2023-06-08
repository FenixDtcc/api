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
        public async Task<IActionResult> GetIdAsync(int hospitalId)
        {
            try
            {
                List<Atendimento> lista = await _atendimentosRepository.GetAtendimentoByHospitalIdAsync(hospitalId);
                List<Especialidade> listaEspecialidade = (List<Especialidade>)await _especialidadesRepository.GetAllAsync();

                foreach (Especialidade esp in listaEspecialidade)
                {
                    int soma = lista.Where(item => item.IdEspecialidade == esp.IdEspecialidade).Sum(item => item.TempoAtendimento);
                    int qtd = lista.Where(item => item.IdEspecialidade == esp.IdEspecialidade).Count();
                    int media = 0;

                    if (qtd > 0)
                    {
                        media = soma / qtd;
                        int hr = media / 60;
                        int min = media % 60;

                        if (min < 10)
                            esp.TempoMedioConvertido = string.Format("{0}:0{1}", hr, min);
                        else
                            esp.TempoMedioConvertido =  string.Format("{0}:{1}", hr, min);
                    }
                    else
                    {
                        esp.TempoMedioConvertido = "0:00";
                    }                    
                }

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