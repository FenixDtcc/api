using Microsoft.EntityFrameworkCore;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Utils;

namespace QuantoDemoraApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Associado> Associados { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<AtendimentoEvento> AtendimentosEventos { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Hospital> Hospitais { get; set; }
        public DbSet<IdentificacaoAtendimento> IdentificacaoAtendimentos { get; set; }
        public DbSet<Logradouro> Logradouros { get; set; }
        public DbSet<StatusAtendimento> StatusAtendimentos { get; set; }
        public DbSet<TipoContato> TiposContatos { get; set; }
        public DbSet<Unidade> Unidades { get; set; }
        public DbSet<UnidadeEspecialidade> UnidadesEspecialidades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Associado>().HasData
        (
            new Associado()
            {
                IdAssociado = 1,
                NomeAssociado = "Admin",
                SobrenomeAssociado = "Admin",
                Cpf = "123.456.789-10",
                DtNascimento = DateTime.Parse("01/01/2000"),
                Sexo = 'M',
                DddCelular = "11",
                NroCelular = "123456789",
                Email = "quantodemora@gmail.com"
            }
        );



            var user = new Usuario();
            Criptografia.CriarPasswordHash("123456", out byte[] hash, out byte[] salt);
            user.IdUsuario = 1;
            user.NomeUsuario = "Admin";
            user.PasswordString = string.Empty;
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            user.TpUsuario = "Admin";
            user.DtCadastro = DateTime.Now;

            mb.Entity<Usuario>().HasData(user);

            mb.Entity<Usuario>().Property(u => u.TpUsuario).HasDefaultValue("Comum");
        }
    }
}