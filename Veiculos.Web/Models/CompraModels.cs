using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veiculos.Web.Models
{
    public class CompraModel : BaseVeiculosModel
    {
        [Required(ErrorMessage = "Informe a data!")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Informe um preço!")]
        [RegularExpression(@"^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$", ErrorMessage = "Preço inválido! Máximo dois pontos decimais.")]
        [Range(1, 9999999999999999.99, ErrorMessage = "Preço inválido! Máximo de 18 dígitos.")]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        [Display(Name = "Forma de pagamento")]
        public int IdFormaPagamento { get; set; }

        [Display(Name = "Observação")]
        public string Obs { get; set; }

        public VeiculoModel Veiculo { get; set; }
    }
}