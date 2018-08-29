using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Veiculos.Ioc.Core.Data;

namespace Veiculos.Ioc.Data.Repositorios
{
    public class RepositoryUsuario : Repository<Ioc.Core.Data.Usuario>
    {        
        public RepositoryUsuario() { }
        public RepositoryUsuario(DbContext Context) : base(Context) { }        
    }
}
