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
    public class LogradourosController : ControllerBase
    {
        private readonly DataContext _context;
        public LogradourosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Logradouro> lista = await _context.Logradouros.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{logradouroId}")]
        public async Task<IActionResult> GetId(int logradouroId)
        {
            try
            {
                Logradouro logradouro = await _context.Logradouros
                    .FirstOrDefaultAsync(x => x.IdLogradouro == logradouroId);
                return Ok(logradouro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}