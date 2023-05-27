using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuantoDemoraApi.Models
{
    [Table("Associados")]
    public class Associado
    {
        [Key]
        public int IdAssociado { get; set; }
        public string NomeAssociado { get; set; }
        public string SobrenomeAssociado { get; set; }
        public string Cpf { get; set; }
        public char Sexo { get; set; }
        public string DddCelular { get; set; }
        public string NroCelular { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}