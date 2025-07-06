// Import the Cart view models so we know what data types the methods will work with
using GoodShoe.ViewModels;
// Import List<T> so we can return collections of items
using System.Collections.Generic;

// Defines a namespace for grouping related cart service code
namespace GoodShoe.Services
{
    // Interface: defines a contract for any CartService implementation
    public interface ICartService
    {
        /// <summary>
        /// Add an item to the shopping cart.
        /// </summary>
        /// <param name="productId">The ID of the product being added.</param>
        /// <param name="size">The chosen size (e.g., "US 9").</param>
        /// <param name="quantity">How many of this item to add.</param>
        void AddToCart(int productId, string size, int quantity);

        /// <summary>
        /// Retrieve all items currently in the cart.
        /// </summary>
        /// <returns>A list of CartItemViewModel objects representing the cart contents.</returns>
        List<CartItemViewModel> GetCartItems();

        /// <summary>
        /// Remove a specific item (by product and size) from the cart.
        /// </summary>
        /// <param name="productId">The ID of the product to remove.</param>
        /// <param name="size">The size variant to remove.</param>
        void RemoveFromCart(int productId, string size);

        /// <summary>
        /// Empty the entire shopping cart.
        /// </summary>
        void ClearCart();
    }
}