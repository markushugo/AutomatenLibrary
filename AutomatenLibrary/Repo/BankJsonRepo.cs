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
            // path kan være en mappe eller en fuld filsti
            if (path.EndsWith(".json"))
                _filePath = path;
            else
                _filePath = Path.Combine(path, "bank.json");
        }       

        public Bank GetBank() // Hent bankens tilstand fra JSON-filen
        {
            if (!File.Exists(_filePath)) // Hvis filen ikke findes, returner en ny Bank
                return new Bank();

            string json = File.ReadAllText(_filePath);
            Bank bank = JsonSerializer.Deserialize<Bank>(json);
            if (bank == null) return new Bank();
            return bank;
        }
        public void SaveBank(Bank bank) // Gem bankens tilstand til JSON-filen
        {
            JsonSerializerOptions opts = new JsonSerializerOptions();
            opts.WriteIndented = true;
            string json = JsonSerializer.Serialize(bank, opts);
            File.WriteAllText(_filePath, json);
        }
    }
}
