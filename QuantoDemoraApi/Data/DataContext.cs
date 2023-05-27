using Microsoft.EntityFrameworkCore;
using QuantoDemoraApi.Models;

namespace QuantoDemoraApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Associado> Associados { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Hospital> Hospitais { get; set; }
        public DbSet<HospitalEspecialidade> HospitalEspecialidades { get; set; }
        public DbSet<IdentificacaoAtendimento> IdentificacaoAtendimentos { get; set; }
        public DbSet<Logradouro> Logradouros { get; set; }
        public DbSet<TipoContato> TiposContato { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}