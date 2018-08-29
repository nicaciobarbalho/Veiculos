using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Veiculos.Ioc.Core.Data
{
    public class Usuario //: BaseEntity
    {
        [Key]
        public Int32 Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cpf { get; set; }

        public string Telefone { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public bool Ativo { get; set; }
        [Required]
        public bool Gerente { get; set; }
    }
}
