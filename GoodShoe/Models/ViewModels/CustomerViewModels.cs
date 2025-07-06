using System.ComponentModel.DataAnnotations;
using GoodShoe.Models.ViewModels;

namespace GoodShoe.ViewModels
{
    // Customer Profile Management
    public class CustomerProfileViewModel
    {
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters")]
        [Display(Name = "Address")]
        public string Address { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public List<CustomerOrderViewModel> RecentOrders { get; set; } = new();
        
        // Statistics
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }
        
        // Helper properties
        public string FullName => $"{FirstName} {LastName}";
        public string DisplayAddress => string.IsNullOrEmpty(Address) ? "No address provided" : Address;
        public string MemberSince => CreatedAt.ToString("MMMM yyyy");
    }

    public class EditCustomerProfileViewModel
    {
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters")]
        [Display(Name = "Address")]
        public string Address { get; set; } = string.Empty;
    }

    // Customer Dashboard
    public class CustomerDashboardViewModel
    {
        public string CustomerName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }
        public int CartItemsCount { get; set; }
        
        public List<CustomerOrderViewModel> RecentOrders { get; set; } = new();
        public List<ProductViewModel> RecommendedProducts { get; set; } = new();
        
        // Quick stats
        public int PendingOrders { get; set; }
        public int DeliveredOrders { get; set; }
        public DateTime LastOrderDate { get; set; }
    }

    // Customer List for Admin
    public class CustomerManagementViewModel
    {
        public List<CustomerListItemViewModel> Customers { get; set; } = new();
        public string SearchTerm { get; set; } = string.Empty;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int TotalCustomers { get; set; }
        
        // Statistics
        public int NewCustomersThisMonth { get; set; }
        public int ActiveCustomers { get; set; }
        public decimal AverageOrderValue { get; set; }
    }

    public class CustomerListItemViewModel
    {
        public int CustomerID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }
        public DateTime? LastOrderDate { get; set; }
        
        // Helper properties
        public string MemberSince => CreatedAt.ToString("MMM yyyy");
        public string LastActivity => LastOrderDate?.ToString("MMM dd, yyyy") ?? "Never";
        public string CustomerStatus => TotalOrders > 0 ? "Active" : "New";
        public string StatusClass => TotalOrders > 0 ? "badge-success" : "badge-secondary";
    }

    // Customer Details for Admin
    public class CustomerDetailsViewModel
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        
        // Statistics
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }
        public decimal AverageOrderValue { get; set; }
        public DateTime? LastOrderDate { get; set; }
        
        // Order history
        public List<CustomerOrderViewModel> OrderHistory { get; set; } = new();
        
        // Helper properties
        public string FullName => $"{FirstName} {LastName}";
        public string DisplayAddress => string.IsNullOrEmpty(Address) ? "No address provided" : Address;
        public string MemberSince => CreatedAt.ToString("MMMM dd, yyyy");
        public string CustomerType => TotalOrders switch
        {
            0 => "New Customer",
            < 5 => "Regular Customer",
            < 15 => "Valued Customer",
            _ => "VIP Customer"
        };
    }

    // Password Change
    public class ChangePasswordViewModel
    {
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Current password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "New password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}