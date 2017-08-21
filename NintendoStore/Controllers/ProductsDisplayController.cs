using NintendoStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace NintendoStore.Controllers
{
    public class ProductsDisplayController : Controller
    {

        private GamesEntities db = new GamesEntities();        

        public ActionResult Index(string proDuctss, string searchString, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            
            var productss = new List<string>();

            var produtsQRY = from wd in db.NintendoCategories
                             orderby wd.CategoryId
                             select wd.CategoryName;
            productss.AddRange(produtsQRY.Distinct());
            ViewBag.proDuctss = new SelectList(productss);

            var products = from w in db.NintendoProducts
                           select w;            

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(w => w.ProductName.Contains(searchString));
            }

            if(!String.IsNullOrEmpty(proDuctss))
            {
                products = products.Where(w => w.NintendoCategory.CategoryName == proDuctss);
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            return View(products.OrderBy(i => i.ProductName).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            NintendoProduct product = db.NintendoProducts.Find(id);

            return View(product);
        }
    }
}