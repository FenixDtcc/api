using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using QuantoDemoraApi.Models.Enums;

namespace QuantoDemoraApi.Models
{
    public class StatusAtendimento
    {
        [Key]
        public int IdStatusAtendimento { get; set; }
        public string DsStatusAtendimento { get; set; }
    }
}