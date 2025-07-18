using System.ComponentModel.DataAnnotations;

namespace GoodShoe.ViewModels
{
    // Shopping Cart
    public class CartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public int TotalItems { get; set; }
        public bool IsEmpty => !Items.Any();
        public int CustomerID { get; set; }
    }

    public class CartItemViewModel
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductBrand { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsInStock { get; set; }
        public int StockAvailable { get; set; }
        
        public decimal TotalPrice => Price * Quantity;
        public string DisplayName => $"{ProductBrand} {ProductName}";
    }

    public class AddToCartViewModel
    {
        [Required(ErrorMessage = "Product is required")]
        public int ProductID { get; set; }
        
        [Required(ErrorMessage = "Please select a size")]
        public string Size { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 10, ErrorMessage = "Quantity must be between 1 and 10")]
        public int Quantity { get; set; } = 1;

        // For display purposes
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public List<string> AvailableSizes { get; set; } = new();
    }

    public class UpdateCartItemViewModel
    {
        [Required]
        public int CartItemId { get; set; }
        
        [Required]
        [Range(1, 10, ErrorMessage = "Quantity must be between 1 and 10")]
        public int Quantity { get; set; }
    }
}