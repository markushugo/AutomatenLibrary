using AutomatenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatenLibrary.Interfaces
{
    
        public interface IBankRepo
        {
        void LoadCash(double amount);  // Load initial cash into the machine    
        void InsertCash(double amount);  // Insert cash into the machine
        bool HasEnough(double price);  // Check if there is enough cash for the purchase       
        double CompletePurchase(double price); // Complete the purchase and return change
        double CancelTransaction(); // Cancel the transaction and return inserted cash      
        Bank GetBank(); // Get the current state of the bank

        void SaveBank(Bank bank); // Save the current state of the bank
    }
}

