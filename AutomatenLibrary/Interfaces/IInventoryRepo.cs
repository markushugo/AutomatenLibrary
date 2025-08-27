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
        List<Inventory> GetAll(); //List for all inventory items

        void Add(Inventory inventory); //Add a new inventory item

        Inventory GetByID(string productID); //Get a product by its ID
        
        void UpdateStock(string productID, int quantity);
    }
}
