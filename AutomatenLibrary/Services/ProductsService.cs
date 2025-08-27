using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatenLibrary.Services
{
    public class Product
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Puffs { get; set; }
        public string Brand { get; set; }

        public Product(string id, string name, double price, int puffs, string brand)
        {
            ID = id;
            Name = name;
            Price = price;
            Puffs = puffs;
            Brand = brand;
        }
    }
}
