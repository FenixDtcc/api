using QuantoDemoraApi.Models;
using System.ComponentModel.DataAnnotations;

namespace QuantoDemoraApi.Repository.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int usuarioId);
        Task<Usuario> GetByNameAsync(string nomeUsuario);
        Task<Usuario> CadastrarAdminAsync(Usuario usarioAdmin);
        Task<bool> VerificarNomeUsuarioExistente(string nomeUsuario);
        Task<bool> VerificarEmailExistente(string emailUsuario);
    }
}
