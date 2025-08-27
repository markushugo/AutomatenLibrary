using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatenLibrary.Models
{
    internal class Inventory
    {
        private string _productId;

        private int _quantity;


        public string ProductId { get; set; }
        public int Quantity { get; set; }


        public Inventory(string productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public Inventory() { }


    }
}
