using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStore.Models
{
    [Table("Parts")]
    public class Part
    {
        [Key]
        public int PartID { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<Inventory> InventoryItems { get; set; }
            = new List<Inventory>();
    }
}