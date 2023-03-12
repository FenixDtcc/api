using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantoDemoraApi.Models
{
    public class Unidade : Hospital
    {
        public int IdUnidade { get; set; }
        public string DsUnidade { get; set; }
        public Hospital Hospital { get; set; }
        public int IdHospital { get; set; }
    }
}