using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Veiculos.Ioc.Core.Data
{
    [Table("FormaPagamento")]
    public class FormaPagamento : BaseEntity
    {
        public string Descricao { get; set; }
    }

}
