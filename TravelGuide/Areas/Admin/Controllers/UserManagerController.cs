using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TravelGuide.Areas.Admin.ViewModels;
using TravelGuide.Common;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Account.Contracts;

namespace TravelGuide.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserManagerController : Controller
    {
        private readonly IUserService userService;
        private readonly IMappingService mappingService;
        private readonly IUtilitiesService utils;

        public UserManagerController(IUserService userService, IMappingService mappingService, IUtilitiesService utils)
        {
            this.userService = userService;
            this.mappingService = mappingService;
            this.utils = utils;
        }

        public ActionResult Index(string query, int? page, string filter, string sort)
        {
            var pagesCount = this.userService.GetPagesCount(query);
            var currentPage = this.utils.GetPage(page, pagesCount);
            var users = this.userService.GetUsersByPage(query, currentPage, AppConstants.AdminPageUsersPageSize);
            var mappedUsers = this.mappingService.Map<IEnumerable<UserViewModel>>(users);
            var model = this.mappingService.Map<UserManagerViewModel>(mappedUsers);
            model = this.utils.AssignViewParams(model, query, currentPage, pagesCount, AppConstants.AdminUserManagerBaseUrl);

            return this.View(model);
        }

        public ActionResult Search(string query, int? page, string filter, string sort)
        {
            var pagesCount = this.userService.GetPagesCount(query);
            var currentPage = this.utils.GetPage(page, pagesCount);
            var users = this.userService.GetUsersByPage(query, currentPage, AppConstants.AdminPageUsersPageSize);
            var mappedUsers = this.mappingService.Map<IEnumerable<UserViewModel>>(users);
            var model = this.mappingService.Map<UserManagerViewModel>(mappedUsers);
            model = this.utils.AssignViewParams(model, query, currentPage, pagesCount, AppConstants.AdminUserManagerBaseUrl);

            return this.View("Index", model);
        }

        public ActionResult UsersEditForm(UserViewModel model)
        {
            return this.PartialView("_EditUserPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserInfo(string userId, UserViewModel user, string queryString)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException();
            }

            var page = this.utils.ExtractPageFromQuery(queryString);
            var query = this.utils.ExtractSearchQueryFromQuery(queryString);

            this.userService.UpdateUserInfo(userId, user.UserName, user.Email, user.FirstName, user.LastName, user.PhoneNumber, user.Address, user.IsDeleted);

            var pagesCount = this.userService.GetPagesCount(query);
            var currentPage = this.utils.GetPage(page, pagesCount);
            var users = this.userService.GetUsersByPage(query, currentPage, AppConstants.AdminPageUsersPageSize);
            var mappedUsers = this.mappingService.Map<IEnumerable<UserViewModel>>(users);
            var model = this.mappingService.Map<UserManagerViewModel>(mappedUsers);
            model = this.utils.AssignViewParams(model, query, currentPage, pagesCount, AppConstants.AdminUserManagerBaseUrl);

            return this.PartialView("_UsersManagementPartial", model);
        }
    }
}