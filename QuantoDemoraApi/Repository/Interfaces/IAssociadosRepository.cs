using QuantoDemoraApi.Models;

namespace QuantoDemoraApi.Repository.Interfaces
{
    public interface IAssociadosRepository
    {
        Task<IEnumerable<Associado>> GetAllAsync();
        Task<Associado> GetByIdAsync(int associadoId);

    }
}
