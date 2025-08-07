using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodShoe.Models;
using GoodShoe.Data;
using GoodShoe.ViewModels;

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

            // NEW: Calculate Order Statistics
            var totalOrders = context.Orders.Count();
            var pendingOrders = context.Orders.Where(o => o.Status == "Pending").Count();
            var shippingOrders = context.Orders.Where(o => o.Status == "Shipping").Count();
            var deliveredOrders = context.Orders.Where(o => o.Status == "Delivered").Count();
            var cancelledOrders = context.Orders.Where(o => o.Status == "Cancelled").Count();

            // Recent orders (last 7 days)
            var recentOrders = context.Orders
                .Where(o => o.CreatedAt >= DateTime.Now.AddDays(-7))
                .Count();

            // Total revenue from completed orders
            var totalRevenue = context.Orders
                .Where(o => o.Status == "Delivered" && o.PaymentStatus == "Completed")
                .Sum(o => o.TotalAmount);

            // Pass all statistics to the view
            ViewBag.TotalProducts = totalProducts;
            ViewBag.TotalStock = totalStock;
            ViewBag.LowStockProducts = lowStockProducts;
            ViewBag.OutOfStockProducts = outOfStockProducts;
            ViewBag.TotalValue = totalValue;

            // Order statistics
            ViewBag.TotalOrders = totalOrders;
            ViewBag.PendingOrders = pendingOrders;
            ViewBag.ShippingOrders = shippingOrders;
            ViewBag.DeliveredOrders = deliveredOrders;
            ViewBag.CancelledOrders = cancelledOrders;
            ViewBag.RecentOrders = recentOrders;
            ViewBag.TotalRevenue = totalRevenue;

            return View();
        }

        // == == == == == Product List Section == == == == == //
        public IActionResult ProdList()
        {
            var prod = context.Product
                .Include(p => p.ProductVariants)
                .OrderBy(p => p.Name)
                .ToList();
            return View(prod);
        }
        
        // Create New Product (for admin)
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Action = "Create";
            return View("Create", new Product());
        }

        // GET method : Edit product details (for admin)
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
        
        // POST method : Edit product details (for admin)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product, string[] selectedSizes, IFormFile imageFile)
        {
            try
            {
                // Handle image upload if provided
                if (imageFile != null && imageFile.Length > 0)
                {
                    var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/bmp" };
                    if (allowedTypes.Contains(imageFile.ContentType.ToLower()))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await imageFile.CopyToAsync(memoryStream);
                            product.Image = memoryStream.ToArray();
                            product.ImageFileName = imageFile.FileName;
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Invalid image format. Please use JPG, PNG, GIF, or BMP.";
                        ViewBag.Action = (product.ProductId == 0) ? "Create" : "Edit";
                        return View(product);
                    }
                }

                // Validate required fields
                if (string.IsNullOrWhiteSpace(product.Name))
                {
                    TempData["ErrorMessage"] = "Product name is required.";
                    ViewBag.Action = (product.ProductId == 0) ? "Create" : "Edit";
                    return View(product);
                }

                if (product.Price <= 0)
                {
                    TempData["ErrorMessage"] = "Product price must be greater than 0.";
                    ViewBag.Action = (product.ProductId == 0) ? "Create" : "Edit";
                    return View(product);
                }

                if (string.IsNullOrWhiteSpace(product.Category))
                {
                    TempData["ErrorMessage"] = "Category is required.";
                    ViewBag.Action = (product.ProductId == 0) ? "Create" : "Edit";
                    return View(product);
                }

                if (selectedSizes == null || selectedSizes.Length == 0)
                {
                    TempData["ErrorMessage"] = "At least one size must be selected.";
                    ViewBag.Action = (product.ProductId == 0) ? "Create" : "Edit";
                    return View(product);
                }

                // For new products
                if (product.ProductId == 0)
                {
                    // Set default values
                    if (string.IsNullOrWhiteSpace(product.Brand))
                        product.Brand = "Default Brand";
                    
                    if (string.IsNullOrWhiteSpace(product.Color))
                        product.Color = "Standard";

                    context.Product.Add(product);
                    await context.SaveChangesAsync();

                    // Create ProductVariants for selected sizes
                    foreach (var sizeStr in selectedSizes)
                    {
                        if (int.TryParse(sizeStr, out var size))
                        {
                            var variant = new ProductVariant
                            {
                                ProductId = product.ProductId,
                                Size = size,
                                StockCount = 0 // Default stock, admin can update later
                            };
                            context.ProductVariant.Add(variant);
                        }
                    }
                    await context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Product created successfully!";
                    return RedirectToAction("ProdList", "Admin");
                }
                // For existing products
                else
                {
                    var existingProduct = context.Product
                        .Include(p => p.ProductVariants)
                        .FirstOrDefault(p => p.ProductId == product.ProductId);

                    if (existingProduct != null)
                    {
                        // Update product properties
                        existingProduct.Name = product.Name;
                        existingProduct.Brand = product.Brand ?? "Default Brand";
                        existingProduct.Price = product.Price;
                        existingProduct.Description = product.Description;
                        existingProduct.Color = product.Color ?? "Standard";
                        existingProduct.Category = product.Category;
                        existingProduct.ImageUrl = product.ImageUrl;

                        // Update image if new one was uploaded
                        if (product.Image != null)
                        {
                            existingProduct.Image = product.Image;
                            existingProduct.ImageFileName = product.ImageFileName;
                        }

                        // Remove existing variants and create new ones
                        context.ProductVariant.RemoveRange(existingProduct.ProductVariants);

                        foreach (var sizeStr in selectedSizes)
                        {
                            if (int.TryParse(sizeStr, out int size))
                            {
                                var variant = new ProductVariant
                                {
                                    ProductId = existingProduct.ProductId,
                                    Size = size,
                                    StockCount = 0 // Admin can update stock separately
                                };
                                context.ProductVariant.Add(variant);
                            }
                        }

                        await context.SaveChangesAsync();
                        TempData["SuccessMessage"] = "Product updated successfully!";
                        return RedirectToAction("ProdList", "Admin");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error saving product: " + ex.Message;
            }

            ViewBag.Action = (product.ProductId == 0) ? "Create" : "Edit";
            return View(product);
        }

        // Get method : Delete product (for admin)
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
        
        // Post method : Delete product (for admin)
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public async Task<IActionResult> Delete(Product prod)
        {
            try
            {
                var existingProduct = context.Product
                    .Include(p => p.ProductVariants)
                    .FirstOrDefault(p => p.ProductId == prod.ProductId);

                if (existingProduct != null)
                {
                    // Remove variants first to avoid foreign key issues
                    context.ProductVariant.RemoveRange(existingProduct.ProductVariants);
                    context.Product.Remove(existingProduct);
                    await context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Product deleted successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Product not found.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error deleting product: " + ex.Message;
            }

            return RedirectToAction("ProdList", "Admin");
        }

        // Product Details (for admin)
        public IActionResult Details(int Id)
        {
            var prod = context.Product
                .Include(p => p.ProductVariants)
                .FirstOrDefault(p => p.ProductId == Id);
            if (prod == null)
                return NotFound();
            
            return View(prod);
        }
        
        // Stock Management (for admin)
        [HttpGet]
        public IActionResult ManageStock(int id)
        {
            var prod = context.Product
                .Include(p => p.ProductVariants)
                .FirstOrDefault(p => p.ProductId == id);
            
            if (prod == null)
                return NotFound();
            
            return View(prod);
        }        
        
        // Post Method for Updating Stock
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStock(int productId, Dictionary<int, int> stockCounts)
        {
            try
            {
                var product = context.Product
                    .Include(p => p.ProductVariants)
                    .FirstOrDefault(p => p.ProductId == productId);

                if (product == null)
                {
                    TempData["ErrorMessage"] = "Product not found.";
                    return RedirectToAction("ProdList");
                }

                foreach (var variant in product.ProductVariants)
                {
                    if (stockCounts.ContainsKey(variant.Size) && stockCounts[variant.Size] >= 0)
                    {
                        variant.StockCount = stockCounts[variant.Size];
                    }
                }

                await context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Stock updated successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error updating stock: " + ex.Message;
            }

            return RedirectToAction("ProdList");
        }        
        
        // == == == == == Order List Section == == == == == /
        public IActionResult OrderList(int page = 1, int pageSize = 10)
        {
            var orders = context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new OrderListViewModel
                {
                    OrderID = o.OrderId,
                    CustomerEmail = o.Customer.Email,
                    TotalAmount = o.TotalAmount,
                    Date = o.CreatedAt,
                    Status = o.Status,
                    Items = o.OrderItems.Sum(oi => oi.Quantity),
                    CanEdit = true
                })
                .ToList();

            var totalOrders = context.Orders.Count();
            var totalPages = (int)Math.Ceiling((double)totalOrders / pageSize);

            var viewModel = new OrderManagementViewModel
            {
                Orders = orders,
                CurrentPage = page,
                TotalPages = totalPages,
                TotalOrders = totalOrders
            };

            return View(viewModel);
        }
        
        // Post method : Update Order Status (for admin)
        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, string newStatus)
        {
            try
            {
                var order = context.Orders.FirstOrDefault(o => o.OrderId == orderId);
                if (order != null)
                {
                    order.Status = newStatus;
                    await context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Order status updated successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Order not found.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error updating order status: " + ex.Message;
            }
            return RedirectToAction("OrderList");
        }
        
        
        // Method for deleting orders
        [HttpPost]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            try
            {
                var order = context.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefault(o => o.OrderId == orderId);
            
                if (order != null)
                {
                    // Remove order items first
                    context.OrderItems.RemoveRange(order.OrderItems);
                    // Then, remove the order
                    context.Orders.Remove(order);
                    await context.SaveChangesAsync();
            
                    TempData["SuccessMessage"] = "Order deleted successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Order not found.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error deleting order: " + ex.Message;
            }
    
            return RedirectToAction("OrderList");
        }
    }
}