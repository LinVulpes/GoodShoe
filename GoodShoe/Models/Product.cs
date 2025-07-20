using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodShoe.Models
{
    public class Product
    {
        // Product Id
        [Key]
        public int ProductId { get; set; }
        
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
        [StringLength(200)]
        public string? Description { get; set; }
        
        // Color
        [Required]
        [StringLength(20)]
        public string? Color { get; set; }
        
        // Category
        [Required]
        [StringLength(20)] 
        public string Category { get; set; } = string.Empty; // (Men/Women/Unisex)
        
        // ImageURL
        [StringLength(255)] 
        public string? ImageUrl { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        // Navigation properties for the database
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
        
        /*[Required]
        public string AvailableSizes { get; set; } = string.Empty; // Available Shoe Sizes*/
        
        // Helper methods
        /*public List<string> GetSizesList()
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
        }*/
    }
}