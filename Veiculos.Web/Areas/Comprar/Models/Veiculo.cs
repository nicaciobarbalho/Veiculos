using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veiculos.Web.Areas.Venda.Models
{
    public class Veiculo
    {
        [Required]
        [Display(Name = "Placa")]
        public string Placa { get; set; }

        [Required]
        [Display(Name = "Modelo")]
        public string Modelo { get; set; }

        [Required]
        [Display(Name = "Ano")]
        public int Ano { get; set; }

        [Required]
        [Display(Name = "Cilindradas")]
        public int Cilindradas { get; set; }

        [Required]
        [Display(Name = "Chassi")]
        public int Chassi { get; set; }
    }
}