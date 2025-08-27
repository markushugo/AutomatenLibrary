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
        private IBankRepo _bankRepo;
        public BankService(IBankRepo bankRepo)
        {
            _bankRepo = bankRepo;
        }
        void InsertCash(double amount);       
        bool HasEnough(double price);        
        double CompletePurchase(double price);
        double CancelTransaction();           
        Bank GetBank();


    }
}
