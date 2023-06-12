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
        private readonly IEspecialidadesRepository _especialidadesRepository;
        public AtendimentosRepository(DataContext context, IEspecialidadesRepository especialidadesRepository)
        {
            _context = context;
            _especialidadesRepository = especialidadesRepository;
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

        public async Task<List<Atendimento>> GetAtendimentoPorEspecialidadeByHospitalIdAsync(int hospitalId)
        {
            try
            {
                List<Atendimento> listaAtendimento = await _context.Atendimentos
                    .Where(x => x.IdHospital == hospitalId).ToListAsync();

                List<Especialidade> listaEspecialidade = (List<Especialidade>)await _especialidadesRepository.GetAllAsync();

                foreach (Especialidade esp in listaEspecialidade)
                {
                    int soma = listaAtendimento.Where(item => item.IdEspecialidade == esp.IdEspecialidade).Sum(item => item.TempoAtendimento);
                    int qtd = listaAtendimento.Where(item => item.IdEspecialidade == esp.IdEspecialidade).Count();
                    int media = 0;

                    if (qtd > 0)
                    {
                        media = soma / qtd;
                        int hr = media / 60;
                        int min = media % 60;

                        if (min < 10)
                            esp.TempoMedioConvertido = string.Format("{0}:0{1}", hr, min);
                        else
                            esp.TempoMedioConvertido = string.Format("{0}:{1}", hr, min);

                        esp.TempoMedioMinutos = media;
                    }
                    else
                    {
                        esp.TempoMedioMinutos = 0;
                        esp.TempoMedioConvertido = "0:00";
                    }
                }

                if (listaAtendimento.IsNullOrEmpty())
                    throw new Exception("Hospital não encontrado, favor conferir o id informado.");

                return listaAtendimento;                
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }
    }
}

