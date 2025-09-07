using Microsoft.AspNetCore.Mvc;
using GoodShoe.ViewModels;
using GoodShoe.Models;
using GoodShoe.Services;

namespace GoodShoe.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET: Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            if (_authService.IsLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        
        // POST: Account/Login // Redirecting to home for now - haven't been connected with the database yet
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            // Login for the admin
            var admin = await _authService.AuthenticateAdminAsync(model.Email, model.Password);
            if (admin != null)
            {
                _authService.SetCurrentAdmin(admin);
                return RedirectToAction("Index", "Admin");
            }
            
            // Login for the customer
            var customer = await _authService.AuthenticateCustomerAsync(model.Email, model.Password);
            if (customer != null)
            {
                _authService.SetCurrentCustomer(customer);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }

        // GET: Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            if (_authService.IsLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await _authService.IsEmailTakenAsync(model.Email))
            {
                ModelState.AddModelError("Email", "Email is already registered.");
                return View(model);
            }

            var customer = await _authService.RegisterCustomerAsync(
                model.FirstName, 
                model.LastName, 
                model.Email, 
                model.Password, 
                model.Phone, 
                model.Address
            );

            if (customer != null)
            {
                _authService.SetCurrentCustomer(customer);
                TempData["Success"] = "Account created successfully!";
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Registration failed. Please try again.");
            return View(model);
        }

        // POST: Account/Logout
        [HttpPost]
        public IActionResult Logout()
        {
            _authService.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}