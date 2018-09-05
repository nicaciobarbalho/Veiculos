using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veiculos.Web.Models
{
    public class BaseVeiculosModel
    {
        [Key]
        public int Id { get; set; }
    }
}