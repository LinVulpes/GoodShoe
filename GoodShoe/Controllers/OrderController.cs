using Microsoft.AspNetCore.Mvc; //Brings in ASP .NET Core MVC types
using GoodShoe.ViewModels; //view-model classes
using GoodShoe.Extensions; // For session extension methods
using GoodShoe.Data;
using GoodShoe.Models; //to find the GoodShoeDbContext class
using GoodShoe.Services; //to "see" the ICartService interface

namespace GoodShoe.Controllers
{
    public class OrderController : Controller
    {
        private readonly GoodShoeDbContext _db;
        private readonly ICartService _cartService;

        public OrderController(GoodShoeDbContext db, ICartService cartService)
        {
            _db = db;
            _cartService = cartService;
        }

        //handle GET requests to /Order/Checkout
        [HttpGet]
        public IActionResult Checkout()
        {
            //calls trace flow and data
            System.Diagnostics.Debug.WriteLine("=== OrderController.Checkout (GET) called ===");

            //pull the list of items from session
            var cartItems = GetCartItems();
            System.Diagnostics.Debug.WriteLine($"OrderController: Retrieved {cartItems.Count} items for checkout");

            // Check if cart is empty
            // If zero items, display "Your cart is empty. Please add items before checkout."
            // Redirects to CartController.Index so the user can add stuff
            if (!cartItems.Any())
            {
                System.Diagnostics.Debug.WriteLine("OrderController: Cart is empty, redirecting back to cart");
                TempData["CartError"] = "Your cart is empty. Please add items before checkout.";
                return RedirectToAction("Index", "Cart");
            }

            //Create a new ?checkout? object and stick in the list of items from the cart.
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
                try
                {
                    // Get Cart Items from session
                    var cartItems = GetCartItems();

                    if (!cartItems.Any())
                    {
                        TempData["CartError"] = "Your cart is empty. Please add items before checkout.";
                        return RedirectToAction("Index", "Cart");
                    }

                    // Create new order
                    var order = new Order()
                    {
                        CustomerId = 1, // For now, use a default customer ID. In real app, get from authentication
                        TotalAmount = model.Total,
                        Status = "Pending",
                        Address = $"{model.FirstName} {model.LastName}, {model.Address}",
                        PaymentMethod = model.PaymentMethod,
                        PaymentStatus = "Completed", // Mock payment as completed
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    // Add order to database
                    _db.Orders.Add(order);
                    _db.SaveChanges();

                    // Create order items - FIXED: Use 'item' instead of 'cartItems'
                    foreach (var item in cartItems)
                    {
                        // Find the ProductVariant that matches the product and size
                        var productVariant = _db.ProductVariant
                            .FirstOrDefault(pv => pv.ProductId == item.ProductID &&
                                                  pv.Size == int.Parse(item.Size.Replace("US ", "")));

                        if (productVariant == null)
                        {
                            throw new Exception($"Product variant not found for Product ID {item.ProductID} and size {item.Size}");
                        }

                        var orderItem = new OrderItem
                        {
                            OrderId = order.OrderId,
                            ProductVariantId = productVariant.Id, // Use the correct ProductVariant ID
                            ProductName = item.ProductName,        // Use 'item' not 'cartItems'
                            Size = int.Parse(item.Size.Replace("US ", "")), // Use 'item' not 'cartItems'
                            Quantity = item.Quantity,              // Use 'item' not 'cartItems'
                            UnitPrice = item.Price,                // Use 'item' not 'cartItems'
                            TotalPrice = item.Price * item.Quantity // Use 'item' not 'cartItems'
                        };
                        _db.OrderItems.Add(orderItem); // Use OrderItems (plural) to match your DbContext
                    }

                    _db.SaveChanges();

                    // Clear cart from session after successful order
                    HttpContext.Session.Remove("CartItems");
                    _cartService.ClearCart();

                    // Store order ID for confirmation page
                    TempData["OrderId"] = order.OrderId;
                    TempData["OrderSuccess"] = "Order placed successfully!";

                    System.Diagnostics.Debug.WriteLine($"Order created successfully with ID: {order.OrderId}");
                    return RedirectToAction("Confirmation");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error creating order: {ex.Message}");
                    TempData["CartError"] = "Error processing your order. Please try again.";

                    // Re-populate cart items if order creation fails
                    model.CartItems = GetCartItems();
                    return View(model);
                }
            }
            // Re-populate cart items if validation fails
            model.CartItems = GetCartItems();
            return View(model);
        }

        public IActionResult Confirmation()
        {
            var orderId = TempData["OrderId"];
            if (orderId == null)
            {
                return RedirectToAction("Index", "Cart");
            }
            ViewBag.OrderId = orderId;
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
            //error handling
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"=== ERROR in GetCartItems ===");
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                return new List<CartItemViewModel>(); //return empty list so app don't crash
            }
        }
    }
}