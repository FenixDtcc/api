using log4net;
using Microsoft.EntityFrameworkCore;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;

namespace QuantoDemoraApi.Repository
{
    public class TiposContatoRepository : ITiposContatoRepository
    {
        private static readonly ILog _logger = LogManager.GetLogger("TiposContato Repository");
        private readonly DataContext _context;
        public TiposContatoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoContato>> GetAllAsync()
        {
            try
            {
                List<TipoContato> lista = await _context.TiposContato.ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<TipoContato> GetByIdAsync(int tipoContatoId)
        {
            try
            {
                TipoContato tipoContato = await _context.TiposContato
                    .FirstOrDefaultAsync(x => x.IdTipoContato == tipoContatoId);
                return tipoContato;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }
    }
}
