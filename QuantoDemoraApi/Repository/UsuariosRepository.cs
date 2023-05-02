using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;

namespace QuantoDemoraApi.Repository
{
    public class UsuariosRepository : IUsuariosRepository
    {
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
                throw ex;
            }
        }
    }
}
