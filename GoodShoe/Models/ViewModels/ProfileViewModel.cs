using System.ComponentModel.DataAnnotations;

namespace GoodShoe.ViewModels
{
    public class ProfileViewModel
    {
        public int CustomerId { get; set; }
        
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "Location")]
        public string? Location { get; set; }
        
        // Purchase History: Separate current and past orders
        public List<PurchaseHistoryItem>? PurchaseHistory { get; set; }
        public List<PurchaseHistoryItem> CurrentOrders => PurchaseHistory?.Where(p => p.IsNewOrder).ToList() ?? new List<PurchaseHistoryItem>();
        public List<PurchaseHistoryItem> PastOrders => PurchaseHistory?.Where(p => !p.IsNewOrder).ToList() ?? new List<PurchaseHistoryItem>();
    }
    
    public class EditProfileViewModel
    {
        public int CustomerId { get; set; }
        
        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; } = string.Empty;        
        
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        [Display(Name = "Address")]
        public string Address { get; set; }
    }    

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class PurchaseHistoryItem
    {
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
    
        // NEW PROPERTIES:
        public int Quantity { get; set; } = 1;
        public int OrderId { get; set; }
        public string Status { get; set; } = string.Empty;
    
        // COMPUTED PROPERTIES:
        public decimal TotalPrice => Price * Quantity;
        public string FormattedOrderId => $"#{OrderId:D6}";
        public string StatusClass => Status.ToLower() switch
        {
            "pending" => "status-pending",
            "shipping" => "status-shipping",
            "delivered" => "status-delivered", 
            "cancelled" => "status-cancelled",
            _ => "status-pending"
        };
    
        // Helper to determine if order is "new" (not delivered/cancelled)
        public bool IsNewOrder => Status.ToLower() != "delivered" && Status.ToLower() != "cancelled";
    }
}
