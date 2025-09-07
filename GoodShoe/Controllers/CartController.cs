using GoodShoe.Services;
using Microsoft.AspNetCore.Mvc;
using GoodShoe.Extensions;
using GoodShoe.Data;
using Microsoft.EntityFrameworkCore;

namespace GoodShoe.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;
        private readonly GoodShoeDbContext _context;

        // Constructor comes first
                public CartController(ICartService cartService, IAuthService authService, GoodShoeDbContext context)
        {
            _cartService = cartService;
            _authService = authService;
            _context = context;
        }

        // Latest Index for Cart Controller
        public IActionResult Index()
        {
            var cartItems = _cartService.GetCartItems();
            return View(cartItems);
        }        

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, string size, int quantity = 1)
        {
            try
            {
                // Debug logging
                System.Diagnostics.Debug.WriteLine($"AddToCart called: ProductID={productId}, Size={size}, Quantity={quantity}");

                // Parse size to get numeric value
                var sizeNumber = int.Parse(size.Replace("US ", ""));
                
                // Get the product variant and check stock
                var productVariant = await _context.ProductVariant
                    .Include(pv => pv.Product)
                    .FirstOrDefaultAsync(pv => pv.Product.ProductId == productId && pv.Size == sizeNumber);

                if (productVariant == null)
                {
                    TempData["CartError"] = "Product variant not found.";
                    return RedirectToAction("Index");
                }

                // Get current cart items to check existing quantity
                var cartItems = _cartService.GetCartItems();
                var existingItem = cartItems.FirstOrDefault(ci => ci.ProductID == productId && ci.Size == size);
                var currentCartQuantity = existingItem?.Quantity ?? 0;
                var newTotalQuantity = currentCartQuantity + quantity;

                // Check if the new total quantity would exceed available stock
                if (newTotalQuantity > productVariant.StockCount)
                {
                    var availableToAdd = productVariant.StockCount - currentCartQuantity;
                    if (availableToAdd <= 0)
                    {
                        TempData["CartError"] = $"Cannot add more items. Maximum available stock ({productVariant.StockCount}) already in cart.";
                    }
                    else
                    {
                        TempData["CartError"] = $"Only {availableToAdd} more item(s) can be added. Available stock: {productVariant.StockCount}, Current in cart: {currentCartQuantity}";
                    }
                    return RedirectToAction("Index");
                }

                // If we're removing quantity (negative), no need to check stock
                if (quantity < 0 && newTotalQuantity < 0)
                {
                    // This would remove the item entirely, which is fine
                    newTotalQuantity = 0;
                }

                // Call the service with the quantity
                _cartService.AddToCart(productId, size, quantity);

                // Debug: Check cart contents after operation
                var updatedCartItems = _cartService.GetCartItems();
                System.Diagnostics.Debug.WriteLine($"Cart after operation has {updatedCartItems.Count} items");

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

        // New method to get stock information for AJAX calls
        [HttpGet]
        public async Task<IActionResult> GetStockInfo(int productId, string size)
        {
            try
            {
                var sizeNumber = int.Parse(size.Replace("US ", ""));
                
                var productVariant = await _context.ProductVariant
                    .FirstOrDefaultAsync(pv => pv.Product.ProductId == productId && pv.Size == sizeNumber);
                    
                if (productVariant == null)
                {
                    return Json(new { success = false, message = "Product variant not found." });
                }

                // Get current cart quantity
                var cartItems = _cartService.GetCartItems();
                var existingItem = cartItems.FirstOrDefault(ci => ci.ProductID == productId && ci.Size == size);
                var currentCartQuantity = existingItem?.Quantity ?? 0;
                var availableToAdd = Math.Max(0, productVariant.StockCount - currentCartQuantity);
                
                return Json(new { 
                    success = true,
                    totalStock = productVariant.StockCount,
                    currentCartQuantity = currentCartQuantity,
                    availableToAdd = availableToAdd,
                    canIncrease = availableToAdd > 0
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error getting stock information." });
            }
        }
    }
}