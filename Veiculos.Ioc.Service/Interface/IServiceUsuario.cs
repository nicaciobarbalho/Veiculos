using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veiculos.Ioc.Core.Data;

namespace Veiculos.Ioc.Service.Interface
{
    public interface IServiceUsuario
    {
        IQueryable<Core.Data.Usuario> Usuarios();
        Core.Data.Usuario Usuario(long id);
        int InserirUsario(Core.Data.Usuario usuario);
        int AtualizarUsario(Core.Data.Usuario usuario);
        int ApagarUsario(Core.Data.Usuario usuario);
    }
}
