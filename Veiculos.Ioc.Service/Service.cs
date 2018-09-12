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

        public int Apagar(T usuario)
        {
            return this.repositorioGenerico.Delete(usuario);
        }

        public int Atualizar(T usuario)
        {
            return this.repositorioGenerico.Edit(usuario);
        }

        public T Inserir(T usuario)
        {
            return this.repositorioGenerico.Insert(usuario);
        }

        public IQueryable<T> BuscarTodos()
        {
            return this.repositorioGenerico.FindAll();
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
