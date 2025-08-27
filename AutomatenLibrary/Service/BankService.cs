using AutomatenLibrary.Interfaces;
using AutomatenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatenLibrary.Service
{
        public class BankService
        {
            private readonly IBankRepo _bankRepo;
            private Bank _bank;

            public BankService(IBankRepo bankRepo) // Constructor with dependency injection
        {
                _bankRepo = bankRepo;
                _bank = _bankRepo.GetBank();
                if (_bank == null) _bank = new Bank();
            }

           
            public void LoadCash(double amount) // Load initial cash into the machine
        {
                _bank.TotalInMachine = _bank.TotalInMachine + amount;
                _bankRepo.SaveBank(_bank);
            }

            
            public void InsertCash(double amount)  // Insert cash into the machine
        {
                _bank.InsertedAmount = _bank.InsertedAmount + amount;
                _bankRepo.SaveBank(_bank);
            }

            
            public bool HasEnough(double price) // Check if there is enough cash for the purchase
        {
                return _bank.InsertedAmount >= price;
            }

            
            public double CompletePurchase(double price) // Complete the purchase and return change
        {
                _bank.TotalInMachine = _bank.TotalInMachine + price;
                double change = _bank.InsertedAmount - price;
                _bank.InsertedAmount = 0.0;
                _bankRepo.SaveBank(_bank);
                return change;
            }

            
            public double CancelTransaction()
            {
                double refund = _bank.InsertedAmount;
                _bank.InsertedAmount = 0.0;
                _bankRepo.SaveBank(_bank);
                return refund;
            }

            
            public Bank GetBank()
            {
                return _bank;
            }




        }
    }


