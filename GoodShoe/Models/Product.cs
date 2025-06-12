using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodShoe.Models
{
    public class Product
    {
        // Product ID
        public int Id { get; set; }
        
        // Name
        [Required] [StringLength(100)] 
        public string? Name { get; set; } = string.Empty;

        // Brand
        [StringLength(50)]
        public string? Brand { get; set; } = string.Empty;
        
        // Price
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        // Description
        [StringLength(1000)]
        public string? Description { get; set; }
        
        // Shoe Size
        public decimal Size {  get; set; }
        
        // StockCount
        public int StockCount { get; set; }
        
        // Color
        public string? Color { get; set; } = string.Empty;
        
        // Gender
        [StringLength(50)] 
        public string? Gender { get; set; } = string.Empty; // [Men/Women/Unisex]

        // for stock availability
        public bool IsActive { get; set; } = true;
        
        // ImageURL
        [StringLength(200)] public string? ImageUrl { get; set; } = string.Empty;
    }
}
