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

        public Product CreateProductWithInventory(int id, string name, double price, int puffs, string brand, int startQty) // Creates a new product and adds it to inventory
        {
            // Check if product with same ID already exists
            List<Product> all = _productService.GetAllProducts();
            int k;
            for (k = 0; k < all.Count; k++)
            {
                if (all[k].ID == id)
                    throw new Exception("Produkt med dette ID findes allerede.");
            }

            Product product = new Product(id, name, price, puffs, brand);
            _productService.AddProduct(product);

            List<Inventory> items = _inventoryService.GetAllInventory(); // Get current inventory

            Inventory existing = null; 
            int i;
            for (i = 0; i < items.Count; i++) // Check if inventory for this product already exists
            {
                if (items[i].ProductID == id)
                {
                    existing = items[i];
                    break;
                }
            }

            if (existing == null)
            {
                Inventory inv = new Inventory(id, startQty); // Create new inventory entry
                _inventoryService.AddInventory(inv);
            }
            else
            {
                existing.Quantity = existing.Quantity + startQty;
                _inventoryService.SaveAllInventory(items);
            }

            return product;
        }

        public void PrintAllProducts() // Prints all products to the console
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

        public void PrintInventoryWithProductInfo() // Prints inventory along with product information
        {
            List<Inventory> inventories = _inventoryService.GetAllInventory();
            List<Product> products = _productService.GetAllProducts();

            int i;
            for (i = 0; i < inventories.Count; i++) // Loop through each inventory item
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

        public void SetInventoryQuantity(int productId, int newQty) // Sets the inventory quantity for a specific product
        {
            List<Inventory> items = _inventoryService.GetAllInventory();

            Inventory found = null;
            int i;
            for (i = 0; i < items.Count; i++) // Search for the inventory item with the specified productId
            {
                if (items[i].ProductID == productId)
                {
                    found = items[i];
                    break;
                }
            }

            if (found == null)
            {
                Inventory inv = new Inventory(productId, newQty); // Create new inventory entry if not found
                _inventoryService.AddInventory(inv);
            }
            else
            {
                found.Quantity = newQty;
                _inventoryService.SaveAllInventory(items);
            }
        }

        private Product FindProductById(List<Product> all, int id) // Helper method to find a product by its ID
        {
            int i;
            for (i = 0; i < all.Count; i++)
            {
                if (all[i].ID == id) return all[i];
            }
            return null;
        }
        public void CashInMachine() // Method to load cash into the vending machine
        {
            Console.WriteLine("Hvor mange penge vil du indsætte");
            double amount = Convert.ToDouble(Console.ReadLine());


            _bankService.LoadCash(amount);

            Console.WriteLine("Du har indsat: " + amount + " kr i maskinen");



            Console.WriteLine("Der er nu så meget i maskinen");
            Console.WriteLine(_bankService.GetBank().TotalInMachine + " kr");

        }

        public void CancelPurchase() // Method to cancel a purchase and refund the inserted amount
        {
            double refund = _bankService.CancelTransaction();
            Console.WriteLine("Din transaktion er annulleret. Dine penge er returneret: " + refund + " kr");
        }

        public void RemoveCashFromMachine() // Method to remove all cash from the vending machine
        {
            double total = _bankService.GetBank().TotalInMachine;
            if (total <= 0)
            {
                Console.WriteLine("Der er ingen penge i maskinen");
                return;
            }
            double removed = _bankService.RemoveCash(total);
            Console.WriteLine("Du har fjernet: " + removed + " kr fra maskinen"); // Always removes all cash
            Console.WriteLine("Der er nu: " + _bankService.GetBank().TotalInMachine + " kr i maskinen");
        }
        public void CreateProduct() // Method to create a new product and add it to inventory
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


            Console.WriteLine("Indtast hvor mange du vil ligge på lager");
            int startQty = Convert.ToInt32(Console.ReadLine());


            Inventory inventory = new Inventory(id, startQty);
            _inventoryService.AddInventory(inventory);

            Console.WriteLine("Følgende produkt er oprettet: ");
            PrintProduct(product);
        }
        public void PrintProduct(Product product) // Helper method to print product details
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine("ID: " + product.ID);
            Console.WriteLine("Name: " + product.Name);
            Console.WriteLine("Price: " + product.Price);
            Console.WriteLine("Puffs: " + product.Puffs);
            Console.WriteLine("Brand: " + product.Brand);
            Console.WriteLine("-----------------------");
        }


        public void BuyProducts() // Method to handle the purchase of products
        {
            PrintInventoryWithProductInfo(); // Display available products

            Console.WriteLine("Vælg nr på det produkt du vil købe");
            int productId = Convert.ToInt32(Console.ReadLine());

            Inventory inv = _inventoryService.GetInventoryById(productId); // <- int overload
            if (inv == null || inv.Quantity <= 0)
            {
                Console.WriteLine("Produkt ikke fundet eller udsolgt"); 
                return;
            }

            Product p = _productService.GetProductById(productId); // <- int overload
            if (p == null)
            {
                Console.WriteLine("Produkt ikke fundet i product.json");
                return;
            }

            Console.WriteLine("Du har valgt: " + p.Name + "  Pris: " + p.Price.ToString("0.00") + " kr");
            Console.WriteLine("Indtast hvor mange penge du vil putte i maskinen (kr):");
            double amount = Convert.ToDouble(Console.ReadLine());
            _bankService.InsertCash(amount);

            
            if (!_bankService.HasEnough(p.Price)) // Check if enough money has been inserted
            {
                Bank b1 = _bankService.GetBank(); // Get current bank state
                double missing = p.Price - b1.InsertedAmount; // Calculate how much more is needed
                if (missing < 0) missing = 0; // Just in case
                Console.WriteLine("Ikke nok penge indsat. Mangler: " + missing.ToString("0.00") + " kr");
                double refund1 = _bankService.CancelTransaction();
                Console.WriteLine("Dine penge er returneret: " + refund1.ToString("0.00") + " kr");
                return;
            }

          
            Bank b2 = _bankService.GetBank(); // Get updated bank state
            double changeNeeded = b2.InsertedAmount - p.Price;
            if (changeNeeded > b2.TotalInMachine) // Check if the machine can provide change
            {
                Console.WriteLine("Køb afvist: Maskinen kan ikke give byttepenge (" + changeNeeded.ToString("0.00") + " kr)."); // Cannot provide change
                double refund2 = _bankService.CancelTransaction();
                Console.WriteLine("Dine penge er returneret: " + refund2.ToString("0.00") + " kr");
                return;
            }

           Console.WriteLine("Vil du fortryde køb ja/nej");
            string cancelPurchase = Console.ReadLine().ToLower();

            if (cancelPurchase == "ja")
            {
                double refund3 = _bankService.CancelTransaction();
                Console.WriteLine("Din transaktion er annulleret. Dine penge er returneret: " + refund3.ToString("0.00") + " kr");
                return;
            }
            else if (cancelPurchase == "nej")
            {
                double change = _bankService.CompletePurchase(p.Price);
                Console.WriteLine("Køb gennemført. Dit bytte er: " + change.ToString("0.00") + " kr");
            }
            else 
            {
            
            Console.WriteLine("Ugyldigt svar. Transaktionen annulleret.");


            }


             int newQty = inv.Quantity - 1; // Decrease inventory quantity by 1
            _inventoryService.UpdateStock(productId, newQty); // <- int overload
        }
        public void AdminEmptyCashBox() // Method for admin to empty the cash box
        {
            double removed = _bankService.EmptyCashBox();
            Console.WriteLine("Tømt kasse: " + removed.ToString("0.00") + " kr");
        }

        public void PrintWithdrawalsOverview()
        {
            List<Withdrawal> list = _bankService.GetWithdrawals();
            if (list == null || list.Count == 0)
            {
                Console.WriteLine("Ingen tømninger registreret.");
                return;
            }
            int i; double sum = 0.0;
            for (i = 0; i < list.Count; i++)
            {
                Withdrawal w = list[i];
                Console.WriteLine(w.When.ToString("yyyy-MM-dd HH:mm") + "  " + w.Amount.ToString("0.00") + " kr");
                sum = sum + w.Amount;
            }
            Console.WriteLine("I alt fjernet: " + sum.ToString("0.00") + " kr");
        }

        public void SetNewStock() 
        {
            PrintInventoryWithProductInfo();
            Console.WriteLine("Hvilke produkt vil du restock?");
            int productId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Hvor mange vil du ligge på lager?");
            int newQty = Convert.ToInt32(Console.ReadLine());         
            SetInventoryQuantity(productId, newQty);
            Console.WriteLine("Lager opdateret.");


        }
    }
}
