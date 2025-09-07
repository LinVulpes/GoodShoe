using GoodShoe.ViewModels;           // Brings in CartItemViewModel
using Newtonsoft.Json;               // For JSON serialization/deserialization
using GoodShoe.Data;                // For GoodShoeDbContext (database context)

namespace GoodShoe.Services
{
    public class CartService : ICartService
    {
        //fields & dependencies
        private readonly GoodShoeDbContext _db; // The database context to look up product details
        private const string SessionKey = "CartSession"; // Key under which the cart is stored in session
        private readonly IHttpContextAccessor _httpContextAccessor; // Allows us to access the user's HTTP session
        private ISession Session => _httpContextAccessor.HttpContext.Session; // Shortcut to get the current session

        // Constructor: ASP.NET will inject the session accessor and database context here
        public CartService(
            IHttpContextAccessor httpContextAccessor,
            GoodShoeDbContext db 
        )
        {
            _httpContextAccessor = httpContextAccessor; // Store session accessor
            _db = db;    // Store database context
        }

        private List<CartItemViewModel> LoadCart()
        {
            var json = Session.GetString(SessionKey); // Get JSON string from session
            return string.IsNullOrEmpty(json)
                ? new List<CartItemViewModel>()  // No JSON? return empty cart
                : JsonConvert.DeserializeObject<List<CartItemViewModel>>(json);  // Parse JSON to list
        }

        // Save the current cart list back into session as JSON
        private void SaveCart(List<CartItemViewModel> cart)
            => Session.SetString(SessionKey, JsonConvert.SerializeObject(cart));

        // Public method to add an item to the cart
        public void AddToCart(int productId, string size, int quantity)
        {
            var cart = LoadCart(); // Get current cart items
            
            // Try to find an existing item with same product and size
            var existing = cart.FirstOrDefault(i =>
                i.ProductID == productId &&
                i.Size == size
            );

            if (existing != null)
            {
                // Calculate new quantity
                var newQuantity = existing.Quantity + quantity;
                
                // Debug logging (remove in production)
                System.Diagnostics.Debug.WriteLine($"Existing item found. Current: {existing.Quantity}, Adding: {quantity}, New: {newQuantity}");
                
                if (newQuantity <= 0)
                {
                    // Remove item if quantity becomes 0 or less
                    cart.Remove(existing);
                    System.Diagnostics.Debug.WriteLine($"Item removed from cart. ProductID: {productId}");
                }
                else
                {
                    // Update quantity (stock validation should happen in controller)
                    existing.Quantity = newQuantity;
                    System.Diagnostics.Debug.WriteLine($"Item quantity updated to: {existing.Quantity}");
                }
            }
            else if (quantity > 0) // Only add new item if quantity is positive
            {
                // If new item, look up product info from database
                var product = _db.Product // Table or DbSet<Product>
                                 .FirstOrDefault(p => p.ProductId == productId);

                if (product == null)
                    throw new KeyNotFoundException($"No product with ID {productId}");

                // Create new CartItemViewModel with all details
                var newItem = new CartItemViewModel
                {
                    ProductID = productId,
                    ProductName = product.Name,  // Product name
                    ImageUrl = !string.IsNullOrEmpty(product.ImageUrl)
                                  ? product.ImageUrl
                                  : $"/Products/GetImage/{product.ProductId}", // Updated to match your image handling
                    Price = product.Price, // Product price
                    Size = size,           // Selected size (e.g. "US 9")
                    Quantity = quantity    // Start with requested qty
                };

                cart.Add(newItem);
                
                System.Diagnostics.Debug.WriteLine($"New item added to cart. ProductID: {productId}, Quantity: {quantity}");
            }
            else
            {
                // Attempting to add negative quantity for non-existing item - do nothing
                System.Diagnostics.Debug.WriteLine($"Attempted to add negative quantity ({quantity}) for non-existing item. ProductID: {productId}");
            }

            // Write updated cart back to session
            SaveCart(cart);
            
            // Debug: Print current cart state
            System.Diagnostics.Debug.WriteLine($"Cart now has {cart.Count} items");
        }

        // Return the list of cart items for display
        public List<CartItemViewModel> GetCartItems() => LoadCart();

        // Remove all items matching productId and size
        public void RemoveFromCart(int productId, string size)
        {
            var cart = LoadCart();
            var removed = cart.RemoveAll(i => i.ProductID == productId && i.Size == size);
            
            System.Diagnostics.Debug.WriteLine($"Removed {removed} item(s) from cart. ProductID: {productId}, Size: {size}");
            
            SaveCart(cart);  // Save the cleaned-up cart
        }

        // Clear the entire cart from session
        public void ClearCart()
        {
            Session.Remove(SessionKey);
            System.Diagnostics.Debug.WriteLine("Cart cleared from session");
        }

        // Get current quantity of a specific item in cart
        public int GetCartItemQuantity(int productId, string size)
        {
            var cart = LoadCart();
            var item = cart.FirstOrDefault(i => i.ProductID == productId && i.Size == size);
            return item?.Quantity ?? 0;
        }
    }
}