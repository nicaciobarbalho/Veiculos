using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veiculos.Web.Extensions;
using Veiculos.Web.Extensions.Alerts;

namespace Veiculos.Web.Controllers
{
    public class VeiculoController : Controller
    {
        // GET: Veiculo
        public ActionResult Index()
        {
            Session.Remove("Veiculo");
            return View();
        }
        [HttpPost]
        public ActionResult Index(string controlador, string acao)
        {
            Session.Remove("Veiculo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pesquisar(string placa, string controlador, string acao)
        {
            if (string.IsNullOrWhiteSpace(placa))
            {
                ModelState.AddModelError(string.Empty, "Informe a placa do veículo!");
                return View("Placa");
            }
            else if (!Veiculos.Util.Validacao.EPlacaValidar(placa))
            {
                ModelState.AddModelError(string.Empty, "Placa do veículo informada está em formato incorreto!");
                return View("Placa");
            }

            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Veiculo> service = new Ioc.Service.Service<Ioc.Core.Data.Veiculo>();

            Ioc.Core.Data.Veiculo veiculo = veiculo = service.Buscar(m => m.Placa == placa); 
                           
            if (veiculo == null || veiculo.Id == 0)
            {
                var cadastrarController = DependencyResolver.Current.GetService<CadastrarController>();
                cadastrarController.ControllerContext = new ControllerContext(this.Request.RequestContext, cadastrarController);

                var result = cadastrarController.BuscarVeiculoPorPlaca(placa, controlador);

                return result;
            }
            //Registrar Compra: Fluxo Alternativo (2): o veículo está cadastrado no sistema e está com status diferente de NÃO PERTENCE À LOJA.
            else if (veiculo != null && veiculo.Id > 0 && controlador.Equals("Comprar") && veiculo.IdStatusVeiculo != 3)
            {
                return View("Placa").WithInfo("Operação não é permitida, pois o veículo já pertence à loja!");
            }
            //Registrar venda: Fluxo Alternativo (2): veículo não está cadastrado no sistema ou seu status é NÃO PERTENCE À LOJA.
            else if (veiculo != null && veiculo.Id > 0 && controlador.Equals("Vender") && veiculo.IdStatusVeiculo == 3)
            {
                return View("Placa").WithInfo("Operação não é permitida, pois o veículo não pertence à loja!");
            }
            //Registrar venda: Fluxo Alternativo (2): veículo está com status EM PROCESSO DE VENDA.
            else if (veiculo != null && veiculo.Id > 0 && controlador.Equals("Vender") && veiculo.IdStatusVeiculo == 2)
            {
                return View("Placa").WithInfo("Veículo está sendo negociado em outra transação!");
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
                DescricaoFabricante = veiculo.Modelo.Fabricante.Descricao,
               Imagem = (HttpPostedFileBase)new MemoryPostedFile(veiculo.Foto)
        };
  
            Session.Remove("Veiculo");
            Session["Veiculo"] = v;
            return RedirectToAction(acao, controlador);
        }
    }
}