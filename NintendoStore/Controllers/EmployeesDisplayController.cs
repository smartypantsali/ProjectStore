using NintendoStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NintendoStore.Controllers
{
    public class EmployeesDisplayController : Controller
    {

        private GamesEntities db = new GamesEntities();

        public ActionResult Index()
        {
            var productsDisplay = from w in db.NintendoEmployees
                                  select w;

            return View(productsDisplay);
        }
    }
}