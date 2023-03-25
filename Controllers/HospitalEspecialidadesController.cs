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
    public class HospitalEspecialidadesController : ControllerBase
    {
        private readonly DataContext _context;
        public HospitalEspecialidadesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Get()
        {
            try
            {
                //List<Hospital> hospitais = new List<Hospital>();
                List<Hospital> hospitais = await _context.Hospitais
                .Include(he => he.HospitalEspecialidades)
                .ThenInclude(e => e.Especialidade)
                .ToListAsync();
                return Ok(hospitais);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{hospitalId}")]
        public async Task<IActionResult> GetId(int hospitalId)
        {
            try
            {
                Hospital hospital = await _context.Hospitais
                    .Include(he => he.HospitalEspecialidades)
                    .ThenInclude(e => e.Especialidade)
                    .FirstOrDefaultAsync(x => x.IdHospital == hospitalId);
                return Ok(hospital);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}