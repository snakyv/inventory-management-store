namespace SportsStore.Models
{
    public interface IStoreRepository
    {
        IQueryable<Inventory> InventoryItems { get; }

        IQueryable<Part> Parts { get; }

        IQueryable<Invoice> Invoices { get; }

        IQueryable<Operation> Operations { get; }

        IQueryable<InventoryOperation> InventoryOperations { get; }

        void SaveInventory(Inventory inventory);

        void CreateInventory(Inventory inventory);

        void DeleteInventory(Inventory inventory);
    }
}