using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuantoDemoraApi.Models.Enums;

namespace QuantoDemoraApi.Models
{
    public class TipoContato
    {
        public int IdTipoContato { get; set; }
        public string DsTipoContato { get; set; }
        public TipoContatoEnum TipoContatoEnum { get; set; }
    }
}