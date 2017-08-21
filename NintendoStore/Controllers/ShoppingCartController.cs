using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NintendoStore.Models;
using NintendoStore.Entities;

namespace NintendoStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private GamesEntities db = new GamesEntities();

        public ActionResult Index()
        {

            return View("Index");
        }
        private int isExisting(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductId == id)
                { return i; }
            }
            return -1;
        }

        public ActionResult Remove(int id)
        {
            int index = isExisting(id);
            List<Item> cart = (List<Item>)Session["cart"];

            if (cart[index].Quantity > 1) { cart[index].Quantity--; }
            else { cart.RemoveAt(index); }
            
            Session["cart"] = cart;
            return View("Index");
        }
        public ActionResult Order(int id)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(db.NintendoProducts.Find(id), 1));
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExisting(id);
                if (index == -1)
                { cart.Add(new Item(db.NintendoProducts.Find(id), 1)); }
                else
                {
                    cart[index].Quantity++;
                    Session["cart"] = cart;
                }
            }
            return View("Index");
        }

       
        public ActionResult Checkout()
        {
            return View(new ShippingDetails());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(ShippingDetails shippingDetails)
        {
            
            List<Item> cart = (List<Item>)Session["cart"];            

            if (cart == null )
            {
                ModelState.AddModelError("", "You cannot place an order with an empty cart!");
            }
            if (ModelState.IsValid)
            {
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}
