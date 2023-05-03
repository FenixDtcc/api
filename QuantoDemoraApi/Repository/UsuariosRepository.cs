using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;

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
            catch(Exception ex)
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
            catch(Exception ex)
            {
                _logger.Info(ex);
                throw;
            }

        }
    }
}
