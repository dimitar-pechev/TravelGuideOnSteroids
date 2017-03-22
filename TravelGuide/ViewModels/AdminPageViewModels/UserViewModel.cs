using System.ComponentModel.DataAnnotations;
using TravelGuide.Common.Contracts;
using TravelGuide.Models;

namespace TravelGuide.ViewModels.AdminPageViewModels
{
    public class UserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public bool IsDeleted { get; set; }
    }
}