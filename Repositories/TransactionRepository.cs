using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Transaction = InventoryManagementSystem.Models.Transaction;

namespace InventoryManagementSystem.Repositories
{
    internal class TransactionRepository
    {
        private InventoryContext _inventoryContext;

        public TransactionRepository()
        {
            _inventoryContext = new InventoryContext();
        }

        public List<Transaction> GetAll() 
        {
            return _inventoryContext.Transactions.ToList();
        }

        public List<Transaction> GetByProductID(int productID)
        {
            return _inventoryContext.Transactions.Where(id=> id.ProductId== productID).ToList();
        }

        public void Add(Transaction transaction)
        {
            _inventoryContext.Transactions.Add(transaction);
            _inventoryContext.SaveChanges();
        }

    }
}
