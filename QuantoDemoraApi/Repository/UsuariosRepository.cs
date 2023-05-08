using log4net;
using Microsoft.EntityFrameworkCore;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;
using QuantoDemoraApi.Utils;
using System.ComponentModel.DataAnnotations;

namespace QuantoDemoraApi.Repository
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private static readonly ILog _logger = LogManager.GetLogger("Usuarios Repository");
        private readonly DataContext _context;
        public UsuariosRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            try
            {
                List<Usuario> lista = await _context.Usuarios.ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<Usuario> GetByIdAsync(int usuarioId)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.IdUsuario == usuarioId);
                return usuario;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<Usuario> GetByNameAsync(string nomeUsuario)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.NomeUsuario.ToLower() == nomeUsuario.ToLower());
                return usuario;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<Usuario> CadastrarAdminAsync(Usuario ua)
        {
            try
            {
                // Verificar porque não está dando certo
                /*if (await VerificarNomeUsuarioExistente(ua.NomeUsuario) == true)
                    throw new Exception("O nome de usuário escolhido já está em uso");*/

                Criptografia.CriarPasswordHash(ua.PasswordString, out byte[] hash, out byte[] salt);
                ua.PasswordString = string.Empty;
                ua.PasswordHash = hash;
                ua.PasswordSalt = salt;
                ua.Email = "quantodemora@gmail.com";
                ua.Cpf = "000.000.001-91";
                ua.DtCadastro = LocalDateTime.HorarioBrasilia();
                ua.TpUsuario = "Admin";

                await _context.Usuarios.AddAsync(ua);
                await _context.SaveChangesAsync();

                return ua;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        // Verificar porque não está dando certo
        public async Task<bool> VerificarNomeUsuarioExistente(string nomeUsuario)
        {
            if (await _context.Usuarios.AnyAsync(x => x.NomeUsuario.ToLower() == nomeUsuario.ToLower()))
            {
                return true;
            }
            return false;
        }

        // Verificar porque não está dando certo
        public async Task<bool> VerificarEmailExistente(string emailUsuario)
        {
            if (await _context.Usuarios.AnyAsync(x => x.Email.ToLower() == emailUsuario.ToLower()))
            {
                return true;
            }
            return false;
        }
    }
}