using System;
using System.Collections.Generic;
using System.Text;

namespace Veiculos.Ioc.Core.Data
{
    [Serializable]
    public class TipoVeiculo : BaseEntity
    {
        public string Descricao { get; set; }
    }
}
