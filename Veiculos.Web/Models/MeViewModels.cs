using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Veiculos.Web.Models
{
    // Modelos retornados por ações MeController.
    public class GetViewModel
    {
        public string Hometown { get; set; }
    }
}