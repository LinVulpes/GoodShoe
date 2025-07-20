using System.ComponentModel.DataAnnotations;

namespace GoodShoe.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}