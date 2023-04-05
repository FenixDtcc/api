using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;

namespace QuantoDemoraApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class EspecialidadesController : ControllerBase
    {
        private readonly DataContext _context;
        public EspecialidadesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Especialidade> lista = await _context.Especialidades.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{especialidadeId}")]
        public async Task<IActionResult> GetId(int especialidadeId)
        {
            try
            {
                Especialidade especialidade = await _context.Especialidades
                    .FirstOrDefaultAsync(x => x.IdEspecialidade == especialidadeId);
                return Ok(especialidade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}