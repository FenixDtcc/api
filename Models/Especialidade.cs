using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuantoDemoraApi.Models.Enums;

namespace QuantoDemoraApi.Models
{
    public class Especialidade
    {
        public int IdEspecialidade { get; set; }
        public string DsEspecialidade { get; set; }
        public EspecialidadeEnum EspecialidadeEnum { get; set; }
    }
}