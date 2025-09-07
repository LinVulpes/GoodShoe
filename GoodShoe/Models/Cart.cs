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

        // Navigation properties
        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; }
        
        [ForeignKey("ProductVariantId")]
        public virtual ProductVariant ProductVariant { get; set; }
    }
}