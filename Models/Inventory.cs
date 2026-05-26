using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStore.Models
{
    [Table("Inventory")]
    public class Inventory
    {
        [Key]
        public int InventoryID { get; set; }

        [Required(ErrorMessage = "Please enter an inventory item name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a price")]
        [Range(typeof(decimal), "0.01", "999999.99",
            ErrorMessage = "Please enter a positive price")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Please enter quantity")]
        [Range(0, 100000,
            ErrorMessage = "Quantity cannot be negative")]
        public int? Quantity { get; set; }

        [Required(ErrorMessage = "Please select a part category")]
        public int? PartID { get; set; }

        public int? InvoiceID { get; set; }

        public Part? Part { get; set; }

        public Invoice? Invoice { get; set; }

        public ICollection<InventoryOperation> InventoryOperations { get; set; }
            = new List<InventoryOperation>();
    }
}