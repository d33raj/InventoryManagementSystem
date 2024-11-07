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
    internal class SupplierRepository
    {
        private InventoryContext _inventoryContext;

        public SupplierRepository()
        {
            _inventoryContext = new InventoryContext();
        }

        public List<Supplier> GetAll()
        {
            return _inventoryContext.Suppliers.ToList();
        }

        public Supplier GetId(int id)
        {
            var supplier= _inventoryContext.Suppliers.FirstOrDefault(x=>x.SupplierId == id);
            if (supplier != null)
                return supplier;
            throw new InvalidIDException("No Such Supplier Exists");
        }

        public void AddSupplier(Supplier supplier)
        {
            var supp = _inventoryContext.Suppliers.Any(p => p.SupplierName == supplier.SupplierName);
            if (supp)
                throw new AlreadyExistsException("Already Exists. Enter new Name");

            _inventoryContext.Suppliers.Add(supplier);
            _inventoryContext.SaveChanges();
        }

        public void DeleteSupplier(Supplier supplier)
        {
            _inventoryContext.Remove(supplier);
            _inventoryContext.SaveChanges();
        }

        public void UpdateSupplier(Supplier supplier)
        {
            var p = _inventoryContext.Suppliers.Find(supplier.SupplierId);
            if (p == null)
                throw new InvalidIDException("No Such Supplier Exists");

            _inventoryContext.Entry(supplier).State=EntityState.Modified;
            _inventoryContext.SaveChanges();
        }
    }
}
