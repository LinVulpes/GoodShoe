using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodShoe.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Shipped, Delivered, Cancelled

        [Required]
        [StringLength(500)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // Helper properties
        public int TotalItems => OrderItems.Sum(item => item.Quantity);
        public bool CanBeCancelled => Status == "Pending";
        public bool CanBeShipped => Status == "Pending";
        public bool CanBeDelivered => Status == "Shipped";
        
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
        public int Id { get; set; }

        [Required]
        public int OrderID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string Size { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        // Navigation properties
        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;

        // Helper properties
        public decimal TotalPrice => Price * Quantity;
        public string DisplayName => $"{ProductName}";
    }
}