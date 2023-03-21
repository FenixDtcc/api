using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using QuantoDemoraApi.Models.Enums;

namespace QuantoDemoraApi.Models
{
    public class HospitalEspecialidade
    {
        public Hospital Hospital { get; set; }
        [Key]
        public int IdHospital { get; set; }
        public Especialidade Especialidade { get; set; }
        //[Key]
        public int IdEspecialidade { get; set; }
        public EspecialidadeEnum EspecialidadeEnum { get; set; }
    }
}