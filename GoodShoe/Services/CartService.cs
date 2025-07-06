using GoodShoe.Models.ViewModels;   // Brings in CartItem model
using Microsoft.AspNetCore.Http;    // For accessing user session
using Newtonsoft.Json;               // For JSON serialization/deserialization
using System.Collections.Generic;   // For List<T>
using System.Linq;                  // For LINQ methods like FirstOrDefault
using GoodShoe.Data;                // For GoodShoeContext (database contex

namespace GoodShoe.Services
{
    public class CartService : ICartService
    {
        //fields & dependencies
        private readonly GoodShoeContext _db; // The database context to look up product details
        private const string SessionKey = "CartSession"; // Key under which the cart is stored in session
        private readonly IHttpContextAccessor _httpContextAccessor; // Allows us to access the user's HTTP session
        private ISession Session => _httpContextAccessor.HttpContext.Session; // Shortcut to get the current session

        // Constructor: ASP.NET will inject the session accessor and database context here
        public CartService(
            IHttpContextAccessor httpContextAccessor,
            GoodShoeContext db 
        )
        {
            _httpContextAccessor = httpContextAccessor; // Store session accessor
            _db = db;    // Store database context
        }

        private List<CartItem> LoadCart()
        {
            var json = Session.GetString(SessionKey); // Get JSON string from session
            return string.IsNullOrEmpty(json)
                ? new List<CartItem>()  // No JSON? return empty cart
                : JsonConvert.DeserializeObject<List<CartItem>>(json);  // Parse JSON to list
        }

        // Save the current cart list back into session as JSON
        private void SaveCart(List<CartItem> cart)
            => Session.SetString(SessionKey, JsonConvert.SerializeObject(cart));

        // Public method to add an item to the cart
        public void AddToCart(int productId, string size, int quantity)
        {
            var cart = LoadCart(); // Get current cart items
            
            // Try to find an existing item with same product and size
            var existing = cart.FirstOrDefault(i =>
                i.ProductId == productId &&
                i.Size == size
            );

            if (existing != null)
            {
                // If item exists, just increase its quantity
                existing.Quantity += quantity;
            }
            else
            {
                // If new item, look up product info from database
                var product = _db.Product // Table or DbSet<Product>
                                 .FirstOrDefault(p => p.Id == productId);

                if (product == null)
                    throw new KeyNotFoundException($"No product with ID {productId}");

                // Create new CartItem with all details
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    Name = product.Name,  // Product name
                    ImageUrl = !string.IsNullOrEmpty(product.ImageUrl)
                                  ? product.ImageUrl
                                  : $"/images/products/{product.Id}.png",
                    Price = product.Price, // Product price
                    Size = size,           // Selected size (e.g. "US 9")
                    Quantity = quantity    // Start with requested qty
                });
            }

            // Write updated cart back to session
            SaveCart(cart);
        }


        // Return the list of cart items for display
        public List<CartItem> GetCartItems() => LoadCart();

        // Remove all items matching productId and size
        public void RemoveFromCart(int productId, string size)
        {
            var cart = LoadCart();
            cart.RemoveAll(i => i.ProductId == productId && i.Size == size);
            SaveCart(cart);  // Save the cleaned-up cart
        }

        // Clear the entire cart from session
        public void ClearCart()
        {
            Session.Remove(SessionKey);
        }
    }
}
