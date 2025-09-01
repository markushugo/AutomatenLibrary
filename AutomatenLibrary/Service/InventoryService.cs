using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatenLibrary.Repo;
using AutomatenLibrary.Interfaces;
using AutomatenLibrary.Models;


namespace AutomatenLibrary.Service
{
    public class InventoryService
    {
      
            private IInventoryRepo _inventoryRepo;

            public InventoryService(IInventoryRepo inventoryRepo)
            {
                _inventoryRepo = inventoryRepo;
            }

            public List<Inventory> GetAllInventory()
            {
                return _inventoryRepo.GetAllInventory();
            }

            public void AddInventory(Inventory inventory)
            {
                _inventoryRepo.Add(inventory);
            }

            public Inventory GetInventoryById(int id)
            {
                return _inventoryRepo.GetByID(id);
            }

            public void SaveAllInventory(List<Inventory> items)
            {
                _inventoryRepo.SaveAllInventory(items);
            }

            public void UpdateStock(int productID, int quantity)
            {
                _inventoryRepo.UpdateStock(productID, quantity);
            }

            // (valgfrit)
            public List<Inventory> GetAll()
            {
                return _inventoryRepo.GetAll();
            }
        }

    }

