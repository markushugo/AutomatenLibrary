using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatenLibrary.Models
{
    public class Inventory
    {
        private string _productID;
        private int _quantity;

        public string ProductID { get; set; }
        public int Quantity { get; set; }

        public Inventory(string productID, int quantity)
        {
            ProductID = productID;
            Quantity = quantity;
        }

        public Inventory() { }



    }
}
