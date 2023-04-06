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
    public class HospitaisController : ControllerBase
    {
        private readonly DataContext _context;
        public HospitaisController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Hospital> lista = await _context.Hospitais.ToListAsync();
                return Ok(lista);
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