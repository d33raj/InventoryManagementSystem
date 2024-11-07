using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    internal class Inventory
    {
        public int InventoryId { get; set; }
        public string Location { get; set; }
        public List<Product> Products { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public List<Transaction> Transactions { get; set; }

        public override string ToString()
        {
            return $"Inventory Id: {InventoryId}"+
                   $"\nLocation of Inventory: {Location}";
        }

    }
}
