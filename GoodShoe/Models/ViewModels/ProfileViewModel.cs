using System.ComponentModel.DataAnnotations;

namespace GoodShoe.ViewModels
{
    public class ProfileViewModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "Location")]
        public string? Location { get; set; }
        
        // For Purchase history
        public List<PurchaseHistoryItem>? PurchaseHistory { get; set; }
    }
    
    public class PurchaseHistoryItem
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
