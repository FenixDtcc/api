using QuantoDemoraApi.Models;

namespace QuantoDemoraApi.Repository.Interfaces
{
    public interface IStatusAtendimentosRepository
    {
        public Task<IEnumerable<StatusAtendimento>> GetAllAsync();
        public Task<StatusAtendimento> GetByIdAsync(int statusAtendimentoId);
    }
}
