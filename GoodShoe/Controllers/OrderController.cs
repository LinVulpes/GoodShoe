using Microsoft.AspNetCore.Mvc;
using GoodShoe.ViewModels;

namespace GoodShoe.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        public IActionResult Checkout()
        {
            var cartItems = GetCartItems();
            var model = new CheckoutViewModel
            {
                CartItems = cartItems
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["OrderSuccess"] = "Order placed successfully!";
                return RedirectToAction("Confirmation");
            }

            model.CartItems = GetCartItems();
            return View(model);
        }

        public IActionResult Confirmation()
        {
            return View();
        }

        private List<CartItemViewModel> GetCartItems()
        {
            // Replace with your actual cart logic
            return new List<CartItemViewModel>();
        }
    }
}