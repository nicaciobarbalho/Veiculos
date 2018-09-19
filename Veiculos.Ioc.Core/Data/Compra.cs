using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Veiculos.Ioc.Core.Data
{
    [Table("Compra")]
    public class Compra : BaseEntity
    {
        public int IdVeiculo { get; set; }
        [ForeignKey("IdVeiculo")]
        public virtual Veiculo Veiculo { get; set; }

        public DateTime Data { get; set; }
        public decimal Preco { get; set; }
        public string Obs { get; set; }
    }
}
