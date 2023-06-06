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
        public AtendimentosController(IAtendimentosRepository atendimentosRepository)
        {
            _atendimentosRepository = atendimentosRepository;
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
        [HttpGet("{hospitalId}/{especialidadeId}")]
        public async Task<IActionResult> GetIdAsync(int hospitalId, int especialidadeId)
        {
            try
            {
                int tempoAtendimento = await _atendimentosRepository.GetByIdAsync(hospitalId, especialidadeId);
                return Ok(tempoAtendimento);
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{hospitalId}")]
        public async Task<IActionResult> GetByHospitalIdAsync(int hospitalId)
        {
            try
            {
                List<Atendimento> lista = await _atendimentosRepository.GetByHospitalIdAsync(hospitalId);

                List<Especialidade> listaEspecialidade = new List<Especialidade>();

                Especialidade especialidade1 = new Especialidade();                
                int somaClinica = lista.Where(item => item.IdEspecialidade == 1).Sum(item => item.TempoAtendimento);
                int qtdClinica = lista.Where(item => item.IdEspecialidade == 1).Count();
                //decimal mediaClinica = 0;
                //if (qtdClinica > 0)
                //    mediaClinica = somaClinica / qtdClinica;

                // Tentativa de "converter" o tempo de minutos para horas:
                int mediaClinica = 0;
                if (qtdClinica > 0)
                {
                    mediaClinica = somaClinica / qtdClinica;
                    int hr = mediaClinica / 60;
                    int min = mediaClinica % 60;

                    if (min < 10)
                        especialidade1.TempoMedioConvertido = string.Format("{0}:0{1}", hr, min);
                    else
                        especialidade1.TempoMedioConvertido =  string.Format("{0}:{1}", hr, min);
                }
                else
                {
                    especialidade1.TempoMedioConvertido = "0:00";
                }

                especialidade1.DsEspecialidade = "Clinica Medica";
                especialidade1.TempoMedio = mediaClinica;
                especialidade1.IdEspecialidade = 1;
                listaEspecialidade.Add(especialidade1);


                Especialidade especialidade2 = new Especialidade();
                int somaOrto = lista.Where(item => item.IdEspecialidade == 2).Sum(item => item.TempoAtendimento);
                int qtdOrto = lista.Where(item => item.IdEspecialidade == 2).Count();
                decimal mediaOrto = 0;                
                if(qtdOrto >0)
                    mediaOrto = somaOrto / qtdOrto;                
                especialidade2.DsEspecialidade = "Ortopedia e Traumatologia";
                especialidade2.TempoMedio = mediaOrto;
                especialidade2.IdEspecialidade = 2;
                listaEspecialidade.Add(especialidade2);

                Especialidade especialidade3 = new Especialidade();
                int somaPediatria = lista.Where(item => item.IdEspecialidade == 3).Sum(item => item.TempoAtendimento);
                int qtdPediatria = lista.Where(item => item.IdEspecialidade == 3).Count();
                decimal mediaPediatria = 0;
                if (qtdPediatria > 0)
                    mediaPediatria = somaPediatria / qtdPediatria;
                especialidade3.DsEspecialidade = "Pediatria";
                especialidade3.TempoMedio = mediaPediatria;
                especialidade3.IdEspecialidade = 3;
                listaEspecialidade.Add(especialidade3);

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