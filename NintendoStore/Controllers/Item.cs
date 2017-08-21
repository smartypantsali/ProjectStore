using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NintendoStore.Models;

namespace NintendoStore.Controllers
{
    public class Item
    {

        private NintendoProduct product = new NintendoProduct();

        public NintendoProduct Product
        {
            get { return product; }
            set { product = value; }
        }

        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public Item()
        {

        }

        public Item(NintendoProduct product, int quantity)
        {
            this.product = product;
            this.quantity = quantity;
        }

    }
}