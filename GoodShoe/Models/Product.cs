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
        [StringLength(50)] 
        public string Name { get; set; } = string.Empty;

        // Brand
        [Required]
        [StringLength(20)]
        public string Brand { get; set; } = string.Empty;
        
        // Price
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        // Description
        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;
        
        // StockCount
        public int StockCount { get; set; }
        
        // Color
        [Required]
        public string Color { get; set; } = string.Empty;
        
        // Category
        [Required]
        [StringLength(50)] 
        public string Category { get; set; } = string.Empty; // (Men/Women/Unisex)
        
        // ImageURL
        [StringLength(200)] 
        public string ImageUrl { get; set; } = string.Empty;
        
        [Required]
        public string AvailableSizes { get; set; } = string.Empty; // Available Shoe Sizes
        
        // Navigation properties for the database
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        
        // Helper methods
        public List<string> GetSizesList()
        {
            return string.IsNullOrEmpty(AvailableSizes) 
                ? new List<string>() 
                : AvailableSizes.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .ToList();
        }
        
        // Method to check if a specific size is available
        public bool IsSizeAvailable(string size)
        {
            var availableSizesList = GetSizesList();
            return availableSizesList.Contains(size.Replace("US ", ""));
        }
        
        // Method to check if US size format is available
        public bool IsUSSizeAvailable(int size)
        {
            return IsSizeAvailable(size.ToString());
        }
        
        public bool IsInStock => StockCount > 0;
        public bool IsLowStock => StockCount <= 5;
    }
    
    // Class for different 9 shoe sizes
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
        
        // US size format
        public static string[] GetUSFormats()
        {
            return Available.Select(s => $"US {s}").ToArray();
        }
    }
}