using GoodShoe.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using GoodShoe.Data;

namespace GoodShoe.Services
{
    public class CartService : ICartService
    {
        private readonly GoodShoeContext _db;
        private const string SessionKey = "CartSession";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext.Session;

        public CartService(
            IHttpContextAccessor httpContextAccessor,
            GoodShoeContext db
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _db = db;    // ← assign the injected context
        }

        private List<CartItem> LoadCart()
        {
            var json = Session.GetString(SessionKey);
            return string.IsNullOrEmpty(json)
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(json);
        }

        private void SaveCart(List<CartItem> cart)
            => Session.SetString(SessionKey, JsonConvert.SerializeObject(cart));

        public void AddToCart(int productId, string size, int quantity)
        {
            var cart = LoadCart();
            // both sides are strings now
            var existing = cart.FirstOrDefault(i =>
                i.ProductId == productId &&
                i.Size == size
            );

            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                // LOOK UP the real product details
                var product = _db.Product
                                 .FirstOrDefault(p => p.Id == productId);

                if (product == null)
                    throw new KeyNotFoundException($"No product with ID {productId}");

                cart.Add(new CartItem
                {
                    ProductId = productId,
                    Name = product.Name,
                    ImageUrl = !string.IsNullOrEmpty(product.ImageUrl)
                                  ? product.ImageUrl
                                  : $"/images/products/{product.Id}.png",
                    Price = product.Price,
                    Size = size,
                    Quantity = quantity
                });
            }

            SaveCart(cart);
        }

        public List<CartItem> GetCartItems() => LoadCart();

        public void RemoveFromCart(int productId, string size)
        {
            var cart = LoadCart();
            cart.RemoveAll(i => i.ProductId == productId && i.Size == size);
            SaveCart(cart);
        }

        public void ClearCart()
        {
            Session.Remove(SessionKey);
        }
    }
}
