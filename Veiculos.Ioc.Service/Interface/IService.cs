using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veiculos.Ioc.Core.Data;

namespace Veiculos.Ioc.Service.Interface
{
    public interface IService<T> 
    {
        IQueryable<T> BuscarTodos();
        T Buscar(int id);
        T Buscar(System.Linq.Expressions.Expression<Func<T, bool>> where = null);
        T Inserir(T modelo);
        int Atualizar(T modelo);
        int Apagar(T modelo);
    }
}
