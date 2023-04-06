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
    [Table("AtendimentosEventos")]
    [PrimaryKey((nameof(IdAtendimento)), (nameof(IdEvento)), (nameof(AcAtendimento)))]
    public class AtendimentoEvento
    {
        public Atendimento Atendimento { get; set; }
        public int IdAtendimento { get; set; }
        public Evento Evento { get; set; }
        public int IdEvento { get; set; }
        public EventoEnum EventoEnum { get; set; }
        public char AcAtendimento { get; set; }
        public DateTime MtAtendimento { get; set; }
    }
}