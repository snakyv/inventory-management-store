using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStore.Models
{
    [Table("Invoices")]
    public class Invoice
    {
        [Key]
        public int InvoiceID { get; set; }

        public int? SupplieID { get; set; }

        public string SerialNumber { get; set; } = string.Empty;

        [Column("totalValue", TypeName = "decimal(18, 2)")]
        public decimal? TotalValue { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public ICollection<Inventory> InventoryItems { get; set; }
            = new List<Inventory>();
    }
}