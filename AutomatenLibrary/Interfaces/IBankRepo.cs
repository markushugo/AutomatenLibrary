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
            Bank GetBank();         
            void SaveBank(Bank bank);  
        }
}

