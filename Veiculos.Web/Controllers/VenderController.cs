﻿using System;
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
            foreach (var erro in ModelState.Where(e => e.Key.StartsWith("Veiculo")))
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
                IdUsuario = int.Parse(User.Identity.GetUserId().ToString()),
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

                    if (pag.IdCompra > 0)
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

        [HttpGet]
        public ActionResult Autorizar()
        {
            return View("Autorizar");
        }

        [HttpPost]
        public JsonResult CarregarAutorizacao()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            //Paging Size (10,20,50,100)  
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Venda> servico = new Ioc.Service.Service<Ioc.Core.Data.Venda>();

            var result = from v in servico.BuscarTodos(f => f.IdStatusVenda == 2)
                         
                         select new
                         {
                             v.Id,
                             v.Data,
                             v.Veiculo.Placa,
                             Fabricante = v.Veiculo.Modelo.Fabricante.Descricao,
                             Modelo = v.Veiculo.Modelo.Descricao,
                             Valor = v.ValorTotal
                         };

            //total number of rows count   
            recordsTotal = result.Count();
            //Paging   
            var data = result.OrderByDescending(o => o.Id).Skip(skip).Take(pageSize).ToList();
            //Returning Json Data  
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data });
        }

        public ActionResult AutorizarVenda(int id)
        {
            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Venda> servico = new Ioc.Service.Service<Ioc.Core.Data.Venda>();

            Ioc.Core.Data.Venda venda = servico.Buscar(id);
            if (venda != null && venda.Id > 0)
            {
                StatusAtualizacao.VendaAtualizar(venda, StatusAtualizacao.StatusVenda.Autorizada);
                StatusAtualizacao.VeiculoAtualizar(venda.Veiculo, StatusAtualizacao.StatusVeiculo.NaoPertenceLoja);

                return RedirectToAction("Autorizar").WithSuccess("Venda autorizada com sucesso!");
            }
            else
            {
                return RedirectToAction("Autorizar").WithInfo("Venda não encontrada!");
            }
        }

        public ActionResult ExcluirVenda(int id)
        {            
            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Venda> servico = new Ioc.Service.Service<Ioc.Core.Data.Venda>();

            Ioc.Core.Data.Venda venda = servico.Buscar(id);

            if (venda != null && venda.Id > 0)
            {
                StatusAtualizacao.VeiculoAtualizar(venda.Veiculo, StatusAtualizacao.StatusVeiculo.DisponivelParaVenda);

                servico.Apagar(venda);

                return RedirectToAction("Autorizar").WithInfo("Venda excluída com sucesso!");
            }
            else
            {
                return RedirectToAction("Autorizar").WithInfo("Venda não encontrada!");
            }
        }
    }
}