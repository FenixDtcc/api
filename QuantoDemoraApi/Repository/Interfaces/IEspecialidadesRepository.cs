using QuantoDemoraApi.Models;

namespace QuantoDemoraApi.Repository.Interfaces
{
    public interface IEspecialidadesRepository
    {
        Task<IEnumerable<Especialidade>> GetAllAsync();
        Task<Especialidade> GetByIdAsync(int especialidadeId);
    }
}
