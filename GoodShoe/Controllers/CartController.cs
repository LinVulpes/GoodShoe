using GoodShoe.ViewModels;
using GoodShoe.Services;
using Microsoft.AspNetCore.Mvc;
using GoodShoe.Extensions;

namespace GoodShoe.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;

        // Constructor comes first
        public CartController(ICartService cartService, IAuthService authService)
        {
            _cartService = cartService;
            _authService = authService;
        }
        // Latest Index for Cart Controller
        public IActionResult Index()
        {
            var cartItems = _cartService.GetCartItems();
            return View(cartItems);
        }        

        [HttpPost]
        public IActionResult AddToCart(int productId, string size, int quantity = 1)
        {
            // Debugging method to check whether the item is added to the cart or not
            try
            {
                // Debug logging
                System.Diagnostics.Debug.WriteLine($"AddToCart called: ProductID={productId}, Size={size}, Quantity={quantity}");

                // Call the service with the quantity (default 1 if not specified)
                _cartService.AddToCart(productId, size, quantity);

                // Debug: Check cart contents after operation
                var cartItems = _cartService.GetCartItems();
                System.Diagnostics.Debug.WriteLine($"Cart after operation has {cartItems.Count} items");

                // Add success message based on action
                if (quantity == 1)
                {
                    TempData["CartMessage"] = "Item added to cart!";
                }
                else if (quantity > 1)
                {
                    TempData["CartMessage"] = "Item quantity increased!";
                }
                else if (quantity < 0)
                {
                    TempData["CartMessage"] = "Item quantity decreased!";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in AddToCart: {ex.Message}");
                TempData["CartError"] = $"Error updating cart: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId, string size)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"RemoveFromCart called: ProductID={productId}, Size={size}");

                _cartService.RemoveFromCart(productId, size);

                TempData["CartMessage"] = "Item removed from cart!";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in RemoveFromCart: {ex.Message}");
                TempData["CartError"] = "Error removing item. Please try again.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult PrepareCheckout()
        {
            try
            {
                var items = _cartService.GetCartItems();

                if (!items.Any())
                {
                    TempData["CartError"] = "Your cart is empty.";
                    return RedirectToAction("Index");
                }
                
                // Check if the customer is login or not.
                var currentCustomer = _authService.GetCurrentCustomer();
                if (currentCustomer == null)
                {
                    TempData["Message"] = "Please login to complete your purchase.";
                    return RedirectToAction("Login", "Account");
                }

                // Save to session using the extension method
                HttpContext.Session.Set("CartItems", items);

                return RedirectToAction("Checkout", "Order");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in PrepareCheckout: {ex.Message}");
                TempData["CartError"] = "Error preparing checkout. Please try again.";
                return RedirectToAction("Index");
            }
        }
    }

}