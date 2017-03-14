using System.ComponentModel.DataAnnotations;

namespace TravelGuide.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The {0} must be between 3 and 20 characters long.")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}