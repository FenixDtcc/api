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
    public class ContatosController : ControllerBase
    {
        private readonly DataContext _context;
        public ContatosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Get()
        {
            try
            {
                //List<Contato> contatos = new List<Contato>();
                List<Contato> contatos = await _context.Contatos.ToListAsync();
                return Ok(contatos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // REVISAR:
        [HttpGet("{hospitalId}")]
        public async Task<IActionResult> GetId(int hospitalId)
        {
            try
            {
                Contato contato = await _context.Contatos
                    .FirstOrDefaultAsync(x => x.IdHospital == hospitalId);
                return Ok(contato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}