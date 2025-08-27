using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatenLibrary.Models
{
    public class Product
    {

        private int id;

        private string name;

        private double price;

        private int puffs;

        private string brand;


        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Puffs { get; set; }
        public string Brand { get; set; }

        public Product(int id, string name, double price, int puffs, string brand)
        {
            ID = id;
            Name = name;
            Price = price;
            Puffs = puffs;
            Brand = brand;
        }


        public Product() { }
    }
}
