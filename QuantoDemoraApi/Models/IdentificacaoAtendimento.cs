using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuantoDemoraApi.Models
{
    [Table("IdentificacaoAtendimentos")]
    public class IdentificacaoAtendimento
    {
        [Key]
        public int IdIdentificacaoAtendimento { get; set; }
        public string DsIdentificacaoAtendimento { get; set; }
        public string DfIdentificacaoAtendimento { get; set; }
    }
}