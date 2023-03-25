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
    public class AssociadosController : ControllerBase
    {
        private readonly DataContext _context;
        public AssociadosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Associado> lista = await _context.Associados.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{associadoId}")]
        public async Task<IActionResult> GetId(int associadoId)
        {
            try
            {
                Associado associado = await _context.Associados
                    .FirstOrDefaultAsync(x => x.IdAssociado == associadoId);
                return Ok(associado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}