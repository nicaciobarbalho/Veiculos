using System;
using System.Collections.Generic;
using System.Text;

namespace Veiculos.Ioc.Core.Data
{
    [Serializable]
    public class PartePagamento : BaseEntity
    {
        public decimal Quantia { get; set; }
        public int IdFormaPagamento { get; set; }
        public int IdVenda { get; set; }
        public int IdCompra { get; set; }
    }

}
