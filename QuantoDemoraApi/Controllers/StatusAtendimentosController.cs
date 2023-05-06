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
    public class StatusAtendimentosController : ControllerBase
    {
        private readonly DataContext _context;
        public StatusAtendimentosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                List<StatusAtendimento> lista = await _context.StatusAtendimentos.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{statusAtendimentoId}")]
        public async Task<IActionResult> GetIdAsync(int statusAtendimentoId)
        {
            try
            {
                StatusAtendimento statusAtendimento = await _context.StatusAtendimentos
                    .FirstOrDefaultAsync(x => x.IdStatusAtendimento == statusAtendimentoId);
                return Ok(statusAtendimento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}