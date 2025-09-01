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

        public double RemoveCash(double amount) 
        { 
                if (amount > _bank.TotalInMachine)
                {
                    amount = _bank.TotalInMachine; // Can only remove as much as is in the machine
                }
                _bank.TotalInMachine = _bank.TotalInMachine - amount;
                _bankRepo.SaveBank(_bank);
                return amount;
        } // Remove cash from the machine

        public bool HasChangeFor(double price)
        {
            Bank b = _bankRepo.GetBank();
            double change = b.InsertedAmount - price;
            if (change < 0) return false;           
            if (change == 0) return true;            
            return change <= b.TotalInMachine;       
        }

        public double CancelTransaction() // Cancel the transaction and return inserted cash
        {
                double refund = _bank.InsertedAmount;
                _bank.InsertedAmount = 0.0;
                _bankRepo.SaveBank(_bank);
                return refund;
            }

            
            public Bank GetBank() // Get the current state of the bank
        {
                return _bank;
            }

        public double EmptyCashBox()
        {
            double removed = _bankRepo.EmptyCashBox();
            _bank = _bankRepo.GetBank(); 
            return removed;
        }

        public double GetWithdrawnTotal()
        {
            List<Withdrawal> list = _bankRepo.GetWithdrawalHistory();
            double sum = 0.0;
            int i;
            for (i = 0; i < list.Count; i++)
            {
                sum = sum + list[i].Amount;
            }
            return sum;
        }

        public List<Withdrawal> GetWithdrawals()
        {
            return _bankRepo.GetWithdrawalHistory();
        }


    }
    }


