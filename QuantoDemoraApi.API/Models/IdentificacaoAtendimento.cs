using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using QuantoDemoraApi.Models.Enums;

namespace QuantoDemoraApi.Models
{
    [Table("IdentificacaoAtendimento")]
    public class IdentificacaoAtendimento
    {
        [Key]
        public int IdIdentificacaoAtendimento { get; set; }
        public string DsIdentificacaoAtendimento { get; set; }
        public string DfIdentificacaoAtendimento { get; set; }
    }
}