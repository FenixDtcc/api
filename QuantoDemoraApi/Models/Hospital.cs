using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using QuantoDemoraApi.Models.Enums;

namespace QuantoDemoraApi.Models
{
    [Table("Hospital")]
    public class Hospital
    {
        [Key]
        public int IdHospital { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        [NotMapped]
        public Logradouro Logradouro { get; set; }
        public int IdLogradouro { get; set; }
        [NotMapped]
        public LogradouroEnum LogradouroEnum { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string IdGoogleMaps { get; set; }
        public List<HospitalEspecialidade> HospitalEspecialidades { get; set; }
    }
}