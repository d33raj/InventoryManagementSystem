using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    internal class Supplier
    {
        [Key]

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierContact {  get; set; }

        public Inventory Inventory { get; set; }
        [ForeignKey("Inventory")]
        public int InventoryId { get; set; }

        public override string ToString()
        {
            return $"Supplier Id: {SupplierId}" +
                   $"\nSupplier Name: {SupplierName}" +
                   $"\nSupplier Contact Info: {SupplierContact}" +
                   $"\nInventory Id of Supplier: {InventoryId}";
        }
    }
}
