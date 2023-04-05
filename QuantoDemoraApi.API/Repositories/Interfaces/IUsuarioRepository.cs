using QuantoDemoraApi.Models;

namespace QuantoDemoraApi.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAsync();
        Task<bool> UsuarioExistente(string nomeDoUsuario);
    }
}
