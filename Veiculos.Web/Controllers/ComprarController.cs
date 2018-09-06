using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult RegistrarCompra(int idVeiculo, Models.CompraModel comprar)
        {
            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Veiculo> serviceVeiculo = new Ioc.Service.Service<Ioc.Core.Data.Veiculo>();

            Ioc.Core.Data.Veiculo v = serviceVeiculo.Buscar(m => m.Id == idVeiculo);

            //   Models.CompraModel comprar = TempData["Comprar"] as Models.CompraModel;
            // Models.VeiculoModel veiculo = TempData["Veiculo"] as Models.VeiculoModel;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        // public ActionResult PesquisarVeiculo(Veiculos.Web.Models.VeiculoModel veiculo)
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


            if (veiculo?.Id == 0)
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

              var   c = new Models.CompraModel()
              {
                  Veiculo = v
              };

            TempData["Veiculo"] = v;
            TempData["Comprar"] = c;
           
            return View("RegistroVenda");
                   
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
