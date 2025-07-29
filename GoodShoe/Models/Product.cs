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
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        // Color
        [Required]
        [StringLength(20)]
        public string Color { get; set; } = string.Empty;
        
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
        
        // Keep these for backward compatibility but calculate from ProductVariants : Not using for now
        public int StockCount => ProductVariants?.Sum(pv => pv.StockCount) ?? 0;
        public string AvailableSizes => ProductVariants != null ? 
            string.Join(",", ProductVariants.Where(pv => pv.StockCount > 0).Select(pv => pv.Size).OrderBy(s => s)) : "";
        
        // Helper method for getting sizes as list (if needed elsewhere)
        public List<string> GetSizesList()
        {
            return ProductVariants?.Where(pv => pv.StockCount > 0)
                .Select(pv => pv.Size.ToString())
                .OrderBy(s => int.Parse(s))
                .ToList() ?? new List<string>();
        }
    }
}