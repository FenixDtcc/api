using QuantoDemoraApi.Models;

namespace QuantoDemoraApi.Repository.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int usuarioId);
        Task<Usuario> GetByNameAsync(string nomeUsuario);
        Task<Usuario> CadastrarAdminAsync(Usuario usarioAdmin);
        Task<Usuario> CadastrarAsync(Usuario usuario);
        Task<Usuario> AutenticarAsync(Usuario creds);
        Task<Usuario> AlterarEmailAsync(Usuario email);
        Task<Usuario> AlterarNomeAsync(Usuario nome);
        Task<Usuario> AlterarSenhaAsync(Usuario senha);
        Task<Usuario> AtualizarLocalizacaoAsync(Usuario usuario);
        Task<int> DeletarAsync(int usuarioId);
    }
}
