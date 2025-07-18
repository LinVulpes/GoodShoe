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
            // Test data - need to connect with database later for actual data
            return new List<CartItemViewModel>
            {
                new CartItemViewModel
                {
                    ProductID = 1,
                    ProductName = "GoodShoe 0.1",
                    Price = 100.00m,
                    Quantity = 1,
                    Size = "9",
                    ImageUrl = "https://via.placeholder.com/150x150/8B4513/FFFFFF?text=Shoe+1"
                },
                new CartItemViewModel
                {
                    ProductID = 2,
                    ProductName = "GoodShoe 0.3",
                    Price = 100.00m,
                    Quantity = 1,
                    Size = "10",
                    ImageUrl = "https://via.placeholder.com/150x150/2F4F4F/FFFFFF?text=Shoe+2"
                }
            };
        }
    }
}