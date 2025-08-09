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
        
        // Create New Product (for admin) - GET
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Action = "Create";
            return View(new Product());
        }

        // Create New Product (for admin) - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, string[] selectedSizes, IFormFile imageFile,
            Dictionary<int, int> stockCounts)
        {
            try
            {
                // Debug logging to check what's being received
                Console.WriteLine("=== CREATE PRODUCT DEBUG ===");
                Console.WriteLine($"Product Name: '{product?.Name}'");
                Console.WriteLine($"Product Price: {product?.Price}");
                Console.WriteLine($"Product Category: '{product?.Category}'");
                Console.WriteLine($"Selected Sizes: {(selectedSizes != null ? $"[{string.Join(",", selectedSizes)}]" : "null")}");

                if (stockCounts != null)
                {
                    Console.WriteLine($"Stock Counts received: {stockCounts.Count} items");
                    foreach (var kvp in stockCounts)
                    {
                        Console.WriteLine($"  Size {kvp.Key}: {kvp.Value} units");
                    }
                }

                // Clear ModelState to avoid validation issues
                ModelState.Clear();

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
                        ViewBag.Action = "Create";
                        return View(product);
                    }
                }

                // Validate required fields
                if (string.IsNullOrWhiteSpace(product.Name))
                {
                    TempData["ErrorMessage"] = "Product name is required.";
                    ViewBag.Action = "Create";
                    return View(product);
                }

                if (product.Price <= 0)
                {
                    TempData["ErrorMessage"] = "Product price must be greater than 0.";
                    ViewBag.Action = "Create";
                    return View(product);
                }

                if (string.IsNullOrWhiteSpace(product.Category))
                {
                    TempData["ErrorMessage"] = "Category is required.";
                    ViewBag.Action = "Create";
                    return View(product);
                }

                if (selectedSizes == null || selectedSizes.Length == 0)
                {
                    TempData["ErrorMessage"] = "At least one size must be selected.";
                    ViewBag.Action = "Create";
                    return View(product);
                }

                // Validate stock counts if provided
                if (stockCounts != null)
                {
                    foreach (var kvp in stockCounts)
                    {
                        if (kvp.Value < 0)
                        {
                            TempData["ErrorMessage"] = "Stock counts cannot be negative.";
                            ViewBag.Action = "Create";
                            return View(product);
                        }
                    }
                }

                // MANUAL ID GENERATION: Get the next available ProductId
                var maxProductId = 0;
                if (context.Product.Any())
                {
                    maxProductId = context.Product.Max(p => p.ProductId);
                }
                var nextProductId = maxProductId + 1;

                // Double-check that this ID doesn't exist (safety check)
                while (context.Product.Any(p => p.ProductId == nextProductId))
                {
                    nextProductId++;
                }

                Console.WriteLine($"Generated ProductId: {nextProductId}");

                // Set default values for new product
                if (string.IsNullOrWhiteSpace(product.Brand))
                    product.Brand = "Default Brand";

                if (string.IsNullOrWhiteSpace(product.Color))
                    product.Color = "Standard";

                if (string.IsNullOrWhiteSpace(product.Description))
                    product.Description = "";

                Console.WriteLine("Adding product to database...");

                // Create a completely new Product entity with manually assigned ID
                var newProduct = new Product
                {
                    ProductId = nextProductId, // Manually assign the ID
                    Name = product.Name.Trim(),
                    Brand = product.Brand.Trim(),
                    Price = product.Price,
                    Description = product.Description?.Trim() ?? "",
                    Color = product.Color.Trim(),
                    Category = product.Category.Trim(),
                    ImageUrl = product.ImageUrl?.Trim(),
                    Image = product.Image,
                    ImageFileName = product.ImageFileName?.Trim(),
                    CreatedAt = DateTime.Now
                };

                // Add product to database first
                context.Product.Add(newProduct);
                await context.SaveChangesAsync();

                Console.WriteLine($"Product created successfully with ID: {newProduct.ProductId}");

                // FIXED: Create ProductVariants properly
                Console.WriteLine("Creating ProductVariants...");
                var variants = new List<ProductVariant>();

                foreach (var sizeStr in selectedSizes)
                {
                    if (int.TryParse(sizeStr, out var size))
                    {
                        var stockCount = 0;
                        if (stockCounts != null && stockCounts.ContainsKey(size))
                        {
                            stockCount = Math.Max(0, stockCounts[size]);
                        }

                        var variant = new ProductVariant
                        {
                            // DO NOT set Id - let the database auto-generate it
                            ProductId = newProduct.ProductId,
                            Size = size,
                            StockCount = stockCount
                        };

                        variants.Add(variant);
                        Console.WriteLine($"Prepared variant: ProductId={newProduct.ProductId}, Size={size}, Stock={stockCount}");
                    }
                }

                // Add all variants at once and save
                if (variants.Any())
                {
                    Console.WriteLine($"Adding {variants.Count} variants to context...");
                    context.ProductVariant.AddRange(variants);
                    
                    // IMPORTANT: Save changes to commit the variants
                    var variantsAdded = await context.SaveChangesAsync();
                    Console.WriteLine($"Successfully saved {variantsAdded} changes to database!");
                    
                    // Verify variants were added
                    var createdVariants = await context.ProductVariant
                        .Where(pv => pv.ProductId == newProduct.ProductId)
                        .ToListAsync();
                    Console.WriteLine($"Verification: Found {createdVariants.Count} variants in database for ProductId {newProduct.ProductId}");
                }

                Console.WriteLine("Product creation completed successfully!");
                TempData["SuccessMessage"] = $"Product '{newProduct.Name}' created successfully with {selectedSizes.Length} size variants!";
                return RedirectToAction("ProdList", "Admin");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== ERROR IN CREATE PRODUCT ===");
                Console.WriteLine($"Error message: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }

                TempData["ErrorMessage"] = $"Error creating product: {ex.Message}";
                ViewBag.Action = "Create";
                return View(product);
            }
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
        public async Task<IActionResult> Edit(Product product, string[] selectedSizes, IFormFile imageFile,
            Dictionary<int, int> stockCounts)
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
                        ViewBag.Action = "Edit";
                        return View(product);
                    }
                }

                // Validate required fields
                if (string.IsNullOrWhiteSpace(product.Name))
                {
                    TempData["ErrorMessage"] = "Product name is required.";
                    ViewBag.Action = "Edit";
                    return View(product);
                }

                if (product.Price <= 0)
                {
                    TempData["ErrorMessage"] = "Product price must be greater than 0.";
                    ViewBag.Action = "Edit";
                    return View(product);
                }

                if (string.IsNullOrWhiteSpace(product.Category))
                {
                    TempData["ErrorMessage"] = "Category is required.";
                    ViewBag.Action = "Edit";
                    return View(product);
                }

                if (selectedSizes == null || selectedSizes.Length == 0)
                {
                    TempData["ErrorMessage"] = "At least one size must be selected.";
                    ViewBag.Action = "Edit";
                    return View(product);
                }

                // Validate stock counts if provided
                if (stockCounts != null)
                {
                    foreach (var kvp in stockCounts)
                    {
                        if (kvp.Value < 0)
                        {
                            TempData["ErrorMessage"] = "Stock counts cannot be negative.";
                            ViewBag.Action = "Edit";
                            return View(product);
                        }
                    }
                }

                var existingProduct = context.Product
                    .Include(p => p.ProductVariants)
                    .FirstOrDefault(p => p.ProductId == product.ProductId);

                if (existingProduct == null)
                {
                    TempData["ErrorMessage"] = "Product not found.";
                    return RedirectToAction("ProdList", "Admin");
                }

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

                // FIXED: Handle ProductVariants with foreign key constraints
                var selectedSizeInts = selectedSizes.Select(s => int.Parse(s)).ToList();
                var existingVariants = existingProduct.ProductVariants.ToList();

                // Check if any variants are referenced by OrderItems before deleting
                foreach (var variant in existingVariants)
                {
                    if (!selectedSizeInts.Contains(variant.Size))
                    {
                        // Check if this variant is referenced by any OrderItems
                        var isReferenced = context.OrderItems.Any(oi => oi.ProductVariantId == variant.Id);
                        
                        if (isReferenced)
                        {
                            // If referenced, set stock to 0 instead of deleting
                            variant.StockCount = 0;
                            Console.WriteLine($"Variant Size {variant.Size} is referenced by orders. Setting stock to 0 instead of deleting.");
                        }
                        else
                        {
                            // Safe to remove if not referenced
                            context.ProductVariant.Remove(variant);
                            Console.WriteLine($"Removing variant Size {variant.Size} - not referenced by any orders.");
                        }
                    }
                }

                // Update existing variants or add new ones
                foreach (var sizeStr in selectedSizes)
                {
                    if (int.TryParse(sizeStr, out int size))
                    {
                        var stockCount = 0;
                        if (stockCounts != null && stockCounts.ContainsKey(size))
                        {
                            stockCount = Math.Max(0, stockCounts[size]); // Ensure non-negative
                        }

                        var existingVariant = existingVariants.FirstOrDefault(v => v.Size == size);
                        
                        if (existingVariant != null)
                        {
                            // Update existing variant
                            existingVariant.StockCount = stockCount;
                            Console.WriteLine($"Updated existing variant Size {size} with stock {stockCount}");
                        }
                        else
                        {
                            // Add new variant
                            var newVariant = new ProductVariant
                            {
                                ProductId = existingProduct.ProductId,
                                Size = size,
                                StockCount = stockCount
                            };
                            context.ProductVariant.Add(newVariant);
                            Console.WriteLine($"Added new variant Size {size} with stock {stockCount}");
                        }
                    }
                }

                await context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Product updated successfully!";
                return RedirectToAction("ProdList", "Admin");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Edit method: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                
                TempData["ErrorMessage"] = "Error updating product: " + ex.Message;
                ViewBag.Action = "Edit";
                return View(product);
            }
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