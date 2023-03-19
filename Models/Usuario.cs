using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuantoDemoraApi.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public Associado Associado { get; set; }
        public int IdAssociado { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public DateTime? DtAcesso { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string TpUsuario { get; set; }
        public DateTime? DtCadastro { get; set; }

        [NotMapped]
        public string PasswordString { get; set; }
    }
}