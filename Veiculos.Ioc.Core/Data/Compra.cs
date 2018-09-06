using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Veiculos.Ioc.Core.Data
{
    [Serializable]
    public class Compra : BaseEntity
    {
        public int IdVeiculo { get; set; }
        public DateTime Data { get; set; }
        public decimal Preco { get; set; }
        public string Obs { get; set; }
        public string IdFormaPagamento { get; set; }
        [ForeignKey("IdFormaPagamento")]
        public virtual FormaPagamento FormaPagamento { get; set; }
    }
}
