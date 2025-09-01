using AutomatenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatenLibrary.Interfaces
{
    public interface IProductRepo
    {
        List<Product> GetAll(); //List for all products

        void Add(Product product); //Add a new product

        Product GetByID(int id); //Get a product by its ID

        Product UdpatePrice(int id, double newPrice); //Update the price of a product
    }
}
