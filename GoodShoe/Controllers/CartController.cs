using Microsoft.AspNetCore.Mvc;

namespace GoodShoe.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
