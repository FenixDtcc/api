using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Utils;

namespace QuantoDemoraApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _context;
        public UsuariosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Usuario> lista = await _context.Usuarios.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> GetId(int usuarioId)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.IdUsuario == usuarioId);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("NomeUsuario/{nomeUsuario}")]
        public async Task<IActionResult> GetUsuario(string nomeUsuario)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.NomeUsuario.ToLower() == nomeUsuario.ToLower());
                return Ok(usuario);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CadastrarAdmin")]
        public async Task<IActionResult> CadastrarAdmin(Usuario ua)
        {
            try
            {
                Criptografia.CriarPasswordHash(ua.PasswordString, out byte[] hash, out byte[] salt);
                ua.PasswordString = string.Empty;
                ua.PasswordHash = hash;
                ua.PasswordSalt = salt;
                ua.Email = "quantodemora@gmail.com";
                ua.Cpf = "999.999.999-99";
                ua.IdAssociado = 0;
                ua.DtCadastro = HorarioBrasilia();
                ua.TpUsuario = "Admin";
                await _context.Usuarios.AddAsync(ua);
                await _context.SaveChangesAsync();

                return Ok(ua.IdUsuario);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar(Usuario u)
        {
            Associado a = new Associado();

            try
            {
                if (await UsuarioExistente(u.NomeUsuario))
                {
                    throw new Exception("Nome de usuário já existe, favor escolher outro nome!");
                }

                Criptografia.CriarPasswordHash(u.PasswordString, out byte[] hash, out byte[] salt);
                u.PasswordString = string.Empty;
                u.PasswordHash = hash;
                u.PasswordSalt = salt;
                u.Email = string.Empty;
                u.Cpf = string.Empty;
                u.IdAssociado = a.IdAssociado;
                u.TpUsuario = "Comum";

                // Ver se tem como implementar isso:
                // u.Entity<Usuario>().Property(u => u.TpUsuario).HasDefaultValue("Comum");

                if (u.Cpf.Contains(a.Cpf))
                {
                    u.DtCadastro = HorarioBrasilia();
                    await _context.Usuarios.AddAsync(u);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("O CPF do informado não consta na Base de Dados do Plano de Saúde!");
                }

                return Ok(u.IdUsuario);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Autenticar")]
        public async Task<IActionResult> Autenticar(Usuario credenciais)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.NomeUsuario.ToLower().Equals(credenciais.NomeUsuario.ToLower()));

                if (usuario is null)
                {
                    throw new System.Exception("Usuário não encontrado!");
                }
                else if (!Criptografia.VerificarPasswordHash(credenciais.PasswordString, usuario.PasswordHash, usuario.PasswordSalt))
                {
                    throw new System.Exception("Senha incorreta!");
                }
                else
                {
                    usuario.DtAcesso = HorarioBrasilia();
                    _context.Usuarios.Update(usuario);
                    await _context.SaveChangesAsync();

                    usuario.PasswordHash = null;
                    usuario.PasswordSalt = null;
                    return Ok(usuario);
                    //return Ok(usuario.Id);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<bool> UsuarioExistente(string nomeUsuario)
        {
            if (await _context.Usuarios.AnyAsync(x => x.NomeUsuario.ToLower() == nomeUsuario.ToLower()))
            {
                return true;
            }
            return false;
        }

        public DateTime HorarioBrasilia() => TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
    }
}