using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using TravelGuide.Common.Contracts;
using TravelGuide.Models;
using TravelGuide.Models.Requests;

namespace TravelGuide.ViewModels.ManageViewModels
{
    public class IndexViewModel : IMapFrom<User>, IPager
    {
        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }

        public IEnumerable<Request> Requests { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(50, MinimumLength = 10)]
        public string Address { get; set; }

        [StringLength(15, MinimumLength = 7)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string BaseUrl { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage { get; set; }

        public int NextPage { get; set; }

        public string Query { get; set; }
    }
}