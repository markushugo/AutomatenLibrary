using AutomatenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatenLibrary.Interfaces
{
    public interface IInventoryRepo
    {      
            
            List<Inventory> GetAllInventory();
            void SaveAllInventory(List<Inventory> items);

            void Add(Inventory inventory);
            Inventory GetByID(int productID);
            void UpdateStock(int productID, int quantity);

            
            List<Inventory> GetAll();
        }
    }

