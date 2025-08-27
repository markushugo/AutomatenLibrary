using AutomatenLibrary.Interfaces;
using AutomatenLibrary.Models;
using AutomatenLibrary.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;



namespace AutomatenLibrary.Repo
{
    public class InventoryJsonRepo : IInventoryRepo
    {

        private List<Inventory> _inventory = new List<Inventory>(); //List for all products
        private string _path; //Path to the JSON file

        public InventoryJsonRepo(string path) //Constructor with path to the JSON file
        {
            _path = path;
            LoadFile(_path); //Load data from the JSON file
        }

        private void LoadFile(string path) //Load data from the JSON file
        {
            string json = File.ReadAllText(path + "inventory.json"); //Read the file
            _inventory = JsonSerializer.Deserialize<List<Inventory>>(json); 
        }

        private void SaveFile(string path) //Save data to the JSON file
        {
            File.WriteAllText(path + "inventory.json", JsonSerializer.Serialize(_inventory, new JsonSerializerOptions { WriteIndented = true }));
        }

        public void Add(Inventory inventory) //Add a new product
        {
            _inventory.Add(inventory);
            SaveFile(_path); //Save the data to the JSON file
        }

        public List<Inventory> GetAll() //List for all products
        {
            return _inventory;
        }

        public Inventory GetByID(string productID) //Get a product by its ID
        {
            foreach (Inventory inventory in _inventory)
            {
                if (inventory.ProductID == inventory.Quantity)
                {
                    return inventory;
                }
            }
            return null;
        }

        public void UpdateStock(Inventory updatedInventory) 
        {
            if (updatedInventory == null)
            {
                return;
            }

            for (int i = 0; i < _inventory.Count; i++)
            {
                Inventory current = _inventory[i];
                if (current.ProductID == updatedInventory.ProductID)
                {
                    current.Quantity = updatedInventory.Quantity;
                    SaveFile(_path); // Save changes to the JSON file
                    return;
                }
            }
        }
    }
}
