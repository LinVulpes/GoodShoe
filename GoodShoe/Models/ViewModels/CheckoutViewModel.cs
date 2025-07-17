using GoodShoe.ViewModels;

namespace GoodShoe.Models.ViewModels;

public class CheckoutViewModel
{
    // Delivery Info
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ShippingAddress { get; set; }
    public string Postcode { get; set; }
    public string ContactNumber { get; set; }

    // Payment Info
    public string PaymentMethod { get; set; }
    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string Expiry { get; set; }
    public string CVV { get; set; }

    // Cart Info
    public List<CartItemViewModel> CartItems { get; set; }
    public decimal Subtotal => CartItems.Sum(i => i.Price * i.Quantity);
    public decimal DeliveryFee => 20;
    public decimal Total => Subtotal + DeliveryFee;
}
