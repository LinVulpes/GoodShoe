using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodShoe.Models;
using GoodShoe.Data;

namespace GoodShoe.Controllers
{
    public class AdminController : Controller
    {

        // Establish the context
        private GoodShoeContext context {  get; set; }
        public AdminController(GoodShoeContext ctx)
        {
            context = ctx;
        }

        // Landing page for admin view
        public IActionResult Index()
        {
            return View();
        }

        // Product List
        public IActionResult ProdList()
        {
            var prod = context.Product
                .OrderBy(p => p.Name)
                .ToList();
            return View(prod);
        }

        // Order List
        public IActionResult OrderList()
        {
            return View();
        }


        // Add new product. Opens the edit view but with a new product

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Action = "Create";
            return View("Edit", new Product());
        }

        // Edit product details

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            ViewBag.Action = "Edit";
            var prod = context.Product.Find(Id);
            return View(prod);
        }

        [HttpPost]
        public IActionResult Edit(Product prod)
        {
            if(ModelState.IsValid)
            {
                if (prod.Id == 0)
                    context.Product.Add(prod);
                else
                    context.Product.Update(prod);
                context.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.Action = (prod.Id == 0) ? "Add" : "Edit";
                return View(prod);
            }
        }

        // Delete product

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var prod = context.Product.Find(Id);
            return View(prod);
        }
        [HttpPost]
        public IActionResult Delete(Product prod)
        {
            context.Product.Remove(prod);
            context.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }

        public IActionResult Details()
        {
            return View();
        }

    }
}
