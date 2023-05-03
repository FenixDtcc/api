using log4net;
using Microsoft.EntityFrameworkCore;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;

namespace QuantoDemoraApi.Repository
{
    public class AssociadosRepository : IAssociadosRepository
    {
        private static readonly ILog _logger = LogManager.GetLogger("Associados Repository");

        private readonly DataContext _context;
        public AssociadosRepository(DataContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Associado>> GetAllAsync()
        {
            try
            {
                List<Associado> lista = await _context.Associados.ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
               _logger.Info(ex);
               throw;
            }
        }

        public async Task<Associado> GetByIdAsync(int associadoId)
        {
            try
            {
                Associado associado = await _context.Associados
                    .FirstOrDefaultAsync(x => x.IdAssociado == associadoId);
                return associado;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }
    }
}
