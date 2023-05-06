using log4net;
using Microsoft.EntityFrameworkCore;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;

namespace QuantoDemoraApi.Repository
{
    public class HospitalEspecialidadesRepository : IHospitalEspecialidadesRepository
    {
        private static readonly ILog _logger = LogManager.GetLogger("HospitalEspecialidades Repository");
        private readonly DataContext _context;
        public HospitalEspecialidadesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hospital>> GetAllAsync()
        {
            try
            {
                List<Hospital> lista = await _context.Hospitais
                .Include(he => he.HospitalEspecialidades)
                .ThenInclude(e => e.Especialidade)
                .ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<Hospital> GetByIdAsync(int hospitalId)
        {
            try
            {
                Hospital hospital = await _context.Hospitais
                    .Include(he => he.HospitalEspecialidades)
                    .ThenInclude(e => e.Especialidade)
                    .FirstOrDefaultAsync(x => x.IdHospital == hospitalId);
                return hospital;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }
    }
}
