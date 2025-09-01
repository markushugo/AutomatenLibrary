using AutomatenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutomatenLibrary.Service
{
    public class VendingMachineService
    {
        private readonly ProductService _productService;
        private readonly InventoryService _inventoryService;
        private readonly BankService _bankService;

        public VendingMachineService(ProductService productService,
                                     InventoryService inventoryService,
                                     BankService bankService)
        {
            _productService = productService;
            _inventoryService = inventoryService;
            _bankService = bankService;
        }

        public Product CreateProductWithInventory(int id, string name, double price, int puffs, string brand, int startQty)
        {
            // (valgfrit) tjek for duplikeret ID
            List<Product> all = _productService.GetAllProducts();
            int k;
            for (k = 0; k < all.Count; k++)
            {
                if (all[k].ID == id)
                    throw new Exception("Produkt med dette ID findes allerede.");
            }

            Product product = new Product(id, name, price, puffs, brand);
            _productService.AddProduct(product);

            List<Inventory> items = _inventoryService.GetAllInventory();

            Inventory existing = null;
            int i;
            for (i = 0; i < items.Count; i++)
            {
                if (items[i].ProductID == id)
                {
                    existing = items[i];
                    break;
                }
            }

            if (existing == null)
            {
                Inventory inv = new Inventory(id, startQty);
                _inventoryService.AddInventory(inv);
            }
            else
            {
                existing.Quantity = existing.Quantity + startQty;
                _inventoryService.SaveAllInventory(items); 
            }

            return product;
        }

        public void PrintAllProducts()
        {
            List<Product> products = _productService.GetAllProducts();
            int i;
            for (i = 0; i < products.Count; i++)
            {
                Product p = products[i];
                Console.WriteLine("-----------------------");
                Console.WriteLine("ID: " + p.ID);
                Console.WriteLine("Name: " + p.Name);
                Console.WriteLine("Price: " + p.Price);
                Console.WriteLine("Puffs: " + p.Puffs);
                Console.WriteLine("Brand: " + p.Brand);
            }
        }

        public void PrintInventoryWithProductInfo()
        {
            List<Inventory> inventories = _inventoryService.GetAllInventory();
            List<Product> products = _productService.GetAllProducts();

            int i;
            for (i = 0; i < inventories.Count; i++)
            {
                Inventory inv = inventories[i];
                Product p = FindProductById(products, inv.ProductID);

                Console.WriteLine("-----------------------");
                Console.WriteLine("Produkt ID: " + inv.ProductID);
                if (p != null)
                {
                    Console.WriteLine("Navn: " + p.Name);
                    Console.WriteLine("Mærke: " + p.Brand);
                    Console.WriteLine("Pris: " + p.Price);
                    Console.WriteLine("Puffs: " + p.Puffs);
                }
                else
                {
                    Console.WriteLine("(Produkt ikke fundet i product.json)");
                }
                Console.WriteLine("Antal på lager: " + inv.Quantity);
            }
        }

        public void SetInventoryQuantity(int productId, int newQty)
        {
            List<Inventory> items = _inventoryService.GetAllInventory();

            Inventory found = null;
            int i;
            for (i = 0; i < items.Count; i++)
            {
                if (items[i].ProductID == productId)
                {
                    found = items[i];
                    break;
                }
            }

            if (found == null)
            {
                Inventory inv = new Inventory(productId, newQty);
                _inventoryService.AddInventory(inv);
            }
            else
            {
                found.Quantity = newQty;
                _inventoryService.SaveAllInventory(items);   
            }
        }

        private Product FindProductById(List<Product> all, int id)
        {
            int i;
            for (i = 0; i < all.Count; i++)
            {
                if (all[i].ID == id) return all[i];
            }
            return null;
        }
        public void CashInMachine()
        {
            Console.WriteLine("Hvor mange penge vil du indsætte");
            double amount = Convert.ToDouble(Console.ReadLine());


            _bankService.LoadCash(amount);

            Console.WriteLine("Du har indsat: " + amount + " kr i maskinen");



            Console.WriteLine("Der er nu så meget i maskinen");
            Console.WriteLine(_bankService.GetBank().TotalInMachine + " kr");

        }
        public void CreateProduct()
        {
            Console.WriteLine("Indtast id på produkt");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Indtast navn på produkt");
            string name = Console.ReadLine();

            Console.WriteLine("Indtast pris på produkt");
            double price = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Indtast antal puffs");
            int puffs = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Indtast mærke på produkt");
            string brand = Console.ReadLine();

        
            Product product = new Product(id, name, price, puffs, brand);
            _productService.AddProduct(product);

            
            Console.WriteLine("Indtast start-antal på lager for dette produkt");
            int startQty = Convert.ToInt32(Console.ReadLine());

           
            Inventory inventory = new Inventory(id, startQty);
            _inventoryService.AddInventory(inventory);

            Console.WriteLine("Følgende produkt er oprettet: ");
            PrintProduct(product);
        }
        public void PrintProduct(Product product)
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine("ID: " + product.ID);
            Console.WriteLine("Name: " + product.Name);
            Console.WriteLine("Price: " + product.Price);
            Console.WriteLine("Puffs: " + product.Puffs);
            Console.WriteLine("Brand: " + product.Brand);
            Console.WriteLine("-----------------------");
        }
    }
}
