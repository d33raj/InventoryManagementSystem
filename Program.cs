using InventoryManagementSystem.Models;
using InventoryManagementSystem.Presentation;
using InventoryManagementSystem.Repositories;

namespace InventoryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {

            InventoryStore inventoryStore = new InventoryStore();
            inventoryStore.Start();

        }
    }
}
