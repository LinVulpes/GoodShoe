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

    // Checkout Process
    public class CheckoutViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new();
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; } = 0; // Not used in simple version
        public decimal ShippingAmount { get; set; } = 0; // Not used in simple version
        public decimal TotalAmount { get; set; }
        
        // Customer Information (pre-filled from profile)
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        
        // Shipping Information
        [Required(ErrorMessage = "Shipping address is required")]
        [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters")]
        [Display(Name = "Shipping Address")]
        public string Address { get; set; } = string.Empty;

        // Payment Information
        [Required(ErrorMessage = "Payment method is required")]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; } = string.Empty;

        [StringLength(1000)]
        [Display(Name = "Order Notes (Optional)")]
        public string Notes { get; set; } = string.Empty;

        // Available options
        public List<string> AvailablePaymentMethods { get; set; } = new() 
        { 
            "Credit Card", "Debit Card", "PayPal", "Bank Transfer" 
        };
    }

    public class OrderConfirmationViewModel
    {
        public int OrderID { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<OrderItemDetailViewModel> Items { get; set; } = new();
        
        public string EstimatedDelivery => OrderDate.AddDays(7).ToString("MMM dd, yyyy");
    }

    /*// Quick Add to Cart (for product pages)
    public class QuickAddToCartViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductBrand { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public List<string> AvailableSizes { get; set; } = new();
        public bool IsInStock { get; set; }
        public int StockCount { get; set; }
        
        [Required]
        public string SelectedSize { get; set; } = string.Empty;
        
        [Range(1, 10)]
        public int Quantity { get; set; } = 1;
    }*/
}