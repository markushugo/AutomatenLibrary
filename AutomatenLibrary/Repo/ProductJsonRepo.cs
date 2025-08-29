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
    public class ProductJsonRepo
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
                string json = File.ReadAllText(path + "product.json"); //Read the file
                _product = JsonSerializer.Deserialize<List<Product>>(json); //Deserialize the JSON data to a list of products
        }

        public void SaveFile(string path) //Save data to the JSON file
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
