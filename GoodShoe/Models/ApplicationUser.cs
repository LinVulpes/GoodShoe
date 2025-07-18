using Microsoft.AspNetCore.Identity;

namespace GoodShoe.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Location { get; set; }
    }
}


