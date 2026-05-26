namespace SportsStore.Models.ViewModels
{
    public class InventoryListViewModel
    {
        public IEnumerable<Inventory> InventoryItems { get; set; }
            = Enumerable.Empty<Inventory>();

        public PagingInfo PagingInfo { get; set; } = new();

        public string? CurrentCategory { get; set; }
    }
}