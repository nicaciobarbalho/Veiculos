using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veiculos.Web.Models
{
    public class FabricanteModel : BaseVeiculosModel
    {
        [Required(ErrorMessage = "Informe a descrição!")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}