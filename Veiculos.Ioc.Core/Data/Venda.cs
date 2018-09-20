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
        public decimal Comissao { get; set; }
        public string Obs { get; set; }
        public decimal ValorTotal { get; set; }

        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }

        public int IdVeiculo { get; set; }
        [ForeignKey("IdVeiculo")]
        public virtual Veiculo Veiculo { get; set; }

    }

}
