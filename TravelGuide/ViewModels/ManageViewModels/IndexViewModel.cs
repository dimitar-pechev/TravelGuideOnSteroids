using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using TravelGuide.Common.Contracts;
using TravelGuide.Models;
using TravelGuide.Models.Requests;
using TravelGuide.Shared;

namespace TravelGuide.ViewModels.ManageViewModels
{
    public class IndexViewModel : IMapFrom<User>, IPager
    {
        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }

        public IEnumerable<Request> Requests { get; set; }

        public string BaseUrl { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage { get; set; }

        public int NextPage { get; set; }

        public string Query { get; set; }
    }
}