using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using QuantoDemoraApi.Models.Enums;

namespace QuantoDemoraApi.Models
{
    public class AtendimentoEvento
    {
        public Atendimento Atendimento { get; set; }
        [Key]
        public int IdAtendimento { get; set; }
        public Evento Evento { get; set; }
        //[Key]
        public int IdEvento { get; set; }
        public EventoEnum EventoEnum { get; set; }
        //[Key]
        public char AcAtendimento { get; set; }
        public DateTime MtAtendimento { get; set; }
    }
}