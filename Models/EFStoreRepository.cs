using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        private readonly StoreDbContext context;

        public EFStoreRepository(StoreDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Inventory> InventoryItems =>
            context.InventoryItems
                .Include(i => i.Part)
                .Include(i => i.Invoice);

        public IQueryable<Part> Parts => context.Parts;

        public IQueryable<Invoice> Invoices => context.Invoices;

        public IQueryable<Operation> Operations => context.Operations;

        public IQueryable<InventoryOperation> InventoryOperations =>
            context.InventoryOperations
                .Include(io => io.Inventory)
                .Include(io => io.Operation);

        public void CreateInventory(Inventory inventory)
        {
            inventory.Part = null;
            inventory.Invoice = null;

            context.InventoryItems.Add(inventory);
            context.SaveChanges();
        }

        public void SaveInventory(Inventory inventory)
        {
            inventory.Part = null;
            inventory.Invoice = null;

            Inventory? dbEntry = context.InventoryItems
                .FirstOrDefault(i => i.InventoryID == inventory.InventoryID);

            if (dbEntry != null)
            {
                dbEntry.Name = inventory.Name;
                dbEntry.Price = inventory.Price;
                dbEntry.Quantity = inventory.Quantity;
                dbEntry.PartID = inventory.PartID;
                dbEntry.InvoiceID = inventory.InvoiceID;

                context.SaveChanges();
            }
        }

        public void DeleteInventory(Inventory inventory)
        {
            Inventory? dbEntry = context.InventoryItems
                .FirstOrDefault(i => i.InventoryID == inventory.InventoryID);

            if (dbEntry != null)
            {
                IQueryable<InventoryOperation> relatedOperations =
                    context.InventoryOperations
                        .Where(io => io.InventoryID == dbEntry.InventoryID);

                context.InventoryOperations.RemoveRange(relatedOperations);

                context.InventoryItems.Remove(dbEntry);

                context.SaveChanges();
            }
        }
    }
}