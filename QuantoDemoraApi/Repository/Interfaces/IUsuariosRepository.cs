using QuantoDemoraApi.Models;

namespace QuantoDemoraApi.Repository.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int usuarioId);
        Task<Usuario> GetByNameAsync(string nomeUsuario);
        Task<Usuario> CadastrarAdminAsync(Usuario usuario);
        Task<Usuario> CadastrarAsync(Usuario usuario);
        Task<Usuario> AutenticarAsync(Usuario creds);
        // Task<int> AlterarCadastroAsync(Usuario usuario);
        Task<int> AlterarEmailAsync(Usuario usuario);
        Task<int> AlterarNomeAsync(Usuario usuario);
        Task<int> AlterarSenhaAsync(Usuario usuario);
        Task<int> AtualizarLocalizacaoAsync(Usuario usuario);
        Task<int> DeletarAsync(int usuarioId);
    }
}
