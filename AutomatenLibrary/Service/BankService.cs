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

        public Bank GetBank()
        {
            return _bankRepo.GetBank();
        }

        public void SaveBank(Bank bank)
        {
            _bankRepo.SaveBank(bank);
        }




    }
}
