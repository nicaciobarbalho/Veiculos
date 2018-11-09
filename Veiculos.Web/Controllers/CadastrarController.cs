using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veiculos.Web.Extensions.Alerts;

namespace Veiculos.Web.Controllers
{
    [Authorize]
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
        public ActionResult Veiculo(Veiculos.Web.Models.VeiculoModel veiculo, string origemFormulario)
        {
            if (!ModelState.IsValid)
            {                
                this.CarregaComboModelo();
                this.CarregaComboFabricante();
                return View("Veiculo", veiculo);
            }

            var serviceVeiculo = new Ioc.Service.Service<Ioc.Core.Data.Veiculo>();

            var v = serviceVeiculo.Buscar(f => f.Placa == veiculo.Placa);

            if (v != null)
            {
                this.CarregaComboModelo();
                this.CarregaComboFabricante();
                
                return View("Veiculo", veiculo).WithInfo("O veículo está cadastrado no sistema!");
            }

            serviceVeiculo.Inserir(new Ioc.Core.Data.Veiculo()
            {
                AnoFabricacao = veiculo.Ano,
                Chassi = veiculo.Chassi,
                Cilindradas = veiculo.Cilindradas,
                IdModelo = veiculo.IdModelo,
                IdStatusVeiculo = 3,
                Placa = veiculo.Placa,
                Foto = ImagemParaByte(veiculo.Imagem)
            });

            if (!string.IsNullOrEmpty(origemFormulario))
            {
                var comprarController = DependencyResolver.Current.GetService<ComprarController>();
                comprarController.ControllerContext = new ControllerContext(this.Request.RequestContext, comprarController);

                var result = comprarController.PesquisarVeiculo(veiculo.Placa, "../Comprar");

                return result;
            }
            else
            {
               return RedirectToAction("Veiculo").WithSuccess("Compra salva com sucesso!");
            }
        }

        [HttpPost]
        public ActionResult BuscarVeiculoPorPlaca(string placa, string origem = null)
        {
            this.CarregaComboModelo();
            this.CarregaComboFabricante();

            ViewBag.OrigemFormulario = origem;
            Models.VeiculoModel veiculo = new Models.VeiculoModel();
            veiculo.Placa = placa;
            return View("../Cadastrar/Veiculo", veiculo);
        }

        private string SaveToPhysicalLocation(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/Veiculos"), fileName);
                file.SaveAs(path);
                return path;
            }
            return string.Empty;
        }

        private byte[] ImagemParaByte(HttpPostedFileBase imagem)
        {
            if (imagem != null)
            {
                BinaryReader b = new BinaryReader(imagem.InputStream);
                byte[] binData = b.ReadBytes(imagem.ContentLength);
                return binData;
            }
            return null;         
        }
    }
}