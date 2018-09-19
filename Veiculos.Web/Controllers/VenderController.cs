using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarVenda(Models.VendaModel model)
        {
            if (!ModelState.IsValid)
            {
                this.FormaPagamento();
         
                Models.VeiculoModel veiculo = (Models.VeiculoModel)Session["Veiculo"];
                model.Veiculo = veiculo;

                return View(model);
            }

            Ioc.Core.Data.Venda venda = new Ioc.Core.Data.Venda()
            {
                Data = model.Data,
                Comissao = model.Comissao,
                Desconto = model.Desconto,
                IdStatusVenda = 3,
                IdUsuario =  int.Parse(User.Identity.GetUserId().ToString()),
                ValorTotal = model.Pagamentos.Sum(f => f.Quantia),                
                Obs = model.Obs,
            };

            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Venda> servicoVenda = new Ioc.Service.Service<Ioc.Core.Data.Venda>();
            venda = servicoVenda.Inserir(venda);
            if (venda.Id > 0)
            {
                Veiculos.Ioc.Service.Service<Ioc.Core.Data.PartePagamento> servicoPP = new Ioc.Service.Service<Ioc.Core.Data.PartePagamento>();

                foreach (var pag in model.Pagamentos)
                {
                    Ioc.Core.Data.PartePagamento pp = new Ioc.Core.Data.PartePagamento()
                    {
                        Quantia = pag.Quantia,
                        IdFormaPagamento = pag.IdFormaPagamento,
                        IdVenda = venda.Id,
                        IdCompra = pag.IdCompra
                    };
                                       
                    servicoPP.Inserir(pp);
                }
            }

                return View();
        }
    }
}