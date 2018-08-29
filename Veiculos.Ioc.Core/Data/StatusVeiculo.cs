using System;
using System.Collections.Generic;
using System.Text;

namespace Veiculos.Ioc.Core.Data
{
    [Serializable]
    public class StatusVeiculo : BaseEntity
    {
        public string Descricao { get; set; }
    }

}
