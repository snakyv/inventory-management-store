using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStore.Models
{
    public class OrderLine
    {
        public int OrderLineID { get; set; }

        public int OrderID { get; set; }

        public Order? Order { get; set; }

        public int InventoryID { get; set; }

        public string ItemName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}