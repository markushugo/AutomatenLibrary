using AutomatenLibrary.Interfaces;
using AutomatenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static AutomatenLibrary.Models.Bank;

namespace AutomatenLibrary.Repo
{
    public class BankJsonRepo : IBankRepo
    {

        public readonly string _filePath;


        public BankJsonRepo(string path)
        {
            if (path.EndsWith(".json"))
                _filePath = Path.GetDirectoryName(path);
            else
                _filePath = path;
            if (_filePath == null) _filePath = ".";
        }
        private string BankPath() { return Path.Combine(_filePath, "bank.json"); }


        private string HistoryPath()
        {
            return Path.Combine(_filePath, "withdrawals.json");
        }
        public Bank GetBank()
        {
            string fp = BankPath();
            if (!Directory.Exists(_filePath)) Directory.CreateDirectory(_filePath);
            if (!File.Exists(fp))
            {
                Bank bnew = new Bank();
                bnew.TotalInMachine = 0.0;
                bnew.InsertedAmount = 0.0;
                SaveBank(bnew);
                return bnew;
            }
            string json = File.ReadAllText(fp);
            if (string.IsNullOrWhiteSpace(json))
            {
                Bank bempty = new Bank();
                SaveBank(bempty);
                return bempty;
            }
            try
            {
                Bank b = JsonSerializer.Deserialize<Bank>(json);
                if (b == null) return new Bank();
                return b;
            }
            catch (JsonException)
            {
                return new Bank();
            }
        }
        public void SaveBank(Bank bank)
        {
            string fp = BankPath();
            if (!Directory.Exists(_filePath)) Directory.CreateDirectory(_filePath);
            JsonSerializerOptions opts = new JsonSerializerOptions();
            opts.WriteIndented = true;
            string json = JsonSerializer.Serialize(bank, opts);
            File.WriteAllText(fp, json);
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

        public void RemoveCash(double amount) // Remove cash from the machine
        {
            Bank bank = GetBank();
            if (amount > bank.TotalInMachine)
                throw new InvalidOperationException("Ikke nok penge i maskinen");
            bank.TotalInMachine -= amount;
            SaveBank(bank);
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


        private void EnsureHistoryFile()
        {
            string hp = HistoryPath();
            if (!Directory.Exists(_filePath)) Directory.CreateDirectory(_filePath);
            if (!File.Exists(hp))
            {
                List<Withdrawal> empty = new List<Withdrawal>();
                JsonSerializerOptions opts = new JsonSerializerOptions();
                opts.WriteIndented = true;
                string json = JsonSerializer.Serialize(empty, opts);
                File.WriteAllText(hp, json);
            }
        

        }
        public List<Withdrawal> GetWithdrawalHistory()
        {
            EnsureHistoryFile();
            string hp = HistoryPath();
            string json = File.ReadAllText(hp);
            if (string.IsNullOrWhiteSpace(json)) return new List<Withdrawal>();

            try
            {
                List<Withdrawal> list = JsonSerializer.Deserialize<List<Withdrawal>>(json);
                if (list == null) return new List<Withdrawal>();
                return list;
            }
            catch (JsonException)
            {
                return new List<Withdrawal>();
            }
        }
        private void SaveWithdrawalHistory(List<Withdrawal> list)
        {
            EnsureHistoryFile();
            string hp = HistoryPath();
            JsonSerializerOptions opts = new JsonSerializerOptions();
            opts.WriteIndented = true;
            string json = JsonSerializer.Serialize(list, opts);
            File.WriteAllText(hp, json);
        }


        public void AppendWithdrawal(Withdrawal w)
        {
            List<Withdrawal> list = GetWithdrawalHistory();
            list.Add(w);
            SaveWithdrawalHistory(list);
        }

        public double EmptyCashBox()
        {
            Bank b = GetBank();
            double removed = b.TotalInMachine;
            if (removed <= 0.0) return 0.0;

            b.TotalInMachine = 0.0;
            SaveBank(b);

            Withdrawal w = new Withdrawal(DateTime.Now, removed);
            AppendWithdrawal(w);

            return removed;
        }
    }
}
