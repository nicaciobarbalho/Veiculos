using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Veiculos.Ioc.Core.Data
{
    [Serializable]
    public class Venda : BaseEntity
    {
        public DateTime Data { get; set; }
        public decimal Desconto { get; set; }
        public int IdStatusVenda { get; set; }
        public string Comissao { get; set; }
        public string Obs { get; set; }
        public decimal ValorTotal { get; set; }
        public int IdUsuario { get; set; }
        [ForeignKey("IdFormaPagamento")]
        public virtual FormaPagamento FormaPagamento { get; set; }
    }

}
