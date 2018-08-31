using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Veiculos.Ioc.Service.Interface;
using System.Data.Entity;

namespace Veiculos.Test
{
    [TestClass]
    public class UnitUsuarioTest
    {
        [TestMethod]
        public void TestDeleteUsuario()
        {
            Ioc.Core.Data.Usuario usuario = new Ioc.Core.Data.Usuario() { Id = 2 };
            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Usuario> Service = new Ioc.Service.Service<Ioc.Core.Data.Usuario>();
            bool compare = (Service.Apagar(usuario) > 0);
            // assert  
            Assert.IsTrue(compare);
        }

        [TestMethod]
        public void TestRecuperarUsuario()
        {
            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Usuario> service = new Ioc.Service.Service<Ioc.Core.Data.Usuario>();

            var usuario = service.Buscar(1);                         
            Assert.IsNotNull(usuario);
        }

        //[TestMethod]
        //public void TestInsertUsuario()
        //{
        //    Ioc.Core.Data.Usuario usuario = new Ioc.Core.Data.Usuario() { Ativo = false, Cpf = "010.536.094-50", Gerente = false, Login = "Costa", Nome = "Barbalho", Senha = "4321", Telefone = "(55)3272-0872" };

        //    Veiculos.Ioc.Service.Service<Ioc.Core.Data.Usuario> service = new Ioc.Service.Service<Ioc.Core.Data.Usuario>();
        //    usuario = service.Inserir(usuario);
        //    bool compare = (usuario != null && usuario.Id > 0);
        //    // assert              
        //    Assert.IsTrue(compare);
        //}

        [TestMethod]
        public void TestUpdateUsuario()
        {
            Ioc.Core.Data.Usuario usuario = new Ioc.Core.Data.Usuario() { Id = 3, Ativo = false, Cpf = "010.536.094-50", Gerente = false, Login = "Arijane", Nome = "Peixoto", Senha = "987654321", Telefone = null };

            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Usuario> service = new Ioc.Service.Service<Ioc.Core.Data.Usuario>();

            bool compare = (service.Atualizar(usuario) > 0);
            // assert  
            Assert.IsTrue(compare);
        }
    }
}
