using GoodShoe.Data;
using GoodShoe.Models;
using GoodShoe.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace GoodShoe.Controllers
{
    public class AccountController : Controller
    {
        private readonly GoodShoeDbContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(GoodShoeDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Account/Login
        public IActionResult Login(string returnUrl = null)
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // login as admin
                var admin = await _context.Admins
                    .FirstOrDefaultAsync(a => a.Email == model.Email && a.Password == model.Password);

                if (admin != null)
                {
                    HttpContext.Session.SetString("UserId", admin.AdminId.ToString());
                    HttpContext.Session.SetString("UserRole", "Admin");
                    _logger.LogInformation($"Admin {admin.UserName} logged in.");
                    return RedirectToAction("Index", "Admin");
                }

                // login as customer
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.Email == model.Email && c.Password == model.Password);

                if (customer != null)
                {
                    HttpContext.Session.SetString("UserId", customer.CustomerId.ToString());
                    HttpContext.Session.SetString("UserRole", "Customer");

                    _logger.LogInformation($"Customer {customer.FirstName} {customer.LastName} registered and logged in.");
                    return RedirectToAction("Index", "Home");
                }

                // If login fail
                _logger.LogWarning("Invalid login attempt.");
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Add as new customer
                var customer = new Customer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    Phone = model.Phone,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");

            }

            return View(model);
        }

        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Profile
        public async Task<IActionResult> Profile()
        {
            var role = HttpContext.Session.GetString("UserRole");
            var userIdStr = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(role) || string.IsNullOrEmpty(userIdStr))
                return RedirectToAction("Login");

            int userId = int.Parse(userIdStr);

            if (role == "Admin")
            {
                var admin = await _context.Admins.FindAsync(userId);
                return View(admin);
            }
            else if (role == "Customer")
            {
                var customer = await _context.Customers.FindAsync(userId);
                return View(customer);
            }

            return RedirectToAction("Login");
        }
    }
}
