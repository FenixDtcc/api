using QuantoDemoraApi.Models;

namespace QuantoDemoraApi.Repository.Interfaces
{
    public interface IHospitaisRepository
    {
        Task<IEnumerable<Hospital>> GetAllAsync();
        Task<Hospital> GetByIdAsync(int hospitalId);
        Task <IEnumerable<Hospital>> GetByNameAsync(string nomeHospital);
    }
}
