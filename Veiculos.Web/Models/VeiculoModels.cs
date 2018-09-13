using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Veiculos.Web.Models
{
    public class VeiculoModel : BaseVeiculosModel
    {      
        [Required(ErrorMessage = "Informe a placa do veículo!")]
        [RegularExpression(@"^([a-zA-Z]{3}-[0-9]{4})$", ErrorMessage = "Placa do veículo informada está em formato incorreto!")]
        [Display(Name = "Placa")]
        public string Placa { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        [Display(Name = "Ano")]
        public Int32 Ano { get; set; }

        [Required]
        [Display(Name = "Cilindradas")]
        public Int32 Cilindradas { get; set; }

        [Required]
        [Display(Name = "Chassi")]
        public string Chassi { get; set; }

        [Display(Name = "Foto")]
        public HttpPostedFileBase Imagem { get; set; }
        
        public int IdModelo { get; set; }
        public string DescricaoModelo { get; set; }
        public int IdFabricante { get; set; }
        public string DescricaoFabricante { get; set; }
    }

    public class ModeloModel : BaseVeiculosModel
    {
        [Required(ErrorMessage = "Informe a descrição!")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }     
    }

    public class FabricanteModel : BaseVeiculosModel
    {
        [Required(ErrorMessage = "Informe a descrição!")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }

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
    }
}