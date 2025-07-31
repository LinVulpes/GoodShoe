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

        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;

        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        public DateTime? DOB { get; set; }

        [StringLength(10)]
        public string Currency { get; set; } = "SGD";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Helper properties
        public string DisplayPhone => string.IsNullOrEmpty(Phone) ? "Not provided" : Phone;
    }
}
