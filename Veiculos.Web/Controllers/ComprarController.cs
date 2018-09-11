using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veiculos.Web.Extensions.Alerts;

namespace Veiculos.Web.Controllers
{
    [Authorize]
    public class ComprarController : Controller
    {
        // GET: Venda/Comprar    
        public ActionResult Index()
        {         
          return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarCompra(int idVeiculo, string placaVeiculo, Models.CompraModel comprar)
        {
            var Veiculo = Session["Veiculo"] as Models.VeiculoModel;
            Veiculos.Ioc.Service.Service<Ioc.Core.Data.FormaPagamento> serviceFP = new Ioc.Service.Service<Ioc.Core.Data.FormaPagamento>();

            if (!ModelState.IsValid)
            {
                if (comprar.Id == -1) ModelState.AddModelError("", "Erro ao salvar!");
                if (comprar.Preco <= 0) ModelState.AddModelError("", "Informe um preço!");
                ViewBag.Veiculo = Veiculo;
                ViewBag.Comprar = comprar;

                ViewBag.IdFormaPagamento = new SelectList
                    (
                        serviceFP.BuscarTodos(),
                        "Id",
                        "Descricao",
                        1
                    );

                return View("RegistroCompra");
            }

            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Compra> serviceCompra = new Ioc.Service.Service<Ioc.Core.Data.Compra>();

            var c = serviceCompra.Inserir(new Ioc.Core.Data.Compra() { Data = comprar.Data, IdFormaPagamento = comprar.IdFormaPagamento, Preco = comprar.Preco, IdVeiculo = idVeiculo, Obs = comprar.Obs });


            if (c.Id > 0)
                return RedirectToAction("Index").WithSuccess("Compra salva com sucesso!");
            else
            {
                comprar.Id = -1;
                return this.RegistrarCompra(idVeiculo, placaVeiculo, comprar);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PesquisarVeiculo(string placa)
        {
         
            

            if (string.IsNullOrWhiteSpace(placa))
            {
                ModelState.AddModelError(string.Empty, "Informe a placa do veículo!");
                return View("Index");
            }
            else if (!Veiculos.Util.Validacao.EPlacaValidar(placa))
            {
                ModelState.AddModelError(string.Empty, "Placa do veículo informada está em formato incorreto!");
                return View("Index");
            }

            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Veiculo> serviceVeiculo = new Ioc.Service.Service<Ioc.Core.Data.Veiculo>();

            Ioc.Core.Data.Veiculo veiculo  = serviceVeiculo.Buscar(m => m.Placa == placa);


            if (veiculo == null || veiculo.Id == 0)
            {
                ModelState.AddModelError(string.Empty, "Placa do veículo informada não encontrada!");
                return View("Index");
            }

    
            var v = new Models.VeiculoModel()
            {
                Id = veiculo.Id,
                Ano = veiculo.AnoFabricacao,
                Chassi = veiculo.Chassi,
                Cilindradas = veiculo.Cilindradas,
                Placa = veiculo.Placa,
                Modelo = new Models.ModeloModel()
                {
                    Id = veiculo.Modelo.Id,
                    Descricao = veiculo.Modelo.Descricao,
                    Fabricante = new Models.FabricanteModel()
                    {
                        Descricao = veiculo.Modelo.Fabricante.Descricao,
                        Id = veiculo.Modelo.Id
                    }
                }
            };

            ViewBag.Veiculo = v;
            ViewBag.Comprar = new Models.CompraModel();

            Veiculos.Ioc.Service.Service<Ioc.Core.Data.FormaPagamento> serviceFP = new Ioc.Service.Service<Ioc.Core.Data.FormaPagamento>();

            ViewBag.IdFormaPagamento = new SelectList
                (
                    serviceFP.BuscarTodos(),
                    "Id",
                    "Descricao",
                    1
                );

            Session.Remove("Veiculo");
            Session["Veiculo"] = v;
            return View("RegistroCompra");               
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
