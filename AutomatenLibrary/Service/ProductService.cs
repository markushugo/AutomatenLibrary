using AutomatenLibrary.Interfaces;
using AutomatenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AutomatenLibrary
{
    public class ProductService
    {
        private IProductRepo _productRepo; // Interface for product repository

        public ProductService(IProductRepo productRepo) // Constructor with dependency injection
        {
            _productRepo = productRepo;
        }

        public List<Product> GetAllProducts() // Get all products
        {
            return _productRepo.GetAll();
        }

        public void AddProduct(Product product) // Add a new product
        {
            _productRepo.Add(product);
        }

        public Product GetProductById(int id) // Get a product by its ID
        {
            return _productRepo.GetByID(id);
        }

        public Product UpdatePrice(int id, double newPrice) // Update the price of a product
        {
            return _productRepo.UdpatePrice(id, newPrice);
        }
    }
}
