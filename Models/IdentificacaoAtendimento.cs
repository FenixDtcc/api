using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuantoDemoraApi.Models.Enums;

namespace QuantoDemoraApi.Models
{
    public class IdentificacaoAtendimento
    {
        public int IdIdentificacaoAtendimento { get; set; }
        public string DsIdentificacaoAtendimento { get; set; }
        public string DfIdentificacaoAtendimento { get; set; }
        public IdentificacaoAtendimentoEnum IdentificacaoAtendimentoEnum { get; set; }
    }
}