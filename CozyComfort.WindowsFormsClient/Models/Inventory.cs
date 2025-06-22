using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyComfort.WindowsFormsClient.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }
        
        [Required]
        public int ProductId { get; set; }
        
        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
        
        [Required]
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
        
        [Required]
        public int StockQuantity { get; set; }
        
        public int ReorderLevel { get; set; }
        
        public int MaxStockLevel { get; set; }
        
        public DateTime LastUpdated { get; set; } = DateTime.Now;
        
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;
    }
} 