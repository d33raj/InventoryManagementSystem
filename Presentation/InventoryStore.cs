using InventoryManagementSystem.Exceptions;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Presentation
{
    internal class InventoryStore
    {
        ProductRepository productRepository=new ProductRepository();
        SupplierRepository supplierRepository=new SupplierRepository();
        TransactionRepository transactionRepository=new TransactionRepository();
        InventoryRepository inventoryRepository=new InventoryRepository();

        public void Start()
        {
            while (true)
            {
                Console.WriteLine($"Welcome To Inventory Management System!"+
                                  $"\nSelect from below options."+
                                  $"\n-----------------------------------------"+
                                  $"\n1. Product Management \n2. Supplier Management"+
                                  $"\n3. Transaction Management \n4. Generate Report \n5.Exit"
                                  + $"\n-----------------------------------------");
                int choice= int.Parse( Console.ReadLine() );

                switch( choice )
                {
                    case 1: ProductMenu(productRepository);
                        break;
                    case 2: SupplierMenu(supplierRepository);
                        break;
                    case 3: TransactionMenu(productRepository,transactionRepository,inventoryRepository);
                        break;
                    case 4: GenerateReport(inventoryRepository);
                        break;
                    case 5: Environment.Exit(0);
                        break;
                    default: Console.WriteLine("Enter Correct Option:");
                        break;
                }
            }
        }

        public void ProductMenu(ProductRepository productRepository)
        {
            while (true)
            {
                Console.WriteLine($"Welcome to Product Management!.\nSelect one option from below." +
                                  $"\n-----------------------------------------" +
                                  $"\n1. Add Product \n2. Update Product \n3. Delete Product" +
                                  $"\n4. View Product \n5. View All Products \n6. Return to MainMenu"
                                  + $"\n-----------------------------------------");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1: AddProduct(productRepository); break;
                    case 2: UpdateProduct(productRepository); break;
                    case 3: DeleteProduct(productRepository); break;
                    case 4: ViewProduct(productRepository); break;
                    case 5: ViewAllProducts(productRepository); break;
                    case 6: return; 
                    default: Console.WriteLine("Invalid Choice Enter correct Option.");
                        break;
                }
            }
        }

        public void SupplierMenu(SupplierRepository supplierRepository)
        {
            while (true)
            {
                Console.WriteLine($"Welcome to Supplier Management!.\nSelect one option from below." + 
                                 $"\n-----------------------------------------" +
                                 $"\n1. Add Supplier \n2. Update Supplier \n3. Delete Supplier" +
                                 $"\n4. View Supplier \n5. View All Suppliers \n6. Return to MainMenu"
                                 + $"\n-----------------------------------------");
                int choice=int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1: AddSupplier(supplierRepository); break;
                    case 2: UpdateSupplier(supplierRepository); break;
                    case 3: DeleteSupplier(supplierRepository); break;
                    case 4: ViewSupplier(supplierRepository); break;
                    case 5: ViewAllSuppliers(supplierRepository); break;
                    case 6: return;
                    default: Console.WriteLine("Invalid Choice Enter correct Option");  break;
                }
            }
        }

        public void TransactionMenu(ProductRepository productRepository,TransactionRepository transactionRepository,
            InventoryRepository inventoryRepository)
        {
            while (true)
            {
                Console.WriteLine($"Welcome to Transaction Management!\nSelect an option from below."+
                                  $"\n-----------------------------------------"+
                                   $"\n1. Add Stock to Product \n2. Remove Stock of Product \n3.Transaction History"+
                                   $"\n4. Return to MainMenu." + $"\n-----------------------------------------");
                int choice= int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1: AddStock(productRepository, transactionRepository, inventoryRepository); break;
                    case 2: RemoveStock(productRepository, transactionRepository, inventoryRepository); break;
                    case 3: TransactionHistory(transactionRepository); break;
                    case 4: return;
                    default: Console.WriteLine("Invalid Choice Enter correct Option."); break;
                }
            }
        }

        static void GenerateReport(InventoryRepository inventoryRepository)
        {
            Console.WriteLine("************************Inventory Report************************");
            var inventory= inventoryRepository.GetDetailedInfo();
            foreach ( var item in inventory)
            {
                Console.WriteLine("------------------Items--------------------");
                Console.WriteLine(item);
                Console.WriteLine("\n------------------Products-----------------");
                foreach (var product in item.Products)
                { 
                    Console.WriteLine(product);
                    Console.WriteLine("-------------------------------------------");
                }

                Console.WriteLine("\n------------------Suppliers----------------");
                foreach (var supplier in item.Suppliers)
                {
                    Console.WriteLine(supplier);
                    Console.WriteLine("-------------------------------------------");
                }

                Console.WriteLine("\n----------------Transaction----------------");
                foreach (var trans in item.Transactions)
                { 
                    Console.WriteLine(trans);
                    Console.WriteLine("---------------------------------------");
                }
                Console.WriteLine("\n\n");
            }
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++PRODUCT METHODS+++++++++++++++++++++++++++++++++++++++++++++++++++
        static void AddProduct(ProductRepository productRepository)
        {
            try
            {
                Console.WriteLine("Enter Name of the Product:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Product Description:");
                string description = Console.ReadLine();
                Console.WriteLine("Enter Quantity of Product:");
                int quantity = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Price of Product:");
                double price = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter Inventory ID:");
                int id = int.Parse(Console.ReadLine());

                var product = new Product() { ProductName = name, Description = description, Quantity = quantity, Price = price, InventoryId = id };
                productRepository.AddProduct(product);
                Console.WriteLine("Product Added Successfully :) ");
            }
            catch (AlreadyExistsException ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        static void UpdateProduct(ProductRepository productRepository)
        {
            try
            {
                var (productExist, product) = ViewProduct(productRepository);
                if (!productExist)
                    return;
                Console.WriteLine("Enter New Name of the Product:");
                product.ProductName = Console.ReadLine();
                Console.WriteLine("Enter New Product Description:");
                product.Description = Console.ReadLine();
                Console.WriteLine("Enter New Quantity of Product:");
                product.Quantity = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter New Price of Product:");
                product.Price = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter New Inventory ID:");
                product.InventoryId = int.Parse(Console.ReadLine());

                productRepository.UpdateProduct(product);
                Console.WriteLine("Product Updated Successfully :) ");
            }
            catch(InvalidIDException ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        static void DeleteProduct(ProductRepository productRepository)
        {
            var (productExist, product) = ViewProduct(productRepository);
            if (!productExist)
                return;
            productRepository.DeleteProduct(product);
            Console.WriteLine("Product Deleted Successfully :) ");

        }

        static (bool, Product) ViewProduct(ProductRepository productRepository)
        {
            Console.WriteLine("Enter Product Id to Search:");
            int id = int.Parse(Console.ReadLine());
            try
            {
                var product = productRepository.GetById(id);
                Console.WriteLine(product);
                return (true, product);
            }
            catch (InvalidIDException ex)
            {
                Console.WriteLine(ex.Message);
                return (false, null);
            }
        }

        static void ViewAllProducts(ProductRepository productRepository)
        {
            var products = productRepository.GetAll();
            foreach (var product in products)
            {
                Console.WriteLine(product);
                Console.WriteLine("===============================");
            }
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++SUPPLIER METHODS+++++++++++++++++++++++++++++++++++++++++++++++++++
        static void AddSupplier(SupplierRepository supplierRepository)
        {
            try
            {
                Console.WriteLine("Enter Supplier Name to Add:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Contact Info of Supplier");
                string contactInfo = Console.ReadLine();
                Console.WriteLine("Enter Inventory Id of Supplier");
                int id = int.Parse(Console.ReadLine());

                var supplier = new Supplier() { SupplierName = name, SupplierContact = contactInfo, InventoryId = id };
                supplierRepository.AddSupplier(supplier);
                Console.WriteLine("Supplier Added Successfully :) ");
            }
            catch (AlreadyExistsException ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        static void UpdateSupplier(SupplierRepository supplierRepository)
        {
            try
            {
                var (supplierExist, supplier) = ViewSupplier(supplierRepository);
                if (!supplierExist)
                    return;
                Console.WriteLine("Enter New Supplier Name:");
                supplier.SupplierName = Console.ReadLine();
                Console.WriteLine("Enter New Supplier Contact Info:");
                supplier.SupplierContact = Console.ReadLine();
                Console.WriteLine("Enter New Inventory Id of Supplier:");
                supplier.InventoryId = int.Parse(Console.ReadLine());

                supplierRepository.UpdateSupplier(supplier);
                Console.WriteLine("Supplier Updated Successfully :) ");
            }
            catch (InvalidIDException ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        static void DeleteSupplier(SupplierRepository supplierRepository)
        {
            var (supplierExist, supplier) = ViewSupplier(supplierRepository);
            if (!supplierExist)
                return;
            supplierRepository.DeleteSupplier(supplier);
            Console.WriteLine("Supplier Deleted Successfully :)");
        }

        static (bool, Supplier) ViewSupplier(SupplierRepository supplierRepository)
        {
            Console.WriteLine("Enter Supplier Id to Search:");
            int id = int.Parse(Console.ReadLine());
            try
            {
                var supplier = supplierRepository.GetId(id);
                Console.WriteLine(supplier);
                return (true, supplier);
            }
            catch (InvalidIDException ex)
            {
                Console.WriteLine(ex.Message);
                return (false, null);
            }
        }
        static void ViewAllSuppliers(SupplierRepository supplierRepository)
        {
            var suppliers = supplierRepository.GetAll();
            foreach (var supplier in suppliers)
            {
                Console.WriteLine(supplier);
                Console.WriteLine("===============================");
            }
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++TRANSACTION METHODS+++++++++++++++++++++++++++++++++++++++++++++++++++

        static void AddStock(ProductRepository productRepository, TransactionRepository transactionRepository, InventoryRepository inventoryRepository)
        {
            try
            {
                Console.WriteLine("Enter Product ID to Add Stock:");
                int id = int.Parse(Console.ReadLine());
                var productId = productRepository.GetById(id);
                if (productId == null)
                { throw new InvalidIDException("No Product Found with this ID"); }

                Console.WriteLine("Enter Inventory Id of Product");
                int inventoryId = int.Parse(Console.ReadLine());

                var inventory = inventoryRepository.GetId(inventoryId);
                if (inventory == null)
                { throw new InvalidIDException("No Inventory Found with this ID"); }

                if(productId.InventoryId != inventoryId)
                { throw new InvalidIDException("Inventory ID Doesn't match with Inventory ID of Product"); }

                Console.WriteLine("Enter Quantity to Add:");
                int quantity = int.Parse(Console.ReadLine());
                productId.Quantity += quantity;

                var transaction = new Transaction() { ProductId = id, Type = "Add", Quantity = quantity, Date = DateTime.Now, InventoryId = inventoryId };

                productRepository.UpdateProduct(productId);
                transactionRepository.Add(transaction);
                Console.WriteLine("Stock Added Successfully :) ");
            }
            catch (InvalidIDException ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        static void RemoveStock(ProductRepository productRepository, TransactionRepository transactionRepository, InventoryRepository inventoryRepository)
        {
            try
            {
                Console.WriteLine("Enter Product ID to Remove Stock:");
                int id = int.Parse(Console.ReadLine());
                var productId = productRepository.GetById(id);
                if (productId == null)
                { throw new InvalidIDException("No Product Found with this ID"); }

                Console.WriteLine("Enter Inventory Id of Product");
                int inventoryId = int.Parse(Console.ReadLine());
                var inventory = inventoryRepository.GetId(inventoryId);
                if (inventory == null)
                { throw new InvalidIDException("No Inventory Found with this ID"); }

                if (productId.InventoryId != inventoryId)
                { throw new InvalidIDException("Inventory ID Doesn't match with Inventory ID of Product"); }

                Console.WriteLine("Enter Quantity to Remove:");
                int quantity = int.Parse(Console.ReadLine());

                if (productId.Quantity < quantity)
                { throw new InsufficientStockException("Stock Not Sufficient to Remove. Enter Valid Quantity."); }
                productId.Quantity -= quantity;

                var transaction = new Transaction() { ProductId = id, Type = "Remove", Quantity = quantity, Date = DateTime.Now, InventoryId = inventoryId };

                productRepository.UpdateProduct(productId);
                transactionRepository.Add(transaction);
                Console.WriteLine("Stock Removed Successfully :) ");
            }
            catch (InvalidIDException ex) { Console.WriteLine(ex.Message); }
            catch(InsufficientStockException ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        static void TransactionHistory(TransactionRepository transactionRepository)
        {
            var transaction = transactionRepository.GetAll();
            foreach (var trans in transaction)
            {
                Console.WriteLine(trans);
                Console.WriteLine("=========================================");
            }
        }
    }
}
