using GoodShoe.Models.ViewModels;
using System.Collections.Generic;

namespace GoodShoe.Services
{
    public interface ICartService
    {
        void AddToCart(int productId, string size, int quantity);
        List<CartItem> GetCartItems();
        void RemoveFromCart(int productId, string size);
        void ClearCart();
    }
}
