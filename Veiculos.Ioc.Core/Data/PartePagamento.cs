using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Veiculos.Ioc.Core.Data
{
    [Serializable]
    public class PartePagamento : BaseEntity
    {
        public decimal Quantia { get; set; }

        public int IdFormaPagamento { get; set; }
        [ForeignKey("IdFormaPagamento")]
        public virtual FormaPagamento FormaPagamento { get; set; }

        public int? IdVenda { get; set; }
        [ForeignKey("IdVenda")]
        public virtual Venda Venda { get; set; }

        public int? IdCompra { get; set; }
        [ForeignKey("IdCompra")]
        public virtual Compra Compra { get; set; }
    }

}
