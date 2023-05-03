using QuantoDemoraApi.Models;

namespace QuantoDemoraApi.Repository.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int usuarioId);
    }
}
