using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuantoDemoraApi.Models.Enums;

namespace QuantoDemoraApi.Models
{
    public class StatusAtendimento
    {
        public int IdStatusAtendimento { get; set; }
        public string DsStatusAtendimento { get; set; }
        public StatusAtendimentoEnum StatusAtendimentoEnum { get; set; }
    }
}