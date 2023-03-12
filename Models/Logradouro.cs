using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuantoDemoraApi.Models.Enums;

namespace QuantoDemoraApi.Models
{
    public class Logradouro
    {
        public int IdLogradouro { get; set; }
        public string DsLogradouro { get; set; }
        public LogradouroEnum LogradouroEnum { get; set; }
    }
}