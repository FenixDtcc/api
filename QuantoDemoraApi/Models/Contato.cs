using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using QuantoDemoraApi.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace QuantoDemoraApi.Models
{
    [Table("Contatos")]
    [PrimaryKey((nameof(IdHospital)), (nameof(IdContato)), (nameof(IdTipoContato)))]
    public class Contato
    {
        public Hospital Hospital { get; set; }
        public int IdHospital { get; set; }
        public int IdContato { get; set; }
        [NotMapped]
        public TipoContato TipoContato { get; set; }
        public int IdTipoContato { get; set; }
        [NotMapped]
        public TipoContatoEnum TipoContatoEnum { get; set; }
        public string DsContato { get; set; }
        public string? InfoContato { get; set; }
    }
}