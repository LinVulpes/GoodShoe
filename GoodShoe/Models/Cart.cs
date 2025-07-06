using System.ComponentModel.DataAnnotations;

namespace GoodShoe.Models
{
    // Cart Model - The shopping cart container (one per customer)
    public class Cart
    {
        public int Id { get; set; }
        
        [Required]
        public int CustomerID { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        // Helper properties
        public decimal TotalAmount => CartItems.Sum(item => item.Product.Price * item.Quantity);
        public int TotalItems => CartItems.Sum(item => item.Quantity);
        public bool IsEmpty => !CartItems.Any();
        public int UniqueProductCount => CartItems.Count;
    }

    // CartItem Model - Individual items in the cart (many per cart)
    public class CartItem
    {
        public int Id { get; set; }

        [Required]
        public int CartId { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [StringLength(10)]
        public string Size { get; set; } = string.Empty;

        // Navigation properties
        public virtual Cart Cart { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;

        // Helper properties
        public decimal UnitPrice => Product?.Price ?? 0;
        public decimal TotalPrice => UnitPrice * Quantity;
        public string DisplayName => Product != null ? $"{Product.Brand} {Product.Name}" : "Unknown Product";
        public string DisplaySize => $"Size {Size}";
        public bool IsValidQuantity => Quantity > 0 && Quantity <= 10;
    }
}