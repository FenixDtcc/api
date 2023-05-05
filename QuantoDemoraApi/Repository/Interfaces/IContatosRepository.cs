using QuantoDemoraApi.Models;

namespace QuantoDemoraApi.Repository.Interfaces
{
    public interface IContatosRepository
    {
        Task<IEnumerable<Contato>> GetAllAsync();
        Task<IEnumerable<Contato>> GetByIdAsync(int hospitalId);
    }
}
