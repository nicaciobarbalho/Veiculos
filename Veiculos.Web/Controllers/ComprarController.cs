﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veiculos.Web.Extensions;
using Veiculos.Web.Extensions.Alerts;

namespace Veiculos.Web.Controllers
{
    [Authorize]
    public class ComprarController : Controller
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

        // GET: Venda/Comprar    
        public ActionResult Index()
        {
            this.FormaPagamento();

            Models.CompraModel comprar = new Models.CompraModel();

            Models.VeiculoModel veiculo = (Models.VeiculoModel)Session["Veiculo"];
            comprar.Veiculo = veiculo;

            return View(comprar);
        }

        // GET: Vender
        public ActionResult Home()
        {
            ViewBag.Controlador = "Comprar";
            ViewBag.Acao = "Index";

            return View("../Veiculo/Placa");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarCompra(int idVeiculo, string placaVeiculo, Models.CompraModel model)
        {
            var Veiculo = Session["Veiculo"] as Models.VeiculoModel;
    
            if (!ModelState.IsValid || model.Id == -1)
            {
                if (model.Id == -1) ModelState.AddModelError("", "Erro ao salvar!");
                if (model.Preco <= 0) ModelState.AddModelError("", "Informe um preço!");

                this.FormaPagamento();

                return View(model);
            }

            Ioc.Core.Data.Compra compra = new Ioc.Core.Data.Compra()
            {
                Data = model.Data,
                Preco = model.Preco,
                IdVeiculo = idVeiculo,
                Obs = model.Obs,
            };

            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Compra> servicoCompra = new Ioc.Service.Service<Ioc.Core.Data.Compra>();
            compra = servicoCompra.Inserir(compra);

            if (compra.Id > 0)
            {
                Ioc.Core.Data.PartePagamento pp = new Ioc.Core.Data.PartePagamento()
                {
                    Quantia = model.Preco,
                    IdFormaPagamento = model.IdFormaPagamento,
                    IdCompra = compra.Id
                };

                Veiculos.Ioc.Service.Service<Ioc.Core.Data.PartePagamento> servicoPP = new Ioc.Service.Service<Ioc.Core.Data.PartePagamento>();
                servicoPP.Inserir(pp);

                StatusAtualizacao.VeiculoAtualizar(new Ioc.Core.Data.Veiculo() { Id = compra.IdVeiculo }, StatusAtualizacao.StatusVeiculo.DisponivelParaVenda);
                return RedirectToAction("Home").WithSuccess("Compra salva com sucesso!");
            }
            else
            {
                model.Id = -1;
                return this.RegistrarCompra(idVeiculo, placaVeiculo, model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PesquisarVeiculo(string placa, string controlador = null)
        {                     
            if (string.IsNullOrWhiteSpace(placa))
            {
                ModelState.AddModelError(string.Empty, "Informe a placa do veículo!");
                return  controlador == null ? View("Index") : View($"{controlador}/Index");
            }
            else if (!Veiculos.Util.Validacao.EPlacaValidar(placa))
            {
                ModelState.AddModelError(string.Empty, "Placa do veículo informada está em formato incorreto!");
                return controlador == null ? View("Index") : View($"{controlador}/Index");
            }

            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Veiculo> serviceVeiculo = new Ioc.Service.Service<Ioc.Core.Data.Veiculo>();

            Ioc.Core.Data.Veiculo veiculo  = serviceVeiculo.Buscar(m => m.Placa == placa);

            if (veiculo == null || veiculo.Id == 0)
            {
                var cadastrarController = DependencyResolver.Current.GetService<CadastrarController>();
                cadastrarController.ControllerContext = new ControllerContext(this.Request.RequestContext, cadastrarController);

                var result = cadastrarController.BuscarVeiculoPorPlaca(placa, "Comprar");

                return result;
            }
    
            var v = new Models.VeiculoModel()
            {
                Id = veiculo.Id,
                Ano = veiculo.AnoFabricacao,
                Chassi = veiculo.Chassi,
                Cilindradas = veiculo.Cilindradas,
                Placa = veiculo.Placa,
                IdModelo = veiculo.Modelo.Id,
                DescricaoModelo = veiculo.Modelo.Descricao,
                IdFabricante = veiculo.Modelo.Fabricante.Id,
                DescricaoFabricante = veiculo.Modelo.Fabricante.Descricao
            };

          
            Models.CompraModel compra = new Models.CompraModel();
            compra.Veiculo = v;

            this.FormaPagamento();

            Session.Remove("Veiculo");
            Session["Veiculo"] = v;
            return controlador == null ? View() : View($"{controlador}/Index", compra);               
        }

        [HttpPost]
        public JsonResult Placa(string placa, string veiculo)
        {
            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Compra> service = new Ioc.Service.Service<Ioc.Core.Data.Compra>();

            var result = (from c in service.BuscarTodos(g => g.Veiculo.IdStatusVeiculo == 3 && (g.Veiculo.Placa.StartsWith(placa) && !g.Veiculo.Placa.Equals(veiculo)))
                          select new
                          {
                              Id = c.Veiculo.Id,
                              Veiculo = c.Veiculo.Placa + "/" + c.Veiculo.Modelo.Descricao + "/" + c.Veiculo.Modelo.Fabricante.Descricao + "/" + c.Veiculo.AnoFabricacao.ToString(),
                              c.Preco,
                              IdCompra = c.Id
                          }
                );

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Venda/Comprar/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Venda/Comprar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Venda/Comprar/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Venda/Comprar/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Venda/Comprar/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Venda/Comprar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Venda/Comprar/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
