using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using QuantoDemoraApi.Models.Enums;

namespace QuantoDemoraApi.Models
{
    public class Logradouro
    {
        [Key]
        public int IdLogradouro { get; set; }
        public string DsLogradouro { get; set; }
    }
}