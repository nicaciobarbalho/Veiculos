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
        private IRepository<T> usuarioRepository;

        public Service(IRepository<T> usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public Service()
        {
            Repository<T> repository = new Repository<T>();
            
            this.usuarioRepository = repository;
        }

        public int Apagar(T usuario)
        {
            return this.usuarioRepository.Delete(usuario);
        }

        public int Atualizar(T usuario)
        {
            return this.usuarioRepository.Edit(usuario);
        }

        public T Inserir(T usuario)
        {
            return this.usuarioRepository.Insert(usuario);
        }

        public IQueryable<T> BuscarTodos()
        {
            return this.usuarioRepository.FindAll();
        }

        public T Buscar(int id)
        {
            return this.usuarioRepository.FindById(id);
        }

        public T Buscar(System.Linq.Expressions.Expression<Func<T, bool>> where = null)
        {
            return this.usuarioRepository.FindOne(where);
        }

    }
}
