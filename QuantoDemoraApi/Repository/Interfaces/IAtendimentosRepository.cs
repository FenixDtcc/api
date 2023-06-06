using QuantoDemoraApi.Models;

namespace QuantoDemoraApi.Repository.Interfaces
{
    public interface IAtendimentosRepository
    {
        Task<IEnumerable<Atendimento>> GetAllAsync();
        Task<int> GetByIdAsync(int hospitalId, int especialidadeId);
        Task<List<Atendimento>> GetByHospitalIdAsync(int hospitalId);
        //Task<IEnumerable<Atendimento>> GetByIdAsync(int hospitalId, int especialidadeId);
    }
}
