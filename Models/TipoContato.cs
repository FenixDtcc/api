using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using QuantoDemoraApi.Models.Enums;

namespace QuantoDemoraApi.Models
{
    [Table("TiposContato")]
    public class TipoContato
    {
        [Key]
        public int IdTipoContato { get; set; }
        public string DsTipoContato { get; set; }
    }
}