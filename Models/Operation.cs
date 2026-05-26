using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStore.Models
{
    [Table("Operations")]
    public class Operation
    {
        [Key]
        public int OperationID { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<InventoryOperation> InventoryOperations { get; set; }
            = new List<InventoryOperation>();
    }
}