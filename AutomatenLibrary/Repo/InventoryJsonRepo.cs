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
     
            private List<Inventory> _inventory = new List<Inventory>();
            private readonly string _path;

            public InventoryJsonRepo(string path)
            {
                _path = path;
                EnsureLoaded();
            }

            private string FilePath()
            {
                return Path.Combine(_path, "inventory.json");
            }

            private void EnsureLoaded()
            {
                string fp = FilePath();
                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }

                if (!File.Exists(fp))
                {
                    _inventory = new List<Inventory>();
                    SaveFile();
                    return;
                }

                string json = File.ReadAllText(fp);
                if (string.IsNullOrWhiteSpace(json))
                {
                    _inventory = new List<Inventory>();
                    SaveFile();
                    return;
                }

                try
                {
                    List<Inventory> loaded = JsonSerializer.Deserialize<List<Inventory>>(json);
                    if (loaded != null) _inventory = loaded; else _inventory = new List<Inventory>();
                }
                catch (JsonException)
                {
                    
                    _inventory = new List<Inventory>();
                    SaveFile();
                }
            }

            private void SaveFile()
            {
                string fp = FilePath();
                JsonSerializerOptions opts = new JsonSerializerOptions();
                opts.WriteIndented = true;
                string json = JsonSerializer.Serialize(_inventory, opts);
                File.WriteAllText(fp, json);
            }

           

            public void Add(Inventory inventory)
            {
                _inventory.Add(inventory);
                SaveFile();
            }

            public List<Inventory> GetAllInventory()
            {
                
                return new List<Inventory>(_inventory);
            }

            public void SaveAllInventory(List<Inventory> items)
            {
                if (items == null) items = new List<Inventory>();
                _inventory = items;
                SaveFile();
            }

            public Inventory GetByID(int productID)
            {
                int i;
                for (i = 0; i < _inventory.Count; i++)
                {
                    if (_inventory[i].ProductID == productID)
                        return _inventory[i];
                }
                return null;
            }

            public void UpdateStock(int productID, int quantity)
            {
                int i;
                for (i = 0; i < _inventory.Count; i++)
                {
                    if (_inventory[i].ProductID == productID)
                    {
                        _inventory[i].Quantity = quantity;
                        SaveFile();
                        return;
                    }
                }


                Inventory inv = new Inventory(productID, quantity);
                _inventory.Add(inv);
                SaveFile();
            }


            public List<Inventory> GetAll()
            {
                return GetAllInventory();
            }
        }
    }
