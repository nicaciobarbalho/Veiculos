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
    public class ServiceUsuario : IServiceUsuario
    {
        private IRepository<Core.Data.Usuario> usuarioRepository;

        public ServiceUsuario(IRepository<Core.Data.Usuario> usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public ServiceUsuario()
        {
            this.usuarioRepository = new Ioc.Data.Repositorios.RepositoryUsuario();
        }

        public int ApagarUsario(Core.Data.Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public int AtualizarUsario(Core.Data.Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public int InserirUsario(Core.Data.Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Core.Data.Usuario> Usuarios()
        {
            return this.usuarioRepository.FindAll();
        }

        public Core.Data.Usuario Usuario(long id)
        {
            return this.usuarioRepository.FindById(id);
        }
    }
}
