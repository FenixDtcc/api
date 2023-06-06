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
        
        [NotMapped]
        public Hospital Hospital { get; set; }
        public int IdHospital { get; set; }
       
        [NotMapped]
        public Especialidade Especialidade { get; set; }
        public int IdEspecialidade { get; set; }
        
        [NotMapped]
        public EspecialidadeEnum EspcialidadeEnum { get; set; }
        [NotMapped]
        public IdentificacaoAtendimento IdentificacaoAtendimento { get; set; }
        [Column ("idTriagem")]
        public int IdIdentificacaoAtendimento { get; set; }
        [NotMapped]
        public IdentificacaoAtendimentoEnum IdentificacaoAtendimentoEnum { get; set; }
        [NotMapped]
        public Associado Associado { get; set; }
        public int IdAssociado { get; set; }
        public int TempoAtendimento { get; set; }
    }
}