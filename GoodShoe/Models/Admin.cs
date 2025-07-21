using System.ComponentModel.DataAnnotations;

namespace GoodShoe.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Phone { get; set; }

        public DateTime? DOB { get; set; }

        [StringLength(10)]
        public string Currency { get; set; } = "SGD";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Helper properties
        public string DisplayPhone => string.IsNullOrEmpty(Phone) ? "Not provided" : Phone;
        public string DisplayDOB => DOB?.ToString("MMM dd, yyyy") ?? "Not provided";
        public int Age => DOB.HasValue ? DateTime.Now.Year - DOB.Value.Year : 0;
    }
}