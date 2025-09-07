// Controllers/ProfileController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodShoe.Data;
using GoodShoe.ViewModels;
using GoodShoe.Services;

namespace GoodShoe.Controllers
{
    public class ProfileController : Controller
    {
        private readonly GoodShoeDbContext _context;
        private readonly IAuthService _authService;

        public ProfileController(GoodShoeDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentCustomer = _authService.GetCurrentCustomer();
            if (currentCustomer == null)
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                // Get purchase history with better error handling
                var orders = await _context.Orders
                    .Where(o => o.CustomerId == currentCustomer.CustomerId)
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariant)
                    .ThenInclude(pv => pv.Product)
                    .OrderByDescending(o => o.CreatedAt)
                    .ToListAsync();

                var purchaseHistory = new List<PurchaseHistoryItem>();
                
                foreach (var order in orders)
                {
                    foreach (var item in order.OrderItems)
                    {
                        purchaseHistory.Add(new PurchaseHistoryItem
                        {
                            ProductName = item.ProductName,
                            Description = item.ProductVariant?.Product?.Description ?? "No description available",
                            Size = item.Size.ToString(),
                            Price = item.UnitPrice,
                            ImageUrl = item.ProductVariant?.Product?.ImageUrl ?? "/images/no-image.png",
                            PurchaseDate = order.CreatedAt,
                            Quantity = item.Quantity,
                            OrderId = order.OrderId,
                            Status = order.Status
                        });
                    }
                }

                // DEBUG: Log what we found
                System.Diagnostics.Debug.WriteLine("=== Purchase History Debug ===");
                System.Diagnostics.Debug.WriteLine($"Found {orders.Count} orders with {purchaseHistory.Count} items");
                
                var currentOrders = purchaseHistory.Where(p => p.IsNewOrder).ToList();
                var pastOrders = purchaseHistory.Where(p => !p.IsNewOrder).ToList();
                
                System.Diagnostics.Debug.WriteLine($"Current Orders: {currentOrders.Count}");
                System.Diagnostics.Debug.WriteLine($"Past Orders: {pastOrders.Count}");
                
                foreach (var item in purchaseHistory)
                {
                    System.Diagnostics.Debug.WriteLine($"Product: {item.ProductName}, Status: '{item.Status}', StatusClass: '{item.StatusClass}', IsNew: {item.IsNewOrder}");
                }

                var viewModel = new ProfileViewModel
                {
                    CustomerId = currentCustomer.CustomerId,
                    FullName = currentCustomer.FullName,
                    Email = currentCustomer.Email,
                    PhoneNumber = currentCustomer.Phone,
                    Location = currentCustomer.Address,
                    PurchaseHistory = purchaseHistory
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading purchase history: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");

                var viewModel = new ProfileViewModel
                {
                    CustomerId = currentCustomer.CustomerId,
                    FullName = currentCustomer.FullName,
                    Email = currentCustomer.Email,
                    PhoneNumber = currentCustomer.Phone,
                    Location = currentCustomer.Address,
                    PurchaseHistory = new List<PurchaseHistoryItem>()
                };

                TempData["Error"] = $"Unable to load purchase history: {ex.Message}";
                return View(viewModel);
            }            
        }
        
        [HttpGet]
        public IActionResult Edit()
        {
            var currentCustomer = _authService.GetCurrentCustomer();
            if (currentCustomer == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var viewModel = new EditProfileViewModel
            {
                CustomerId = currentCustomer.CustomerId,
                FirstName = currentCustomer.FirstName,
                LastName = currentCustomer.LastName,
                Email = currentCustomer.Email,
                PhoneNumber = currentCustomer.Phone,
                Address = currentCustomer.Address
            };

            return View(viewModel);
        }

        // POST: Profile/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProfileViewModel model)
        {
            var currentCustomer = _authService.GetCurrentCustomer();
            if (currentCustomer == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Server-side email validation using EmailAddressAttribute or custom validation
            if (!string.IsNullOrEmpty(model.Email))
            {
                var emailAttribute = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
                if (!emailAttribute.IsValid(model.Email))
                {
                    ModelState.AddModelError("Email", "Please enter a valid email address.");
                }
            }

            // Additional custom validations
            if (string.IsNullOrWhiteSpace(model.FirstName))
            {
                ModelState.AddModelError("FirstName", "First name is required.");
            }

            if (string.IsNullOrWhiteSpace(model.LastName))
            {
                ModelState.AddModelError("LastName", "Last name is required.");
            }

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                ModelState.AddModelError("Email", "Email address is required.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check if email is taken by another user
            if (model.Email != currentCustomer.Email && await _authService.IsEmailTakenAsync(model.Email))
            {
                ModelState.AddModelError("Email", "This email address is already registered to another account.");
                return View(model);
            }

            // Update customer
            currentCustomer.FirstName = model.FirstName;
            currentCustomer.LastName = model.LastName;
            currentCustomer.Email = model.Email;
            currentCustomer.Phone = model.PhoneNumber;
            currentCustomer.Address = model.Address;
            currentCustomer.UpdatedAt = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                
                // Update session with new data
                _authService.SetCurrentCustomer(currentCustomer);
                
                TempData["Success"] = "Profile updated successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception if you have logging configured
                TempData["Error"] = "Failed to update profile. Please try again.";
                return View(model);
            }
        }

        // POST: Profile/ChangePassword
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please check your password entries.";
                return RedirectToAction("Edit");
            }

            var currentCustomer = _authService.GetCurrentCustomer();
            if (currentCustomer == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Verify current password
            if (currentCustomer.Password != model.CurrentPassword)
            {
                TempData["Error"] = "Current password is incorrect.";
                return RedirectToAction("Edit");
            }

            // Update password
            currentCustomer.Password = model.NewPassword;
            currentCustomer.UpdatedAt = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                TempData["Success"] = "Password changed successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to change password. Please try again.";
                return RedirectToAction("Edit");
            }
        }
        
        // POST: Profile/DeleteAccount
        [HttpPost]
        public async Task<IActionResult> DeleteAccount()
        {
            var currentCustomer = _authService.GetCurrentCustomer();
            if (currentCustomer == null)
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                // Delete related order items first (to maintain referential integrity)
                var customerOrders = await _context.Orders
                    .Where(o => o.CustomerId == currentCustomer.CustomerId)
                    .Include(o => o.OrderItems)
                    .ToListAsync();

                foreach (var order in customerOrders)
                {
                    _context.OrderItems.RemoveRange(order.OrderItems);
                }

                // Delete orders
                _context.Orders.RemoveRange(customerOrders);

                // Delete customer account
                _context.Customers.Remove(currentCustomer);

                // Save all changes
                await _context.SaveChangesAsync();

                // Clear session/authentication
                _authService.Logout();

                // Set success message for redirect
                TempData["Success"] = "Your account has been permanently deleted. We're sorry to see you go!";
        
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to delete account. Please try again or contact support.";
                return RedirectToAction("Index");
            }
        }
        
        // Temporary method to provide mock purchase history
        private List<PurchaseHistoryItem> GetMockPurchaseHistory()
        {
            return new List<PurchaseHistoryItem>
            {
                new PurchaseHistoryItem
                {
                    ProductName = "GoodShoe 1",
                    Description = "Everyday wear",
                    Size = "10",
                    Price = 259.00m,
                    ImageUrl = "../images/products/image1.png",
                    PurchaseDate = DateTime.Now.AddDays(-30)
                },
                new PurchaseHistoryItem
                {
                    ProductName = "Aero Burst",
                    Description = "Shoes for running",
                    Size = "9",
                    Price = 150.00m,
                    ImageUrl = "../images/products/image2.png",
                    PurchaseDate = DateTime.Now.AddDays(-60)
                }
            };
        }
    }
}