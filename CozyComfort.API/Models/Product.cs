using System.ComponentModel.DataAnnotations;

namespace CozyComfort.API.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string SKU { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Material { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string Size { get; set; } = string.Empty;
        
        [StringLength(30)]
        public string Color { get; set; } = string.Empty;
        
        [Required]
        public decimal Price { get; set; }
        
        public int ProductionCapacity { get; set; }
        
        public int ProductionLeadTimeDays { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedDate { get; set; }
    }
} 