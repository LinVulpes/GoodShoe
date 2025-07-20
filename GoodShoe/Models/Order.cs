using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodShoe.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int CustomerId { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Pending"; // Pending, Shipped, Delivered, Cancelled

        [Required]
        [StringLength(255)]
        public string? Address { get; set; }

        [Required]
        [StringLength(50)]
        public string? PaymentMethod { get; set; }
        
        [StringLength(20)]
        public string PaymentStatus { get; set; } = "Pending"; // Pending, Completed, Failed

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // Helper properties
        /*public int TotalItems => OrderItems.Sum(item => item.Quantity);
        public bool CanBeCancelled => Status == "Pending";
        public bool CanBeShipped => Status == "Pending";
        public bool CanBeDelivered => Status == "Shipped";*/
        
        public string StatusColor => Status.ToLower() switch
        {
            "pending" => "warning",
            "shipped" => "info", 
            "delivered" => "success",
            "cancelled" => "danger",
            _ => "secondary"
        };

        public string StatusDisplayClass => Status.ToLower() switch
        {
            "pending" => "badge-warning",
            "shipped" => "badge-info",
            "delivered" => "badge-success", 
            "cancelled" => "badge-danger",
            _ => "badge-secondary"
        };
    }
    
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductVariantId { get; set; }
        
        // To preserve data at time of order
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        public int Size { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        

        // Navigation properties
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        
        [ForeignKey("ProductVariantId")]
        public virtual ProductVariant ProductVariant { get; set; }

        /*// Helper properties
        public decimal TotalPrice => Price * Quantity;
        public string DisplayName => $"{ProductName}";*/
    }
}