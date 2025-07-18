using System.ComponentModel.DataAnnotations;

namespace GoodShoe.ViewModels
{
    public class CheckoutViewModel
    {
        // Delivery Information
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Street Address & Postcode")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; } = string.Empty;

        // Payment Information
        [Required]
        public string PaymentMethod { get; set; } = "Credit / Debit Card";

        [Display(Name = "Name on Card")]
        public string NameOnCard { get; set; } = string.Empty;

        [Display(Name = "Card Number")]
        public string CardNumber { get; set; } = string.Empty;

        [Display(Name = "MM/YY")]
        public string ExpiryDate { get; set; } = string.Empty;

        [Display(Name = "CVV")]
        public string CVV { get; set; } = string.Empty;

        // Cart Items
        public List<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();

        // Calculated totals
        public decimal Subtotal => CartItems.Sum(i => i.Price * i.Quantity);
        public decimal DeliveryFee => 20.00m;
        public decimal Total => Subtotal + DeliveryFee;

    }
}