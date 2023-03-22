using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using QuantoDemoraApi.Models.Enums;

namespace QuantoDemoraApi.Models
{
    public class Atendimento
    {
        [Key]
        public int IdAtendimento { get; set; }
        public Especialidade Especialidade { get; set; }
        public int IdEspecialidade { get; set; }
        public EspecialidadeEnum EspcialidadeEnum { get; set; }
        public StatusAtendimento StatusAtendimento { get; set; }
        public int IdStatusAtendimento { get; set; }
        public StatusAtendimentoEnum StatusAtendimentoEnum { get; set; }
        public IdentificacaoAtendimento IdentificacaoAtendimento { get; set; }
        public int IdIdentificacaoAtendimento { get; set; }
        public IdentificacaoAtendimentoEnum IdentificacaoAtendimentoEnum { get; set; }
        public Hospital Hospital { get; set; }
        public int IdHospital { get; set; }
        public Associado Associado { get; set; }
        public int IdAssociado { get; set; }
        public DateTime DtAtendimento { get; set; }
    }
}