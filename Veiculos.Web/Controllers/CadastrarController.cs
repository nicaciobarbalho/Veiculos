using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veiculos.Web.Extensions.Alerts;

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

            ViewBag.IdModelo =  new SelectList
                (
                    service.BuscarTodos(),
                    "Id",
                    "Descricao"                   
                );
        }

        private void CarregaComboFabricante()
        {
            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Fabricante> service = new Ioc.Service.Service<Ioc.Core.Data.Fabricante>();

            ViewBag.IdFabricante =  new SelectList
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
        public ActionResult Veiculo(Veiculos.Web.Models.VeiculoModel veiculo)
        {
            if (!ModelState.IsValid)
            {                
                this.CarregaComboModelo();
                this.CarregaComboFabricante();
                return View("Veiculo", veiculo);
            }

            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Veiculo> serviceVeiculo = new Ioc.Service.Service<Ioc.Core.Data.Veiculo>();

            serviceVeiculo.Inserir(new Ioc.Core.Data.Veiculo()
            {
                AnoFabricacao = veiculo.Ano,
                Chassi = veiculo.Chassi,
                Cilindradas = veiculo.Cilindradas,
                IdModelo = veiculo.IdModelo,
                IdStatusVeiculo = 1,
                Placa = veiculo.Placa
            });
            return RedirectToAction("Veiculo").WithSuccess("Compra salva com sucesso!");
        }
    }
}