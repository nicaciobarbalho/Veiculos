using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veiculos.Web.Models
{
    public class VeiculoModel : BaseVeiculosModel
    {
      
        [Required(ErrorMessage = "Informe a placa do veículo!")]
        [RegularExpression(@"^[a-zA-Z]{3}$", ErrorMessage = "Informe uma placa válida!")]
        [Display(Name = "Placa")]
        public string Placa { get; set; }

        [Required]
        [Display(Name = "Ano")]
        public int Ano { get; set; }

        [Required]
        [Display(Name = "Cilindradas")]
        public int Cilindradas { get; set; }

        [Required]
        [Display(Name = "Chassi")]
        public string Chassi { get; set; }

        public ModeloModel Modelo { get; set; }
    }
}