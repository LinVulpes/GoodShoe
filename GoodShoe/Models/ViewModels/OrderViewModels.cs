using System.ComponentModel.DataAnnotations;
using GoodShoe.Models;

namespace GoodShoe.ViewModels
{
    // Order Management for Admin
    public class OrderManagementViewModel
    {
        public List<OrderListViewModel> Orders { get; set; } = new();
        public string SearchTerm { get; set; } = string.Empty;
        public string StatusFilter { get; set; } = string.Empty;
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int TotalOrders { get; set; }
    }

    public class OrderListViewModel
    {
        public int OrderID { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public int Items { get; set; } // ADD this line
        public string StatusColor => Status.ToLower() switch
        {
            "pending" => "status-pending",
            "shipping" => "status-shipping",
            "delivered" => "status-delivered",
            "cancelled" => "status-cancelled",
            _ => "status-pending"
        };
        public bool CanEdit { get; set; } = true;
    
        // Format order ID as 6-digit number
        public string FormattedOrderId => $"#{OrderID.ToString("D6")}";
    
        // Format date and time
        public string FormattedDate => Date.ToString("MMM dd, yyyy");
        public string FormattedTime => Date.ToString("hh:mm tt");
    }

    public class OrderDetailsViewModel
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string StatusColor { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DeliveryFee { get; set; } = 20.00m;
        public DateTime Date { get; set; }
        public List<OrderItemDetailViewModel> Items { get; set; } = new();
        
        // Status management
        public bool CanBeCancelled { get; set; }
        public bool CanBeShipped { get; set; }
        public bool CanBeDelivered { get; set; }
        public List<string> AvailableStatuses { get; set; } = new() { "Pending", "Shipping", "Delivered", "Cancelled" };
        
        // Formatted properties
        public string FormattedOrderId => $"#{OrderID.ToString("D6")}";
        public string FormattedDate => Date.ToString("MMM dd, yyyy");
        public string FormattedTime => Date.ToString("hh:mm tt");
        public string EstimatedDeliveryDate => Date.AddDays(5).ToString("MMM dd, yyyy");
        public string MaskedPaymentMethod => 
            PaymentMethod.Contains("Card") ? $"{PaymentMethod} ending in ****" : PaymentMethod;        
    }

    public class OrderItemDetailViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductBrand { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public decimal TotalPrice => Price * Quantity;
        public string DisplayName => $"{ProductBrand} {ProductName}";
    }
    
    // Create Order for Admin
    public class CreateOrderViewModel
    {
        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Required]
        [Display(Name = "Total Amount")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than 0")]
        public decimal TotalAmount { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Pending";

        [Display(Name = "Delivery Address")]
        [StringLength(500)]
        public string? Address { get; set; }

        [Required]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; } = "Credit / Debit Card";

        [Required]
        [Display(Name = "Payment Status")]
        public string PaymentStatus { get; set; } = "Completed";

        [Required]
        [Display(Name = "Order Date")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // For adding products to the order
        [Display(Name = "Product IDs (comma-separated)")]
        public string? ProductIds { get; set; }

        [Display(Name = "Quantities (comma-separated)")]
        public string? Quantities { get; set; }

        [Display(Name = "Sizes (comma-separated)")]
        public string? Sizes { get; set; }

        // CHANGED: From List<dynamic> to List<CustomerOption>
        public List<Customer> Customers { get; set; } = new();
        public List<Product> Products { get; set; } = new();

        // Available options
        public List<string> StatusOptions { get; set; } = new() 
        { 
            "Pending", "Shipping", "Delivered", "Cancelled" 
        };

        public List<string> PaymentMethods { get; set; } = new() 
        { 
            "Credit / Debit Card", "Apple Pay", "PayPal" 
        };

        public List<string> PaymentStatusOptions { get; set; } = new() 
        { 
            "Pending", "Completed", "Failed" 
        };
    }

    public class UpdateOrderStatusViewModel
    {
        public int OrderID { get; set; }
        
        [Required(ErrorMessage = "Status is required")]
        public string NewStatus { get; set; } = string.Empty;
        
        public string CurrentStatus { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;
        
        public List<string> AvailableStatuses { get; set; } = new() { "Pending", "Shipping", "Delivered", "Cancelled" };
    }
    
    // Customer Order History
    public class CustomerOrderHistoryViewModel
    {
        public List<CustomerOrderViewModel> Orders { get; set; } = new();
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }
    }

    public class CustomerOrderViewModel
    {
        public int OrderID { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string StatusColor { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int ItemCount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public List<OrderItemDetailViewModel> Items { get; set; } = new();
    }
}