using NintendoStore.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NintendoStore.Controllers
{
    public class ProductsController : Controller
    {
        private GamesEntities db = new GamesEntities();

        public ActionResult Index(string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            var products = from w in db.NintendoProducts
                             select w;

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            return View(products.OrderBy(i => i.ProductName).ToPagedList(pageNumber, pageSize));
        }
        
        public ActionResult Add()
        {
            ViewBag.ProductId = new SelectList(db.NintendoCategories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "ProductId,ProductName,Picture,Description,Price,CategoryId")] NintendoProduct nintendoProduct)
        {
            if (ModelState.IsValid)
            {
                db.NintendoProducts.Add(nintendoProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.NintendoCategories, "CategoryId", "CategoryName", nintendoProduct.ProductId);
            return View(nintendoProduct);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NintendoProduct nintendoProduct = db.NintendoProducts.Find(id);
            if (nintendoProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.NintendoCategories, "CategoryId", "CategoryName", nintendoProduct.ProductId);
            return View(nintendoProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Picture,Description,Price,CategoryId")] NintendoProduct nintendoProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nintendoProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.NintendoCategories, "CategoryId", "CategoryName", nintendoProduct.ProductId);
            return View(nintendoProduct);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NintendoProduct nintendoProduct = db.NintendoProducts.Find(id);
            if (nintendoProduct == null)
            {
                return HttpNotFound();
            }
            return View(nintendoProduct);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NintendoProduct nintendoProduct = db.NintendoProducts.Find(id);
            db.NintendoProducts.Remove(nintendoProduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
