// Controllers/ProfileController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GoodShoe.Models.ViewModels;
using GoodShoe.ViewModels;
using GoodShoe.Models;

namespace GoodShoe.Controllers
{
    // [Authorize] // Disabled for testing - re-enable later
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ProfileController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            // For debugging - this should show in browser
            ViewBag.Debug = "ProfileController Index action reached!";
            
            var user = await _userManager.GetUserAsync(User);
            
            // If no user is logged in, use test data for development
            var model = new ProfileViewModel
            {
                Email = user?.Email ?? "queenara@gmail.com", // Use real user data after connecting with the database
                PhoneNumber = user?.PhoneNumber ?? "+65 9123 4567",
                Location = user?.Location ?? "Singapore",
                PurchaseHistory = GetMockPurchaseHistory()
            };

            return View(model);
        }
        

        // GET: Profile/Edit
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // For testing purposes, return a model with default values
                var testModel = new EditProfileViewModel
                {
                    Email = "queenara@gmail.com",
                    PhoneNumber = "+65 9123 4567",
                    Location = "Singapore"
                };
                return View(testModel);
            }

            var model = new EditProfileViewModel
            {
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber,
                Location = user.Location
            };

            return View(model);
        }

        // POST: Profile/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // For testing purposes, just show success message
                TempData["Success"] = "Profile updated successfully! (Test mode)";
                return RedirectToAction("Index");
            }

            // Update user properties
            user.Email = model.Email;
            user.UserName = model.Email; // Keep username in sync with email
            user.PhoneNumber = model.PhoneNumber;
            user.Location = model.Location;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["Success"] = "Profile updated successfully!";
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        // POST: Profile/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fill in all password fields correctly.";
                return RedirectToAction("Edit");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // For testing purposes, just show success message
                TempData["Success"] = "Password changed successfully! (Test mode)";
                return RedirectToAction("Edit");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                TempData["Success"] = "Password changed successfully!";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    TempData["Error"] = error.Description;
                    break; // Show only the first error
                }
            }

            return RedirectToAction("Edit");
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