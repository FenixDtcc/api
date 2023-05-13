using log4net;
using Microsoft.EntityFrameworkCore;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;

namespace QuantoDemoraApi.Repository
{
    public class EspecialidadesRepository : IEspecialidadesRepository
    {
        private static readonly ILog _logger = LogManager.GetLogger("Especialidades Repository");
        private readonly DataContext _context;
        public EspecialidadesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Especialidade>> GetAllAsync()
        {
            try
            {
                List<Especialidade> lista = await _context.Especialidades.ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<Especialidade> GetByIdAsync(int especialidadeId)
        {
            try
            {
                Especialidade especialidade = await _context.Especialidades
                    .FirstOrDefaultAsync(x => x.IdEspecialidade == especialidadeId);

                if (especialidade == null)
                    throw new Exception("Especialidade não encontrada, favor conferir o id informado.");

                return especialidade;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }
    }
}