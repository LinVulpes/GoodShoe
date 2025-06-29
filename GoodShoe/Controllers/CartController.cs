using GoodShoe.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GoodShoe.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var cart = GetCart(); // should never be null!
            return View(cart);
        }

        private List<CartItem> GetCart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
            return cart ?? new List<CartItem>(); // <-- this ensures you never return null
        }
    }
}
