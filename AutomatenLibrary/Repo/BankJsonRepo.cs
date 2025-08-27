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
            private readonly string _filePath;

            public BankJsonRepo(string path)
            {
                // path kan være en mappe eller en fuld filsti
                if (path.EndsWith(".json"))
                    _filePath = path;
                else
                    _filePath = Path.Combine(path, "bank.json");
            }

            public Bank GetBank()
            {
                if (!File.Exists(_filePath))
                    return new Bank(); // tom standard

                string json = File.ReadAllText(_filePath);
                if (string.IsNullOrWhiteSpace(json))
                    return new Bank();

                Bank bank = JsonSerializer.Deserialize<Bank>(json);
                if (bank == null)
                    return new Bank();

                return bank;
            }

            public void SaveBank(Bank bank)
            {
                JsonSerializerOptions opts = new JsonSerializerOptions();
                opts.WriteIndented = true;

                string json = JsonSerializer.Serialize(bank, opts);
                File.WriteAllText(_filePath, json);
            }





        }
}
