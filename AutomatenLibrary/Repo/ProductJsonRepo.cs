using AutomatenLibrary.Models;
using AutomatenLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace AutomatenLibrary.Repo
{
    public class ProductJsonRepo : IProductRepo
    {
        private List<Product> _product = new List<Product>(); //List for all products
        private string _path; //Path to the JSON file

        public ProductJsonRepo(string path) //Constructor with path to the JSON file
        {
            _path = path;
            LoadFile(_path); //Load data from the JSON file
        }

        public void LoadFile(string path) //Load data from the JSON file
        {
            string file = Path.Combine(path, "product.json");
            if (File.Exists(file))
            {
                string json = File.ReadAllText(file);
                var products = JsonSerializer.Deserialize<List<Product>>(json);
                if (products != null)
                    _product = products;
            }
        }

        private void SaveFile(string path) // Method to save the data to the json file
        {
            File.WriteAllText(path + "product.json", JsonSerializer.Serialize(_product, new JsonSerializerOptions { WriteIndented = true }));
        }

        public void Add(Product product) //Add a new product
        {
            _product.Add(product);
            SaveFile(_path); //Save the data to the JSON file
        }

        public List<Product> GetAll() //List for all products
        {
            return _product;
        }

        public Product GetByID(int id) //Get a product by its ID
        {
            foreach (Product product in _product)
            {
                if (product.ID == id)
                {
                    return product;
                }
            }
            return null;
        }
    }
}
