using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using Veiculos.Ioc.Core;
using Veiculos.Ioc.Data.Mapping;

namespace Veiculos.Ioc.Data
{
    public class IocDbContext : DbContext, IDbContext
    {
        // "Server=DESKTOP-ORNM0B4;Database=Veiculos;User ID=sa;Password=athenas;Trusted_Connection=False;Packet Size=4096"
        public IocDbContext()
           : base(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nicac\Desktop\IFRN\Veiculos.Web\Veiculos.Test\Veiculos.mdf;Integrated Security=True;Connect Timeout=30")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Map();
           
            base.OnModelCreating(modelBuilder);
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }
    }
}