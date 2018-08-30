using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veiculos.Ioc.Data.Mapping
{
    public static class ClassAllMap 
    {
        public static void Map(this DbModelBuilder modelBuilder)
        {           
            modelBuilder.Entity<Ioc.Core.Data.Compra>().ToTable("Compra");
            modelBuilder.Entity<Ioc.Core.Data.Fabricante>().ToTable("Fabricante");
            modelBuilder.Entity<Ioc.Core.Data.FormaPagamento>().ToTable("FormaPagamento");
            modelBuilder.Entity<Ioc.Core.Data.Modelo>().ToTable("Modelo");
            modelBuilder.Entity<Ioc.Core.Data.PartePagamento>().ToTable("PartePagamento");
            modelBuilder.Entity<Ioc.Core.Data.StatusVeiculo>().ToTable("StatusVeiculo");
            modelBuilder.Entity<Ioc.Core.Data.StatusVenda>().ToTable("StatusVenda");
            modelBuilder.Entity<Ioc.Core.Data.TipoVeiculo>().ToTable("TipoVeiculo");
            modelBuilder.Entity<Ioc.Core.Data.Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Ioc.Core.Data.Veiculo>().ToTable("Veiculo");
            modelBuilder.Entity<Ioc.Core.Data.Venda>().ToTable("Venda");            
        }
    }
}
