using GoodShoe.Data;
using GoodShoe.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodShoe.Services
{
    public class AuthService : IAuthService
    {
        private readonly GoodShoeDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(GoodShoeDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Customer?> AuthenticateCustomerAsync(string email, string password)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == email && c.Password == password);
        }

        public async Task<Admin?> AuthenticateAdminAsync(string email, string password)
        {
            return await _context.Admin
                .FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
        }

        public async Task<Customer?> RegisterCustomerAsync(string firstName, string lastName, string email, string password, string? phone = null, string? address = null)
        {
            if (await IsEmailTakenAsync(email))
                return null;

            var customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                Phone = phone,
                Address = address,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _context.Customers.AnyAsync(c => c.Email == email) ||
                   await _context.Admin.AnyAsync(a => a.Email == email);
        }

        public void SetCurrentCustomer(Customer customer)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session != null)
            {
                session.SetInt32("CustomerId", customer.CustomerId);
                session.SetString("CustomerName", customer.FullName);
                session.SetString("CustomerEmail", customer.Email);
                session.Remove("AdminId"); // Clear admin session if exists
            }
        }

        public void SetCurrentAdmin(Admin admin)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session != null)
            {
                session.SetInt32("AdminId", admin.AdminId);
                session.SetString("AdminName", admin.UserName);
                session.SetString("AdminEmail", admin.Email);
                session.Remove("CustomerId"); // Clear customer session if exists
            }
        }

        public void Logout()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session != null)
            {
                session.Remove("CustomerId");
                session.Remove("CustomerName");
                session.Remove("CustomerEmail");
                session.Remove("AdminId");
                session.Remove("AdminName");
                session.Remove("AdminEmail");
            }
        }

        public Customer? GetCurrentCustomer()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            var customerId = session?.GetInt32("CustomerId");
            
            if (customerId.HasValue)
            {
                return _context.Customers.FirstOrDefault(c => c.CustomerId == customerId.Value);
            }
            
            return null;
        }

        public Admin? GetCurrentAdmin()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            var adminId = session?.GetInt32("AdminId");
            
            if (adminId.HasValue)
            {
                return _context.Admin.FirstOrDefault(a => a.AdminId == adminId.Value);
            }
            
            return null;
        }

        public bool IsLoggedIn()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            return session?.GetInt32("CustomerId").HasValue == true || 
                   session?.GetInt32("AdminId").HasValue == true;
        }

        public bool IsAdmin()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            return session?.GetInt32("AdminId").HasValue == true;
        }
    }
}