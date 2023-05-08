using QuantoDemoraApi.Models;

namespace QuantoDemoraApi.Repository.Interfaces
{
    public interface ILogradourosRepository
    {
        public Task<IEnumerable<Logradouro>> GetAllAsync();
        public Task<Logradouro> GetByIdAsync(int logradouroId);
    }
}
