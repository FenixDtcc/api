using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuantoDemoraApi.Models.Enums;

namespace QuantoDemoraApi.Models
{
    public class Contato
    {
        public Hospital Hospital { get; set; }
        public int IdHospital { get; set; }
        public int IdContato { get; set; }
        public TipoContato TipoContato { get; set; }
        public int IdTipoContato { get; set; }
        public TipoContatoEnum TipoContatoEnum { get; set; }
        public string DsContato { get; set; }
        public string InContato { get; set; }
    }
}