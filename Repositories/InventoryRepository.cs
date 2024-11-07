using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Repositories
{
    internal class InventoryRepository
    {
        private InventoryContext _inventoryContext;

        public InventoryRepository()
        {
            _inventoryContext=new InventoryContext();
        }

        public List<Inventory> GetAll()
        {
            return _inventoryContext.Inventories.ToList();
        }

        public Inventory GetId(int id)
        {
            var inventory= _inventoryContext.Inventories.FirstOrDefault(x=>x.InventoryId==id);  
            if(inventory!=null)
                return inventory;
            return null;
        }

        public void Add(Inventory inventory)
        {
            _inventoryContext.Inventories.Add(inventory);
            _inventoryContext.SaveChanges();
        }

        public List<Inventory> GetDetailedInfo()
        {
            return _inventoryContext.Inventories.Include(i=> i.Products)
                                                .Include(i=> i.Suppliers)
                                                .Include(i=>i.Transactions)
                                                .ToList();
        }
    }
}
