using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;

namespace QuantoDemoraApi.Repository
{
    public class HospitaisRepository : IHospitaisRepository
    {
        private static readonly ILog _logger = LogManager.GetLogger("Hospitais Repository");
        private readonly DataContext _context;
        public HospitaisRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hospital>> GetAllAsync()
        {
            try
            {
                List<Hospital> lista = await _context.Hospitais.ToListAsync();
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
                    .FirstOrDefaultAsync(x => x.IdHospital == hospitalId);

                if (hospital == null)
                    throw new Exception("Hospital não encontrado, favor conferir o id informado.");

                return hospital;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<IEnumerable<Hospital>> GetByNameAsync(string nomeHospital)
        {
            try
            {
                List<Hospital> lista = await _context.Hospitais.ToListAsync();
                var busca = lista.Where(x => x.NomeFantasia.ToLower().Contains(nomeHospital.ToLower()));

                if (busca.IsNullOrEmpty())
                    throw new Exception("Hospital não encontrado, favor conferir o nome informado.");

                return busca;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }
    }
}