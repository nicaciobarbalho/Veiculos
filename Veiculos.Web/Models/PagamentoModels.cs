using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veiculos.Web.Models
{
    public class PagamentoModel : BaseVeiculosModel
    {
        public int IdFormaPagamento { get; set; }
        public decimal Quantia { get; set; }
    }

    public class FormaPagamentoModel : BaseVeiculosModel
    {
        [Required(ErrorMessage = "Informe uma descrição!")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }

    public class PartePagamentoModel : BaseVeiculosModel
    {
        [Required(ErrorMessage = "Informe um valor!")]
        [RegularExpression(@"^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$", ErrorMessage = "Preço inválido! Máximo dois pontos decimais.")]
        [Range(1, 9999999999999999.99, ErrorMessage = "Preço inválido! Máximo de 18 dígitos.")]
        [Display(Name = "Valor")]
        public decimal Quantia { get; set; }

    }
}