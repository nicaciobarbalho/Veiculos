using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Veiculos.Web.Areas.Venda.Controllers
{
    public class ComprarController : Controller
    {
        // GET: Venda/Comprar    
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarVeiculo(Veiculos.Web.Areas.Venda.Models.Veiculo veiculo)
        {
            return View();
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
