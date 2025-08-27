using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatenLibrary.Models
{
    public class Bank
    {
        private double _totalInMachine;
        private decimal _insertedAmount;
        public double TotalInMachine { get; set; }
        public double InsertedAmount { get; set; }


        public Bank(double totalInMachine, double insertedAmount)
        {
            TotalInMachine = totalInMachine;
            InsertedAmount = insertedAmount;
        }

        public Bank() { }
    }
}
