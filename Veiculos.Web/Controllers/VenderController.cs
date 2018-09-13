using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Veiculos.Web.Controllers
{
    public class VenderController : Controller
    {
        private void FormaPagamento()
        {
            Veiculos.Ioc.Service.Service<Ioc.Core.Data.FormaPagamento> service = new Ioc.Service.Service<Ioc.Core.Data.FormaPagamento>();

            ViewBag.IdFormaPagamento = new SelectList
                   (
                       service.BuscarTodos(),
                       "Id",
                       "Descricao",
                       1
                   );
        }

        // GET: Vender
        public ActionResult Index()
        {
           this.FormaPagamento();

            Models.VendaModel venda = new Models.VendaModel();

            Models.VeiculoModel veiculo = (Models.VeiculoModel)Session["Veiculo"];
            venda.Veiculo = veiculo;

            return View(venda);
        }

        // GET: Vender
        public ActionResult Home()
        {
            ViewBag.Controlador = "Vender";
            ViewBag.Acao = "Index";

            return View("../Veiculo/Placa");
        }
    }
}