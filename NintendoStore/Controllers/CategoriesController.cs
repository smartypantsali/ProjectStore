using NintendoStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NintendoStore.Controllers
{
    public class CategoriesController : Controller
    {
        private GamesEntities db = new GamesEntities();

        public ActionResult Index()
        {
            var categories = from w in db.NintendoCategories
                            select w;
            return View(categories);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NintendoCategory category = db.NintendoCategories.Find(id);

            if (category == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId, CategoryName")]NintendoCategory category)
        {
            if(ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(category);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(NintendoCategory category)
        {
            if(ModelState.IsValid)
            {
                db.NintendoCategories.Add(category);
                db.SaveChanges();
                return RedirectToAction("index");
            }

            return View(category);
        }

        public ActionResult Delete(int id)
        {
            NintendoCategory category = db.NintendoCategories.Find(id);
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            NintendoCategory category = db.NintendoCategories.Find(id);
            db.NintendoCategories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("index");
        }
            
    }
}