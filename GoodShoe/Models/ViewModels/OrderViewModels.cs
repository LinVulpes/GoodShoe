using System.ComponentModel.DataAnnotations;

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
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public int Items { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public string StatusColor { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public bool CanBeCancelled { get; set; }
        public bool CanBeShipped { get; set; }
        public bool CanBeDelivered { get; set; }
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
        public DateTime Date { get; set; }
        public List<OrderItemDetailViewModel> Items { get; set; } = new();
        
        // Status management
        public bool CanBeCancelled { get; set; }
        public bool CanBeShipped { get; set; }
        public bool CanBeDelivered { get; set; }
        public List<string> AvailableStatuses { get; set; } = new() { "Pending", "Shipped", "Delivered", "Cancelled" };
    }

    public class OrderItemDetailViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductBrand { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public decimal TotalPrice => Price * Quantity;
        public string DisplayName => $"{ProductBrand} {ProductName}";
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
        
        public List<string> AvailableStatuses { get; set; } = new() { "Pending", "Shipped", "Delivered", "Cancelled" };
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