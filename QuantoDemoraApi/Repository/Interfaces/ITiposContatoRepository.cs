using QuantoDemoraApi.Models;

namespace QuantoDemoraApi.Repository.Interfaces
{
    public interface ITiposContatoRepository
    {
        public Task<IEnumerable<TipoContato>> GetAllAsync();
        public Task<TipoContato> GetByIdAsync(int tipoContatoId);
    }
}
