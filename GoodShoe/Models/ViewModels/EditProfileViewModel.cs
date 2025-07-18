using System.ComponentModel.DataAnnotations;

namespace GoodShoe.ViewModels
{
    public class EditProfileViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string Location { get; set; }
    }
}

