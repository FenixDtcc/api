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
                List<Contato> lista = await _context.Contatos.ToListAsync();
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
                List<Contato> lista = await _context.Contatos
                    .Where(x => x.IdHospital == hospitalId).ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}