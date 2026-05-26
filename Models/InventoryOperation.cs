using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStore.Models
{
    [Table("InventoryOperations")]
    public class InventoryOperation
    {
        [Key]
        public int InventoryOperationID { get; set; }

        public string Explanation { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        public int? InventoryID { get; set; }

        public int? OperationID { get; set; }

        public Inventory? Inventory { get; set; }

        public Operation? Operation { get; set; }
    }
}