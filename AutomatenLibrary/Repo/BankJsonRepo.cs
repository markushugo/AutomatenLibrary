using AutomatenLibrary.Interfaces;
using AutomatenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AutomatenLibrary.Repo
{
    public class BankJsonRepo : IBankRepo
    {

        private List<Bank> _bank = new List<Bank>(); //List for all products
        private string _path; //Path to the JSON file

        public BankJsonRepo(string path) //Constructor with path to the JSON file
        {
            _path = path;
            LoadFile(_path); //Load data from the JSON file
           
        }

        private void LoadFile(string path) //Load data from the JSON file
        {
            string json = File.ReadAllText(path + "bank.json"); //Read the file
            _bank = JsonSerializer.Deserialize<List<Bank>>(json);
        }

        private void SaveFile(string path) //Save data to the JSON file
        {
            File.WriteAllText(path + "bank.json", JsonSerializer.Serialize(_bank, new JsonSerializerOptions { WriteIndented = true }));
        }

        public void Add(Bank bank) //Add a new product
        {
            _bank.Add(bank);
            SaveFile(_path); //Save the data to the JSON file
        }

        public List<Bank> GetAll() //List for all products
        {
            return _bank;
        }
        public Bank GetByID(int id) //Get a product by its ID
        {
            foreach (Bank bank in _bank)
            {
                if (id == bank.TotalInMachine)
                {
                    return bank;
                }
            }
            return null;
        }

        public Bank GetBank()
        {
            return _bank[0];
        }






        public void SaveBank(Bank bank) //Save a specific bank object
        {
            _bank[0] = bank;
            SaveFile(_path); //Save the data to the JSON file




        }
    }
}
