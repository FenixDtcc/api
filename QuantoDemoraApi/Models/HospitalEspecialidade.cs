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
    [Table("HospitalEspecialidades")]
    [PrimaryKey((nameof(IdHospital)), (nameof(IdEspecialidade)))]
    public class HospitalEspecialidade
    {
        public Hospital Hospital { get; set; }
        public int IdHospital { get; set; }
        public Especialidade Especialidade { get; set; }
        public int IdEspecialidade { get; set; }
        [NotMapped]
        public EspecialidadeEnum EspecialidadeEnum { get; set; }
    }
}