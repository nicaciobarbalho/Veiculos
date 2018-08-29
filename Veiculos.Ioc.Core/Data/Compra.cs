using System;
using System.Collections.Generic;
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
    }
}
