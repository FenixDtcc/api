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
    public class TiposContatoController : ControllerBase
    {
        private readonly DataContext _context;
        public TiposContatoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<TipoContato> lista = await _context.TiposContato.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{tipoContatoId}")]
        public async Task<IActionResult> GetId(int tipoContatoId)
        {
            try
            {
                TipoContato tipoContato = await _context.TiposContato
                    .FirstOrDefaultAsync(x => x.IdTipoContato == tipoContatoId);
                return Ok(tipoContato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}