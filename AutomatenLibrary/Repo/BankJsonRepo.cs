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
    public class BankJsonRepo
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
    }
}
