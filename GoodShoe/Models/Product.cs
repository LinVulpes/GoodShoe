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
        
        // Image stored as binary data
        public byte[]? Image { get; set; }
        
        // Store the original file name and content type
        [StringLength(255)]
        public string? ImageFileName { get; set; }
        
        // Timestamps for products
        [Display(Name = "Created Date")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Navigation properties for the database
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
        
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
        
        [NotMapped]
        public string StockStatusClass
        {
            get
            {
                var totalStock = ProductVariants?.Sum(pv => pv.StockCount) ?? 0;
                if (totalStock == 0) return "stock-out";
                if (totalStock < 10) return "stock-low";
                return "stock-high";
            }
        }
        
        [NotMapped]
        public string StockStatus
        {
            get
            {
                var totalStock = ProductVariants?.Sum(pv => pv.StockCount) ?? 0;
                if (totalStock == 0) return "Out of Stock";
                if (totalStock < 10) return "Low Stock";
                return "In Stock";
            }
        }
    }
}