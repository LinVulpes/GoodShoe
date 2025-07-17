using GoodShoe.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GoodShoe.Services;
using GoodShoe.ViewModels;
using GoodShoe.Models; // Assuming your Order and OrderItem models are here

namespace GoodShoe.Controllers
{
    [Authorize] // Ensure only logged-in users can check out
    public class OrderController : Controller
    {
        private readonly ICartService _cartService;
        private readonly GoodShoeDbContext _db;

        public OrderController(ICartService cartService, GoodShoeDbContext db)
        {
            _cartService = cartService;
            _db = db;
        }

        // GET: /Order/Checkout
        [HttpGet]
        public IActionResult Checkout()
        {
            var cartItems = _cartService.GetCartItems();

            if (!cartItems.Any())
            {
                TempData["Error"] = "Your cart is empty.";
                return RedirectToAction("Cart", "Cart");
            }

            var model = new CheckoutViewModel
            {
                CartItems = cartItems
            };

            return View(model);
        }

        // POST: /Order/SubmitCheckout
        [HttpPost]
        public IActionResult SubmitCheckout(CheckoutViewModel model)
        {
            var cartItems = _cartService.GetCartItems();

            if (!ModelState.IsValid || !cartItems.Any())
            {
                model.CartItems = cartItems;
                if (!cartItems.Any())
                    TempData["Error"] = "Your cart is empty.";
                return View("Checkout", model);
            }

            // Create new Order
            var order = new Order
            {
                UserId = User.Identity.Name, // Or store User ID from claims
                FirstName = model.FirstName,
                LastName = model.LastName,
                ShippingAddress = model.ShippingAddress,
                Postcode = model.Postcode,
                ContactNumber = model.ContactNumber,
                PaymentMethod = model.PaymentMethod,
                OrderDate = DateTime.Now,
                TotalAmount = model.Total,
                Items = cartItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductID,
                    ProductName = item.ProductName,
                    Size = item.Size,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };

            // Save order to database
            _db.Orders.Add(order);
            _db.SaveChanges();

            // Clear cart from session
            _cartService.ClearCart();

            return RedirectToAction("OrderConfirmation");
        }

        // GET: /Order/OrderConfirmation
        [HttpGet]
        public IActionResult OrderConfirmation()
        {
            return View();
        }
    }
}
