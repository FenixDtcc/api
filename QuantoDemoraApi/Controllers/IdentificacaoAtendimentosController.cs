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
    public class IdentificacaoAtendimentosController : ControllerBase
    {
        private readonly DataContext _context;
        public IdentificacaoAtendimentosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<IdentificacaoAtendimento> lista = await _context.IdentificacaoAtendimentos.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{identificacaoAtendimentoId}")]
        public async Task<IActionResult> GetId(int identificacaoAtendimentoId)
        {
            try
            {
                IdentificacaoAtendimento identificacaoAtendimento = await _context.IdentificacaoAtendimentos
                    .FirstOrDefaultAsync(x => x.IdIdentificacaoAtendimento == identificacaoAtendimentoId);
                return Ok(identificacaoAtendimento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

