using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodShoe.Models;
using GoodShoe.Data;

namespace GoodShoe.Controllers
{
    public class AdminController : Controller
    {

        // Establish the context
        private GoodShoeDbContext context {  get; set; }
        public AdminController(GoodShoeDbContext ctx)
        {
            context = ctx;
        }

        // Landing page for admin view
        public IActionResult Index()
        {
            // Admin Dashboard statistics Updated
            var totalProducts = context.Product.Count();

            var totalStock = context.ProductVariant.Sum(p => p.StockCount);
            var lowStockProducts = context.ProductVariant.Where(pv => pv.StockCount > 0 && pv.StockCount < 5).Count();
            var outOfStockProducts = context.ProductVariant.Where(p => p.StockCount == 0).Count();
            
            // Calculate total value from variants
            var totalValue = context.ProductVariant
                .Include(pv => pv.Product)
                .Sum(pv => pv.Product.Price * pv.StockCount);
            
            ViewBag.TotalProducts = totalProducts;
            ViewBag.TotalStock = totalStock;
            ViewBag.LowStockProducts = lowStockProducts;
            ViewBag.OutOfStockProducts = outOfStockProducts;
            ViewBag.TotalValue = totalValue;
            
            // For Order Management - to implement later : hard-coded for now
            ViewBag.TotalOrders = 15;
            ViewBag.PendingOrders = 3;
            
            return View();
        }

        // Product List
        public IActionResult ProdList(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                var prod = context.Product
                    .Include(p => p.ProductVariants)
                    .OrderBy(p => p.Name)
                    .ToList();
                return View(prod);
            }
            else
            {
                var prod = (
                    from p in context.Product
                    where p.Name.Contains(searchString)
                    select p
                    ).ToList();
                return View(prod);
            }
        }

        // Order List - to be implemented later
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
            var prod = context.Product
                .Include(p => p.ProductVariants)
                .FirstOrDefault(p => p.ProductId == Id);
            if (prod == null)
                return NotFound();
            return View(prod);
        }
        [HttpPost]
        public IActionResult Edit(Product prod, string[] selectedSizes)
        {
            // Handles Size Selection
            if (selectedSizes != null && selectedSizes.Length > 0)
            {
                // For new Products
                if (prod.ProductId == 0)
                {
                    if (ModelState.IsValid)
                    {
                        context.Product.Add(prod);
                        context.SaveChanges();
                        
                        // Create ProductVariatn for selected size
                        foreach (var sizeStr in selectedSizes)
                        {
                            if (int.TryParse(sizeStr, out var size))
                            {
                                var variant = new ProductVariant
                                {
                                    ProductId = prod.ProductId,
                                    Size = size,
                                    StockCount = 0 // Default stock, for admin to update later
                                };
                                context.ProductVariant.Add(variant);
                            }
                        }
                        context.SaveChanges();
                        return RedirectToAction("ProdList", "Admin");
                    }
                }
            }
            else
            {
                // For Existing Products
                if (ModelState.IsValid)
                {
                    var existingProduct = context.Product
                        .Include(p => p.ProductVariants)
                        .FirstOrDefault(p => p.ProductId == prod.ProductId);

                    if (existingProduct != null)
                    {
                        // Update product properties
                        existingProduct.Name = prod.Name;
                        existingProduct.Brand = prod.Brand;
                        existingProduct.Price = prod.Price;
                        existingProduct.Description = prod.Description;
                        existingProduct.Color = prod.Color;
                        existingProduct.Category = prod.Category;
                        existingProduct.ImageUrl = prod.ImageUrl;
                        
                        // Simple variant management: remove all and recreate
                        context.ProductVariant.RemoveRange(existingProduct.ProductVariants);
                            
                        foreach (var sizeStr in selectedSizes)
                        {
                            if (int.TryParse(sizeStr, out int size))
                            {
                                var variant = new ProductVariant
                                {
                                    ProductId = existingProduct.ProductId,
                                    Size = size,
                                    StockCount = 0
                                };
                                context.ProductVariant.Add(variant);
                            }
                        }
                        context.SaveChanges();
                    }
                    return RedirectToAction("ProdList", "Admin");
                }
            }
            ViewBag.Action = (prod.ProductId == 0) ? "Create" : "Edit";
            return View(prod);
        }

        // Delete product
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var prod = context.Product
                .Include(p => p.ProductVariants)
                .FirstOrDefault(p => p.ProductId == Id);
            if (prod == null)
                return NotFound();
            return View(prod);
        }
        
        [HttpPost]
        public IActionResult Delete(Product prod)
        {
            var existingProduct = context.Product
                .Include(p => p.ProductVariants)
                .FirstOrDefault(p => p.ProductId == prod.ProductId);

            if (existingProduct != null)
            {
                // Remove variants first to avoid foreign key issues
                context.ProductVariant.RemoveRange(existingProduct.ProductVariants);
                context.Product.Remove(existingProduct);
                context.SaveChanges();
            }
            return RedirectToAction("ProdList", "Admin");
        }

        // Product Details
        public IActionResult Details(int Id)
        {
            var prod = context.Product
                .Include(p => p.ProductVariants)
                .FirstOrDefault(p => p.ProductId == Id);
            if (prod == null)
                return NotFound();
            return View(prod);
        }
    }
}