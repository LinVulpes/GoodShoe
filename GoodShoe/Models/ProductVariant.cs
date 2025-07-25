using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodShoe.Models
{
    public class ProductVariant
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int ProductId { get; set; }
        
        [Required]
        public int Size { get; set; } // 8, 9, 10, 11, 12, 13, 14, 15, 16
        
        [Required]
        [Range(0, int.MaxValue)]
        public int StockCount { get; set; }
        

        // Navigation properties
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}