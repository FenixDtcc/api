using log4net;
using Microsoft.EntityFrameworkCore;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;

namespace QuantoDemoraApi.Repository
{
    public class EventosRepository : IEventosRepository
    {
        private static readonly ILog _logger = LogManager.GetLogger("Eventos Repository");
        private readonly DataContext _context;
        public EventosRepository(DataContext context)
        {
            _context = context;      
        }

        public async Task<IEnumerable<Evento>> GetAllAsync()
        {
            try
            {
                List<Evento> lista = await _context.Eventos.ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<Evento> GetByIdAsync(int eventoId)
        {
            try
            {
                Evento evento = await _context.Eventos
                    .FirstOrDefaultAsync(x => x.IdEvento == eventoId);

                if (evento == null)
                    throw new Exception("Evento não encontrado, favor conferir o id informado.");

                return evento;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }
    }
}