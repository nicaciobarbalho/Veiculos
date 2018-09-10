using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Veiculos.Web.Controllers
{
    public class CadastrarController : Controller
    {
        // GET: Cadastrar
        public ActionResult Index()
        {
            return View();
        }

        private void CarregaComboModelo()
        {
            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Modelo> service = new Ioc.Service.Service<Ioc.Core.Data.Modelo>();

            ViewBag.IdModelo = new SelectList
                (
                    service.BuscarTodos(),
                    "Id",
                    "Descricao"                   
                );
        }

        private void CarregaComboFabricante()
        {
            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Fabricante> service = new Ioc.Service.Service<Ioc.Core.Data.Fabricante>();

            ViewBag.IdFabricante = new SelectList
                (
                    service.BuscarTodos(),
                    "Id",
                    "Descricao"                    
                );
        }

        public ActionResult Veiculo()
        {
            this.CarregaComboModelo();
            this.CarregaComboFabricante();

            return View();
        }
        [HttpPost]
        public ActionResult Veiculo(string placa)
        {
            return View();
        }
    }
}