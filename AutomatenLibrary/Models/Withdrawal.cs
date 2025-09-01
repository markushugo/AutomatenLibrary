using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatenLibrary.Models
{
    public class Withdrawal
    {
        public DateTime When { get; set; }
        public double Amount { get; set; }

        public Withdrawal() { }

        public Withdrawal(DateTime when, double amount)
        {
            When = when;
            Amount = amount;
        }
    }
}

