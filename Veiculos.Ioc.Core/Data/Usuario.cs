using System;
using System.Collections.Generic;
using System.Text;

namespace Veiculos.Ioc.Core.Data
{
    public class Usuario //: BaseEntity
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
       /* public bool Ativo { get; set; }
        public bool Gerente { get; set; }*/
    }
}
