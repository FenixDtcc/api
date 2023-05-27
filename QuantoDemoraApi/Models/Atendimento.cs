using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using QuantoDemoraApi.Models.Enums;

namespace QuantoDemoraApi.Models
{
    [Table("Atendimentos")]
    public class Atendimento
    {
        [Key]
        public string SenhaAtendimento { get; set; }
        public int IdAtendimento { get; set; }
        public Hospital Hospital { get; set; }
        public int IdHospital { get; set; }
        public Especialidade Especialidade { get; set; }
        public int IdEspecialidade { get; set; }
        public EspecialidadeEnum EspcialidadeEnum { get; set; }
        public IdentificacaoAtendimento IdentificacaoAtendimento { get; set; }
        [Column ("idTriagem")]
        public int IdIdentificacaoAtendimento { get; set; }
        public IdentificacaoAtendimentoEnum IdentificacaoAtendimentoEnum { get; set; }
        public int TempoAtendimento { get; set; }
    }
}