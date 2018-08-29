using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Veiculos.Ioc.Service.Interface;
using Veiculos.Ioc.Data;

namespace Veiculos.Test
{
    [TestClass]
    public class UnitUsuarioTest
    {
        [TestMethod]
        public void TestRecuperarUsuario()
        {
            Veiculos.Ioc.Service.ServiceUsuario serviceUsuario = new Ioc.Service.ServiceUsuario();
            Ioc.Core.Data.Usuario usuario = serviceUsuario.Usuario(1);
            bool result = (usuario != null && usuario.Id > 0);
            // assert  
            Assert.AreEqual(true, result);
        }
    }
}
