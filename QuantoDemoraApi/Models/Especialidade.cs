using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using QuantoDemoraApi.Models.Enums;

namespace QuantoDemoraApi.Models
{
    [Table("Especialidades")]
    public class Especialidade
    {
        [Key]
        public int IdEspecialidade { get; set; }
        public string DsEspecialidade { get; set; }
    }
}