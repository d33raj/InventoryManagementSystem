using InventoryManagementSystem.Data;
using InventoryManagementSystem.Exceptions;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Repositories
{
    internal class ProductRepository
    {
        private InventoryContext _inventoryContext;

        public ProductRepository()
        {
            _inventoryContext = new InventoryContext();
        }

        public List<Product> GetAll()
        {
            var productsList=_inventoryContext.Products.ToList();
            return productsList;
        }

        public Product GetById(int id)
        {
            var product = _inventoryContext.Products.FirstOrDefault(x => x.ProductId == id);
            if (product != null)
                return product;
            throw new InvalidIDException("No such Product Exists");
        }

        public void AddProduct(Product product)
        {
            var prod = _inventoryContext.Products.Any(p => p.ProductName == product.ProductName);
            if (prod)
                throw new AlreadyExistsException("Already Exists. Enter new Name");

            _inventoryContext.Products.Add(product);
            _inventoryContext.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _inventoryContext.Remove(product);
            _inventoryContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            var p= _inventoryContext.Products.Find(product.ProductId);
            if (p == null)
                throw new InvalidIDException("No such Product Exists");

            _inventoryContext.Entry(product).State=EntityState.Modified;
            _inventoryContext.SaveChanges();
        }
    }
}
