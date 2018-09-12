using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Veiculos.Ioc.Core.Data
{
    [Serializable]
    public class Veiculo : BaseEntity
    {
        public int AnoFabricacao { get; set; }
        public string Chassi { get; set; }
        public string Placa { get; set; }
        public byte[] Foto { get; set; }
        public int Cilindradas { get; set; }
        public int IdStatusVeiculo { get; set; }
        public int IdModelo { get; set; }
        [ForeignKey("IdModelo")]
        public virtual Modelo Modelo { get; set; }
    }
}
