using NintendoStore.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NintendoStore.Controllers
{
    public class EmployeesController : Controller
    {
        private GamesEntities db = new GamesEntities();

        public ActionResult Index(string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            var employees = from w in db.NintendoEmployees
                            select w;            

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(employees.OrderBy(i => i.Name).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "StaffId,Name,Email,Position,Description,Profilepic")]NintendoEmployee employee)
        {
            if(ModelState.IsValid)
            {
                db.NintendoEmployees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NintendoEmployee employee = db.NintendoEmployees.Find(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffId, Profilepic, Name, Position, Description, Email")]NintendoEmployee employee)
        {
            if(ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public ActionResult Delete(int id)
        {
            NintendoEmployee employee = db.NintendoEmployees.Find(id);

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NintendoEmployee employee = db.NintendoEmployees.Find(id);
            db.NintendoEmployees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}