using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;

namespace QuantoDemoraApi.Repository
{
    public class AtendimentosRepository : IAtendimentosRepository
    {
        private static readonly ILog _logger = LogManager.GetLogger("Atendimentos Repository");
        private readonly DataContext _context;
        public AtendimentosRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Atendimento>> GetAllAsync()
        {
            try
            {
                List<Atendimento> lista = await _context.Atendimentos.ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        //public async Task<IEnumerable<Atendimento>> GetByIdAsync(int hospitalId, int especialidadeId)
        public async Task<int> GetByIdAsync(int hospitalId, int especialidadeId)
        {
            try
            {
                //Atendimento atendimento = await _context.Atendimentos
                //    .Include(h => h.Hospital)
                //    .Include(e => e.Especialidade)
                //    .FirstOrDefaultAsync(x => x.IdHospital == hospitalId);

                //return atendimento;

                int especialidade;

                List<Atendimento> lista = await _context.Atendimentos
                    .Where(x => x.IdHospital == hospitalId)
                    .Where(x => x.IdEspecialidade == especialidadeId)
                    .ToListAsync();

                if (lista.IsNullOrEmpty())
                    throw new Exception("Hospital/Especialidade não encontrados, favor conferir o id informado.");

                if (lista.Count > 0)
                {
                    if (hospitalId == 1 &&  especialidadeId == 1)
                    {
                        especialidade = lista.Count;
                        return lista.Sum(x => x.TempoAtendimento) / especialidade;
                    }
                }

                return 0;

            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }
    }
}

