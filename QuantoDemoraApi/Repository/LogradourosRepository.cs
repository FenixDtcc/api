using log4net;
using Microsoft.EntityFrameworkCore;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;

namespace QuantoDemoraApi.Repository
{
    public class LogradourosRepository : ILogradourosRepository
    {
        private static readonly ILog _logger = LogManager.GetLogger("Logradouros Repository");
        private readonly DataContext _context;
        public LogradourosRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Logradouro>> GetAllAsync()
        {
            try
            {
                List<Logradouro> lista = await _context.Logradouros.ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<Logradouro> GetByIdAsync(int logradouroId)
        {
            try
            {
                Logradouro logradouro = await _context.Logradouros
                    .FirstOrDefaultAsync(x => x.IdLogradouro == logradouroId);

                if (logradouro == null)
                    throw new Exception("Logradouro não encontrado, favor conferir o id informado.");

                return logradouro;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }
    }
}