using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veiculos.Web.Extensions;

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
}