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
            // Admin Dashboard statistics
            var totalProducts = context.Product.Count();
            var lowStockProducts = context.Product.Where(p => p.StockCount < 5).Count();
            var outOfStockProducts = context.Product.Where(p => p.StockCount == 0).Count();
            var totalValue = context.Product.Sum(p => p.Price * p.StockCount);
            
            ViewBag.TotalProducts = totalProducts;
            ViewBag.LowStockProducts = lowStockProducts;
            ViewBag.OutOfStockProducts = outOfStockProducts;
            ViewBag.TotalValue = totalValue;
            
            // For Order - to implement later
            ViewBag.TotalOrders = 15;
            ViewBag.PendingOrders = 3;
            
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
            return View("Create", new Product());
        }

        // Edit product details

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            ViewBag.Action = "Edit";
            var prod = context.Product.Find(Id);
            if (prod == null)
                return NotFound();
            return View(prod);
        }

        [HttpPost]
        public IActionResult Edit(Product prod, string[] selectedSizes)
        {
            /*// Size Selection
            if (selectedSizes != null && selectedSizes.Length > 0)
            {
                prod.AvailableSizes = string.Join(",", selectedSizes);
            }
            else
            {
                prod.AvailableSizes = "";
            }*/
            
            if(ModelState.IsValid)
            {
                if (prod.Id == 0)
                    context.Product.Add(prod);
                else
                    context.Product.Update(prod);
                context.SaveChanges();
                return RedirectToAction("ProdList", "Admin");
            }
            else
            {
                ViewBag.Action = (prod.Id == 0) ? "Create" : "Edit";
                return View(prod);
            }
        }

        // Delete product
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var prod = context.Product.Find(Id);
            if (prod == null)
                return NotFound();
            return View(prod);
        }
        
        [HttpPost]
        public IActionResult Delete(Product prod)
        {
            context.Product.Remove(prod);
            context.SaveChanges();
            return RedirectToAction("ProdList", "Admin");
        }

        // Product Details
        public IActionResult Details(int Id)
        {
            var prod = context.Product.Find(Id);
            if (prod == null)
                return NotFound();
            return View();
        }

    }
}
