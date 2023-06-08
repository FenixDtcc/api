using QuantoDemoraApi.Models;

namespace QuantoDemoraApi.Repository.Interfaces
{
    public interface IAtendimentosRepository
    {
        Task<IEnumerable<Atendimento>> GetAllAsync();
        Task<List<Atendimento>> GetAtendimentoByHospitalIdAsync(int hospitalId);
    }
}
