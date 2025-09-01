using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatenLibrary.Models
{
    public class Inventory
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public Inventory(int productID, int quantity)
        {
            ProductID = productID;
            Quantity = quantity;
        }

        public Inventory() { }
    }


}

