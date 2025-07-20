using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Cart and CartItems

namespace GoodShoe.Models
{
    // Cart Model - The shopping cart container (one per customer)
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        
        [Required]
        public int CustomerId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        /*// Helper properties
        public decimal TotalAmount => CartItems.Sum(item => item.Product.Price * item.Quantity);
        public int TotalItems => CartItems.Sum(item => item.Quantity);
        public bool IsEmpty => !CartItems.Any();
        public int UniqueProductCount => CartItems.Count;*/
    }

    // CartItem Model - Individual items in the cart (many per cart)
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CartId { get; set; }

        [Required]
        public int ProductVariantId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        
        public DateTime AddedAt { get; set; } = DateTime.Now;

        /*[Required]
        [StringLength(10)]
        public string Size { get; set; } = string.Empty;*/

        // Navigation properties
        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; }
        
        [ForeignKey("ProductVariantId")]
        public virtual ProductVariant ProductVariant { get; set; }
    }
}