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

        // Order List
        public IActionResult OrderList(int page = 1, int pageSize = 10)
        {
            var orders = context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)  // ADD this line to include OrderItems
                .OrderByDescending(o => o.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new OrderListViewModel
                {
                    OrderID = o.OrderId,
                    CustomerEmail = o.Customer != null ? o.Customer.Email : "guest@example.com",
                    Date = o.CreatedAt,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    Items = o.OrderItems.Sum(oi => oi.Quantity), // ADD this line
                    CanEdit = o.Status != "Delivered" && o.Status != "Cancelled"
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

        // GET: Create Order
        [HttpGet]
        public IActionResult CreateOrder()
        {
            var viewModel = new CreateOrderViewModel
            {
                // FIXED: Using Customer instead of anonymous type
                Customers = context.Customers.Select(c => new Customer 
                { 
                    CustomerId = c.CustomerId, 
                    Email = c.Email 
                }).ToList(),
        
                // Get products for selection
                Products = context.Product
                    .Include(p => p.ProductVariants)
                    .Where(p => p.ProductVariants.Any(pv => pv.StockCount > 0))
                    .ToList(),
            
                CreatedAt = DateTime.Now,
                Status = "Pending",
                PaymentMethod = "Credit / Debit Card",
                PaymentStatus = "Completed"
            };
    
            return View(viewModel);
        }

        // POST: Create Order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrder(CreateOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Get customer info
                    var customer = context.Customers.FirstOrDefault(c => c.CustomerId == model.CustomerId);
                    
                    // Create new order
                    var order = new Order
                    {
                        CustomerId = model.CustomerId,
                        TotalAmount = model.TotalAmount,
                        Status = model.Status,
                        Address = model.Address ?? $"{customer?.Email}, {customer?.Address}",
                        PaymentMethod = model.PaymentMethod,
                        PaymentStatus = model.PaymentStatus,
                        CreatedAt = model.CreatedAt,
                        UpdatedAt = DateTime.Now
                    };

                    context.Orders.Add(order);
                    context.SaveChanges();

                    // Create order items if provided
                    if (!string.IsNullOrEmpty(model.ProductIds))
                    {
                        var productIds = model.ProductIds.Split(',').Select(int.Parse).ToList();
                        var quantities = model.Quantities?.Split(',').Select(int.Parse).ToList() ?? new List<int>();
                        var sizes = model.Sizes?.Split(',').ToList() ?? new List<string>();

                        for (int i = 0; i < productIds.Count; i++)
                        {
                            var productId = productIds[i];
                            var quantity = i < quantities.Count ? quantities[i] : 1;
                            var size = i < sizes.Count ? sizes[i] : "10";

                            var product = context.Product.FirstOrDefault(p => p.ProductId == productId);
                            var productVariant = context.ProductVariant
                                .FirstOrDefault(pv => pv.ProductId == productId && pv.Size == int.Parse(size));

                            if (product != null && productVariant != null)
                            {
                                var orderItem = new OrderItem
                                {
                                    OrderId = order.OrderId,
                                    ProductVariantId = productVariant.Id,
                                    ProductName = product.Name,
                                    Size = int.Parse(size),
                                    Quantity = quantity,
                                    UnitPrice = product.Price,
                                    TotalPrice = product.Price * quantity
                                };

                                context.OrderItems.Add(orderItem);
                            }
                        }
                        
                        context.SaveChanges();
                    }

                    TempData["SuccessMessage"] = "Order created successfully!";
                    return RedirectToAction("OrderList");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Error creating order: " + ex.Message;
                }
            }

            // FIXED: Reload data if validation fails
            model.Customers = context.Customers.Select(c => new Customer
            { 
                CustomerId = c.CustomerId, 
                Email = c.Email 
            }).ToList();
            
            model.Products = context.Product
                .Include(p => p.ProductVariants)
                .Where(p => p.ProductVariants.Any(pv => pv.StockCount > 0))
                .ToList();
                
            return View(model);
        }     


        [HttpPost]
        public IActionResult UpdateOrderStatus(int orderId, string newStatus)
        {
            try
            {
                var order = context.Orders.FirstOrDefault(o => o.OrderId == orderId);
                if (order != null)
                {
                    order.Status = newStatus;
                    order.UpdatedAt = DateTime.Now;
                    context.SaveChanges();

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
        public IActionResult DeleteOrder(int orderId)
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
                    // Remove the order
                    context.Orders.Remove(order);
                    context.SaveChanges();
            
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

        // Add new product. Opens the edit view but with a new product
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Action = "Create";
            return View("Create", new Product());
        }

     // ~~ Edit product details ~~
        // Handle GET request to edit the item
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

        // Handle POST request to save the edits
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
                        // First add the shoe model details to product database
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
                                // Then add the shoe variant to its database
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

                        context.Product.Update(prod); // update the product database context

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
                                context.ProductVariant.Add(variant); // add the recreated variants to db context
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