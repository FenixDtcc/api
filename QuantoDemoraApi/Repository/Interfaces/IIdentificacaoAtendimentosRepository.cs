using QuantoDemoraApi.Models;

namespace QuantoDemoraApi.Repository.Interfaces
{
    public interface IIdentificacaoAtendimentosRepository
    {
        Task<IEnumerable<IdentificacaoAtendimento>> GetAllAsync();
        Task<IdentificacaoAtendimento> GetByIdAsync(int identificacaoAtendimentoId);
    }
}
