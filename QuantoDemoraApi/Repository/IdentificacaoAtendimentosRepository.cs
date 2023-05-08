using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;

namespace QuantoDemoraApi.Repository
{
    public class IdentificacaoAtendimentosRepository : IIdentificacaoAtendimentosRepository
    {
        private static readonly ILog _logger = LogManager.GetLogger("IdentificacaoAtendimentos Repository");
        private readonly DataContext _context;
        public IdentificacaoAtendimentosRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IdentificacaoAtendimento>> GetAllAsync()
        {
            try
            {
                List<IdentificacaoAtendimento> lista = await _context.IdentificacaoAtendimentos.ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }
        public async Task<IdentificacaoAtendimento> GetByIdAsync(int identificacaoAtendimentoId)
        {
            try
            {
                IdentificacaoAtendimento identificacaoAtendimento = await _context.IdentificacaoAtendimentos
                    .FirstOrDefaultAsync(x => x.IdIdentificacaoAtendimento == identificacaoAtendimentoId);
                return identificacaoAtendimento;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }
    }
}
