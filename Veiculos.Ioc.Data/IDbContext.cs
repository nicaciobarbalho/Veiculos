using System.Data.Entity;
using Veiculos.Ioc.Core;

namespace Veiculos.Ioc.Data
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        int SaveChanges();
    }
}
