using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veiculos.Ioc.Core.Data;
using Veiculos.Ioc.Data;
using Veiculos.Ioc.Service.Interface;

namespace Veiculos.Ioc.Service
{
    public class Service<T> : IService<T> where T : class, new()
    {
        private IRepository<T> repositorioGenerico;

        public Service(IRepository<T> usuarioRepository)
        {
            this.repositorioGenerico = usuarioRepository;
        }

        public Service()
        {
            Repository<T> repository = new Repository<T>();
            
            this.repositorioGenerico = repository;
        }

        public int Apagar(T modelo)
        {
            return this.repositorioGenerico.Delete(modelo);
        }

        public int Atualizar(T modelo)
        {
            return this.repositorioGenerico.Edit(modelo);
        }

        public T Inserir(T modelo)
        {
            return this.repositorioGenerico.Insert(modelo);
        }

        public IQueryable<T> BuscarTodos(System.Linq.Expressions.Expression<Func<T, bool>> where = null)
        {
            return this.repositorioGenerico.FindAll(where);
        }

        public T Buscar(int id)
        {
            return this.repositorioGenerico.FindById(id);
        }

        public T Buscar(System.Linq.Expressions.Expression<Func<T, bool>> where = null)
        {
            return this.repositorioGenerico.FindOne(where);
        }

    }
}
