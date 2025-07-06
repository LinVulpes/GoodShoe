using GoodShoe.Models.ViewModels;
using GoodShoe.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoodShoe.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        // Constructor comes first
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // KEEP THIS Index — it pulls from your ICartService
        public IActionResult Index()
        {
            var items = _cartService.GetCartItems();
            return View(items); // expects List<CartItem>
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, string size)
        {
            _cartService.AddToCart(productId, size, 1);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId, string size)
        {
            _cartService.RemoveFromCart(productId, size);
            return RedirectToAction("Index");
        }
    }
}
