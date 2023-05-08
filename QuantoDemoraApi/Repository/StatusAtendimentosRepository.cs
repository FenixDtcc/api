using log4net;
using Microsoft.EntityFrameworkCore;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;

namespace QuantoDemoraApi.Repository
{
    public class StatusAtendimentosRepository : IStatusAtendimentosRepository
    {
        private static readonly ILog _logger = LogManager.GetLogger("StatusAtendimentos Repository");
        private readonly DataContext _context;
        public StatusAtendimentosRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StatusAtendimento>> GetAllAsync()
        {
            try
            {
                List<StatusAtendimento> lista = await _context.StatusAtendimentos.ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<StatusAtendimento> GetByIdAsync(int statusAtendimentoId)
        {
            try
            {
                StatusAtendimento statusAtendimento = await _context.StatusAtendimentos
                    .FirstOrDefaultAsync(x => x.IdStatusAtendimento == statusAtendimentoId);    
                return statusAtendimento;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }
    }
}
