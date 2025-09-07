using GoodShoe.Models;

namespace GoodShoe.Services
{
    public interface IAuthService
    {
        Task<Customer?> AuthenticateCustomerAsync(string email, string password);
        Task<Admin?> AuthenticateAdminAsync(string email, string password);
        Task<Customer?> RegisterCustomerAsync(string firstName, string lastName, string email, string password, string? phone = null, string? address = null);
        Task<bool> IsEmailTakenAsync(string email);
        void SetCurrentCustomer(Customer customer);
        void SetCurrentAdmin(Admin admin);
        void Logout();
        Customer? GetCurrentCustomer();
        Admin? GetCurrentAdmin();
        bool IsLoggedIn();
        bool IsAdmin();
    }
}