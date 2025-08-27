using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatenLibrary.Models
{
    public class Bank
    {
        public double TotalInMachine { get; set; }   // Penge i maskinen i alt
        public double InsertedAmount { get; set; }   // Penge indsat af aktuel kunde

        public Bank() { }

        public Bank(double totalInMachine, double insertedAmount)
        {
            TotalInMachine = totalInMachine;
            InsertedAmount = insertedAmount;
        }
    }
}
