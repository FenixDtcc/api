using QuantoDemoraApi.Models;

namespace QuantoDemoraApi.Repository.Interfaces
{
    public interface IAtendimentosRepository
    {
        Task<IEnumerable<Atendimento>> GetAllAsync();
        Task<IEnumerable<Atendimento>> GetByIdAsync(int hospitalId, int especialidadeId);
        //Task<Atendimento> GetByIdAsync(int hospitalId);
    }
}
