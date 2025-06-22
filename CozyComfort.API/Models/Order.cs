using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyComfort.API.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; } = string.Empty;
        
        [Required]
        public int SellerId { get; set; }
        
        [ForeignKey("SellerId")]
        public User Seller { get; set; } = null!;
        
        public int? DistributorId { get; set; }
        
        [ForeignKey("DistributorId")]
        public User? Distributor { get; set; }
        
        public int? ManufacturerId { get; set; }
        
        [ForeignKey("ManufacturerId")]
        public User? Manufacturer { get; set; }
        
        [Required]
        public OrderStatus Status { get; set; }
        
        [Required]
        public decimal TotalAmount { get; set; }
        
        [StringLength(200)]
        public string CustomerName { get; set; } = string.Empty;
        
        [StringLength(300)]
        public string DeliveryAddress { get; set; } = string.Empty;
        
        [StringLength(20)]
        public string CustomerPhone { get; set; } = string.Empty;
        
        public DateTime OrderDate { get; set; } = DateTime.Now;
        
        public DateTime? ExpectedDeliveryDate { get; set; }
        
        public DateTime? ActualDeliveryDate { get; set; }
        
        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;
        
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
    
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        
        [Required]
        public int OrderId { get; set; }
        
        [ForeignKey("OrderId")]
        public Order Order { get; set; } = null!;
        
        [Required]
        public int ProductId { get; set; }
        
        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
        
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public decimal UnitPrice { get; set; }
        
        [Required]
        public decimal TotalPrice { get; set; }
    }
    
    public enum OrderStatus
    {
        Pending = 1,
        CheckingInventory = 2,
        ConfirmedWithDistributor = 3,
        ConfirmedWithManufacturer = 4,
        InProduction = 5,
        ReadyForShipment = 6,
        Shipped = 7,
        Delivered = 8,
        Cancelled = 9
    }
} 