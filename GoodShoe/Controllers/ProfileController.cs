// Controllers/ProfileController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodShoe.Data;
using GoodShoe.ViewModels;
using GoodShoe.Services;

namespace GoodShoe.Controllers
{
    // [Authorize] // Disabled for testing - re-enable later
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

            // Get purchase history
            var purchaseHistory = await _context.Orders
                .Where(o => o.CustomerId == currentCustomer.CustomerId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.ProductVariant)
                .ThenInclude(pv => pv.Product)
                .OrderByDescending(o => o.CreatedAt)
                .SelectMany(o => o.OrderItems.Select(oi => new PurchaseHistoryItem
                {
                    ProductName = oi.ProductName,
                    Description = oi.ProductVariant.Product.Description ?? "",
                    Size = oi.Size.ToString(),
                    Price = oi.UnitPrice,
                    ImageUrl = oi.ProductVariant.Product.ImageUrl ?? "",
                    PurchaseDate = o.CreatedAt
                }))
                .ToListAsync();

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
        public async Task<IActionResult> Edit(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentCustomer = _authService.GetCurrentCustomer();
            if (currentCustomer == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Check if email is taken by another user
            if (model.Email != currentCustomer.Email && await _authService.IsEmailTakenAsync(model.Email))
            {
                ModelState.AddModelError("Email", "Email is already registered.");
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
            catch (Exception)
            {
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