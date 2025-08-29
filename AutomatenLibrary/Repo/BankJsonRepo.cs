using AutomatenLibrary.Interfaces;
using AutomatenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AutomatenLibrary.Repo
{
    public class BankJsonRepo : IBankRepo
    {

        public readonly string _filePath;

        public BankJsonRepo(string path) //Constructor with path to the JSON file
        {

            if (path.EndsWith(".json"))
                _filePath = path;
            else
                _filePath = Path.Combine(path, "bank.json");
        }

        public Bank GetBank() // Get the current state of the bank
        {
            if (!File.Exists(_filePath))
                return new Bank();

            string json = File.ReadAllText(_filePath);
            Bank bank = JsonSerializer.Deserialize<Bank>(json);
            if (bank == null) return new Bank();
            return bank;
        }
        public void SaveBank(Bank bank) // Save the current state of the bank
        {
            JsonSerializerOptions opts = new JsonSerializerOptions();
            opts.WriteIndented = true;
            string json = JsonSerializer.Serialize(bank, opts);
            File.WriteAllText(_filePath, json);
        }

        public void LoadCash(double amount)  // Load initial cash into the machine    
        {
            Bank bank = GetBank();
            bank.TotalInMachine += amount;
            SaveBank(bank);
        }
        public void InsertCash(double amount)  // Insert cash into the machine
        {
            Bank bank = GetBank();
            bank.InsertedAmount += amount;
            SaveBank(bank);
        }
        public bool HasEnough(double price)  // Check if there is enough cash for the purchase       
        {
            Bank bank = GetBank();
            return bank.InsertedAmount >= price;
        }
        public double CompletePurchase(double price) // Complete the purchase and return change
        {
            Bank bank = GetBank();
            if (bank.InsertedAmount < price)
                throw new InvalidOperationException("Ikke nok penge");
            double change = bank.InsertedAmount - price;
            bank.TotalInMachine += price;
            bank.InsertedAmount = 0;
            SaveBank(bank);
            return change;
        }
        public double CancelTransaction() // Cancel the transaction and return inserted cash      
        {
            Bank bank = GetBank();
            double inserted = bank.InsertedAmount;
            bank.InsertedAmount = 0;
            SaveBank(bank);
            return inserted;

        }
    }
}
