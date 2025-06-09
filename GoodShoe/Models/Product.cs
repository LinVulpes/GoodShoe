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
        public string? Name { get; set; }

        // Price
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        // Description
        [StringLength(200)]
        public string? Description { get; set; }
        
        // Brand
        public string? Brand { get; set; }
        
        // Shoe Size
        public decimal Size {  get; set; }
        
        // StockCount
        public int StockCount { get; set; }
        
        // ImageURL
        //[StringLength(200)]
        //public string? ImageUrl { get; set; }
        
        // Color
        public string? Color { get; set; }
        
        // Gender
        [StringLength(50)]
        public string? Gender { get; set; }
    }
}
