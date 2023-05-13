using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;

namespace QuantoDemoraApi.Repository
{
    public class ContatosRepository : IContatosRepository
    {
        private static readonly ILog _logger = LogManager.GetLogger("Contatos Repository");

        private readonly DataContext _context;
        public ContatosRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contato>> GetAllAsync()
        {
            try
            {
                List<Contato> lista = await _context.Contatos.ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<IEnumerable<Contato>> GetByIdAsync(int hospitalId)
        {
            try
            {
                List<Contato> lista = await _context.Contatos
                    .Where(x => x.IdHospital == hospitalId).ToListAsync();

                if (lista.IsNullOrEmpty())
                    throw new Exception("Hospital não encontrado, favor conferir o id informado.");

                return lista;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }
    }
}