using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using QuantoDemoraApi.Models.Enums;

namespace QuantoDemoraApi.Models
{
    public class Contato
    {
        public Hospital Hospital { get; set; }
        [Key]
        public int IdHospital { get; set; }
        //[Key]
        public int IdContato { get; set; }
        public TipoContato TipoContato { get; set; }
        //[Key]
        public int IdTipoContato { get; set; }
        public TipoContatoEnum TipoContatoEnum { get; set; }
        public string DsContato { get; set; }
        public string InContato { get; set; }
    }
}