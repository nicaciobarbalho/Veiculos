using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Veiculos.Ioc.Core.Data
{
    [Serializable]
    public class Modelo : BaseEntity
    {
        public string Descricao { get; set; }
        public int IdTipoVeiculo { get; set; }
        public int IdFabricante { get; set; }
        [ForeignKey("IdFabricante")]
        public virtual Fabricante Fabricante { get; set; }
    }
}
