using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using Veiculos.Web.Extensions.Alerts;

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
            foreach(var erro in ModelState.Where(e => e.Key.StartsWith("Veiculo")))
            {
                erro.Value.Errors.Clear();
            }

            if (!ModelState.IsValid)
            {
                this.FormaPagamento();
         
                Models.VeiculoModel veiculo = (Models.VeiculoModel)Session["Veiculo"];
                model.Veiculo = veiculo;

                return View("Index", model);
            }
            
            Ioc.Core.Data.Venda venda = new Ioc.Core.Data.Venda()
            {
                Data = model.Data,
                Comissao = model.Comissao,
                Desconto = model.Desconto,
                IdStatusVenda = 3,
                IdUsuario =  int.Parse(User.Identity.GetUserId().ToString()),
                ValorTotal = model.Pagamentos.Sum(f => f.Quantia),
                IdVeiculo = model.Veiculo.Id,           
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
                        IdVenda = venda.Id                        
                    };

                    if(pag.IdCompra > 0)
                        pp.IdCompra = pag.IdCompra;

                    servicoPP.Inserir(pp);
                }

                if (User.IsInRole("Gerente"))
                {
                    StatusAtualizacao.VeiculoAtualizar(new Ioc.Core.Data.Veiculo() { Id = model.Veiculo.Id }, StatusAtualizacao.StatusVeiculo.NaoPertenceLoja);

                    foreach (var pag in model.Pagamentos.Where(f => f.IdCompra > 0))
                    {
                        Veiculos.Ioc.Service.Service<Ioc.Core.Data.Compra> servicoCompra = new Ioc.Service.Service<Ioc.Core.Data.Compra>();

                        var compra = servicoCompra.Buscar(pag.IdCompra);

                        StatusAtualizacao.VeiculoAtualizar(new Ioc.Core.Data.Veiculo() { Id = compra.IdVeiculo }, StatusAtualizacao.StatusVeiculo.NaoPertenceLoja);
                    }

                    return RedirectToAction("Home").WithSuccess("Venda salva com sucesso!");
                }
                else
                {
                    StatusAtualizacao.VeiculoAtualizar(new Ioc.Core.Data.Veiculo() { Id = model.Veiculo.Id }, StatusAtualizacao.StatusVeiculo.EmProcessoVenda);

                    foreach (var pag in model.Pagamentos.Where(f => f.IdCompra > 0))
                    {
                        Veiculos.Ioc.Service.Service<Ioc.Core.Data.Compra> servicoCompra = new Ioc.Service.Service<Ioc.Core.Data.Compra>();

                        var compra = servicoCompra.Buscar(pag.IdCompra);

                        StatusAtualizacao.VeiculoAtualizar(new Ioc.Core.Data.Veiculo() { Id = compra.IdVeiculo }, StatusAtualizacao.StatusVeiculo.EmProcessoVenda);
                    }

                    return RedirectToAction("Home").WithInfo("Aguardando autorização do gerente.");
                }
            }

            return View("Index", model);
        }
    }
}