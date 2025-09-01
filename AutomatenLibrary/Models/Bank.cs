using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomatenLibrary.Models.Withdrawal;

namespace AutomatenLibrary.Models
{
    public class Bank
    {
        public double TotalInMachine { get; set; }   
        public double InsertedAmount { get; set; }  

     
        public Bank(double totalInMachine, double insertedAmount) 
        { 
            TotalInMachine = totalInMachine;
            InsertedAmount = insertedAmount;



        }    

        public Bank() 
        { 
            
        }

    }



   
}

