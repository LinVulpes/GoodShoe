using Microsoft.AspNetCore.Mvc;
using GoodShoe.ViewModels;
using GoodShoe.Extensions; // For session extension methods

namespace GoodShoe.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        public IActionResult Checkout()
        {
            System.Diagnostics.Debug.WriteLine("=== OrderController.Checkout (GET) called ===");

            var cartItems = GetCartItems();
            System.Diagnostics.Debug.WriteLine($"OrderController: Retrieved {cartItems.Count} items for checkout");

            // Check if cart is empty
            if (!cartItems.Any())
            {
                System.Diagnostics.Debug.WriteLine("OrderController: Cart is empty, redirecting back to cart");
                TempData["CartError"] = "Your cart is empty. Please add items before checkout.";
                return RedirectToAction("Index", "Cart");
            }

            System.Diagnostics.Debug.WriteLine("OrderController: Creating checkout view model");
            var model = new CheckoutViewModel
            {
                CartItems = cartItems
            };

            // Debug: Print cart items in the model
            foreach (var item in model.CartItems)
            {
                System.Diagnostics.Debug.WriteLine($"Model Item: {item.ProductName}, Qty: {item.Quantity}, Size: {item.Size}, Price: {item.Price}");
            }

            System.Diagnostics.Debug.WriteLine("OrderController: Returning checkout view");
            return View(model);
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Clear cart from session after successful order
                HttpContext.Session.Remove("CartItems");
                TempData["OrderSuccess"] = "Order placed successfully!";
                return RedirectToAction("Confirmation");
            }

            // Re-populate cart items if validation fails
            model.CartItems = GetCartItems();
            return View(model);
        }

        public IActionResult Confirmation()
        {
            return View();
        }

        private List<CartItemViewModel> GetCartItems()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("=== GetCartItems called ===");

                // Check if session exists
                if (HttpContext.Session == null)
                {
                    System.Diagnostics.Debug.WriteLine("Session is null!");
                    return new List<CartItemViewModel>();
                }

                // Try to get the raw session value first
                var rawSessionValue = HttpContext.Session.GetString("CartItems");
                System.Diagnostics.Debug.WriteLine($"Raw session value: {rawSessionValue ?? "NULL"}");

                // Get cart items from session (set by CartController.PrepareCheckout)
                var cartItems = HttpContext.Session.Get<List<CartItemViewModel>>("CartItems");

                if (cartItems != null && cartItems.Any())
                {
                    System.Diagnostics.Debug.WriteLine($"Successfully retrieved {cartItems.Count} items from session");
                    foreach (var item in cartItems)
                    {
                        System.Diagnostics.Debug.WriteLine($"Session Item: {item.ProductName}, Qty: {item.Quantity}, Size: {item.Size}");
                    }
                    return cartItems;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No cart items found in session or cartItems is null/empty");
                    return new List<CartItemViewModel>();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"=== ERROR in GetCartItems ===");
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                return new List<CartItemViewModel>();
            }
        }
    }
}