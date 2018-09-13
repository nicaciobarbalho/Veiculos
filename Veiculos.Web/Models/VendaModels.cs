using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veiculos.Web.Models
{
    public class VendaModel : BaseVeiculosModel
    {
        public VendaModel()
        {
            this.Pagamentos = new List<PagamentoModel>();
            this.Veiculo = new VeiculoModel();
        }

        [Required(ErrorMessage = "Informe a data!")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Data")]
        public DateTime Data { get; set; }
        
        [RegularExpression(@"^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$", ErrorMessage = "Desconto inválido! Máximo dois pontos decimais.")]
        [Range(1, 9999999999999999.99, ErrorMessage = "Preço inválido! Máximo de 18 dígitos.")]
        [Display(Name = "Desconto")]
        public decimal Desconto { get; set; }

        [RegularExpression(@"^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$", ErrorMessage = "Comissão inválido! Máximo dois pontos decimais.")]
        [Range(1, 9999999999999999.99, ErrorMessage = "Preço inválido! Máximo de 18 dígitos.")]
        [Display(Name = "Comissão")]
        public decimal Comissao { get; set; }
        
        [Display(Name = "Observação")]
        public string Obs { get; set; }
        public List<PagamentoModel> Pagamentos { get; set; }
        public VeiculoModel Veiculo { get; set; }

    }
}