using System.ComponentModel.DataAnnotations;

namespace GoodShoe.ViewModels
{
    public class CheckoutViewModel
    {
        // Estimate delivery dates 3-5 days (not including weekends)
        // helper to count only Mon–Fri
        private DateTime AddBusinessDays(DateTime from, int days)
        {
            var current = from;
            int added = 0;
            while (added < days)
            {
                current = current.AddDays(1);
                if (current.DayOfWeek != DayOfWeek.Saturday
                    && current.DayOfWeek != DayOfWeek.Sunday)
                {
                    added++;
                }
            }
            return current;
        }

        // computed property for the view
        public string DeliveryDateRange
        {
            get
            {
                var start = AddBusinessDays(DateTime.Now, 3);
                var end = AddBusinessDays(DateTime.Now, 5);
                return $"{start:ddd, MMM dd} – {end:ddd, MMM dd}";
            }
        }


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