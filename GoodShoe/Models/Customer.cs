using System.ComponentModel.DataAnnotations;

namespace GoodShoe.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual Cart? Cart { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        // Helper properties
        public string FullName => $"{FirstName} {LastName}";
        public string DisplayAddress => string.IsNullOrEmpty(Address) ? "No address provided" : Address;
        public string MemberSince => CreatedAt.ToString("MMMM yyyy");
    }
}