using System.ComponentModel.DataAnnotations;

namespace GoodShoe.Models
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Phone { get; set; }

        public DateTime? DOB { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [StringLength(10)]
        public string Currency { get; set; } = "SGD";

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Helper properties
        public string DisplayPhone => string.IsNullOrEmpty(Phone) ? "Not provided" : Phone;
        public string DisplayDOB => DOB?.ToString("MMM dd, yyyy") ?? "Not provided";
        public int Age => DOB.HasValue ? DateTime.Now.Year - DOB.Value.Year : 0;
    }
}