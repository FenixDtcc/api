using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuantoDemoraApi.Models
{
    [Table("Especialidades")]
    public class Especialidade
    {
        [Key]
        public int IdEspecialidade { get; set; }
        public string DsEspecialidade { get; set; }
    }
}