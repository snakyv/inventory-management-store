namespace SportsStore.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new();

        public virtual void AddItem(Inventory inventoryItem, int quantity)
        {
            CartLine? line = Lines
                .FirstOrDefault(p =>
                    p.InventoryItem.InventoryID == inventoryItem.InventoryID);

            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    InventoryItem = new CartInventoryItem
                    {
                        InventoryID = inventoryItem.InventoryID,
                        Name = inventoryItem.Name,
                        Price = inventoryItem.Price ?? 0
                    },
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(int inventoryId)
        {
            Lines.RemoveAll(l =>
                l.InventoryItem.InventoryID == inventoryId);
        }

        public decimal ComputeTotalValue()
        {
            return Lines.Sum(e =>
                e.InventoryItem.Price * e.Quantity);
        }

        public virtual void Clear()
        {
            Lines.Clear();
        }
    }

    public class CartLine
    {
        public int CartLineID { get; set; }

        public CartInventoryItem InventoryItem { get; set; } = new();

        public int Quantity { get; set; }
    }

    public class CartInventoryItem
    {
        public int InventoryID { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}