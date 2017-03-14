using System.ComponentModel.DataAnnotations;

namespace TravelGuide.ViewModels.AccountViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Username")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The {0} must be between 3 and 20 characters long.")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}