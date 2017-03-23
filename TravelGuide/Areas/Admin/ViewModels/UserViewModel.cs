using System.ComponentModel.DataAnnotations;
using TravelGuide.Common.Contracts;
using TravelGuide.Models;

namespace TravelGuide.Areas.Admin.ViewModels
{
    public class UserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [Display(Name = "Status")]
        public bool IsDeleted { get; set; }
    }
}