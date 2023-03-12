using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuantoDemoraApi.Models.Enums;

namespace QuantoDemoraApi.Models
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public string DsEvento { get; set; }
        public EventoEnum EventoEnum { get; set; }
    }
}