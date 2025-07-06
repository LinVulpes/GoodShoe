using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodShoe.Models
{
    public class Product
    {
        // Product ID
        public int Id { get; set; }
        
        // Name
        [Required] 
        [StringLength(100)] 
        public string Name { get; set; } = string.Empty;

        // Brand
        [Required]
        [StringLength(50)]
        public string Brand { get; set; } = string.Empty;
        
        // Price
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        // Description
        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;
        
        // StockCount
        public int StockCount { get; set; }
        
        // Color
        [Required]
        public string Color { get; set; } = string.Empty;
        
        // Category (Men/Women/Unisex)
        [Required]
        [StringLength(50)] 
        public string Category { get; set; } = string.Empty;
        
        // ImageURL
        [StringLength(200)] 
        public string ImageUrl { get; set; } = string.Empty;
        
        // Available Shoe Sizes (comma-separated: "8,9,10,11,12")
        [Required]
        public string AvailableSizes { get; set; } = string.Empty;
        
        // Navigation properties
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        
        // Helper methods
        public List<string> GetSizesList()
        {
            return string.IsNullOrEmpty(AvailableSizes) 
                ? new List<string>() 
                : AvailableSizes.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        
        public bool IsInStock => StockCount > 0;
        public bool IsLowStock => StockCount <= 5;
    }
    
    // Helper class for shoe sizes
    public static class ShoeSizes
    {
        public static readonly string[] Available = { "8", "9", "10", "11", "12", "13", "14", "15", "16" };
        
        public static bool IsValid(string size)
        {
            return Available.Contains(size);
        }
        
        public static string GetSizesAsString()
        {
            return string.Join(",", Available);
        }
    }
}