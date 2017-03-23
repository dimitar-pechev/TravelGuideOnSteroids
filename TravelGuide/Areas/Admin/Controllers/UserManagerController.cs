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

        public UserManagerController(IUserService userService, IMappingService mappingService)
        {
            this.userService = userService;
            this.mappingService = mappingService;
        }

        public ActionResult Index(string query, int? page)
        {
            var pagesCount = this.userService.GetPagesCount(query);
            var currentPage = this.GetPage(page, pagesCount);
            var users = this.userService.GetUsersByPage(query, currentPage, AppConstants.AdminPageUsersPageSize);
            var mappedUsers = this.mappingService.Map<IEnumerable<UserViewModel>>(users);
            var model = this.mappingService.Map<UserManagerViewModel>(mappedUsers);
            model = this.AssignViewParams(model, query, currentPage, pagesCount, AppConstants.AdminUserManagerBaseUrl);

            return this.View(model);
        }

        public ActionResult Search(string query, int? page)
        {
            var pagesCount = this.userService.GetPagesCount(query);
            var currentPage = this.GetPage(page, pagesCount);
            var users = this.userService.GetUsersByPage(query, currentPage, AppConstants.AdminPageUsersPageSize);
            var mappedUsers = this.mappingService.Map<IEnumerable<UserViewModel>>(users);
            var model = this.mappingService.Map<UserManagerViewModel>(mappedUsers);
            model = this.AssignViewParams(model, query, currentPage, pagesCount, AppConstants.AdminUserManagerBaseUrl);

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

            var page = this.ExtractPageFromQuery(queryString);
            var query = this.ExtractSearchQueryFromQuery(queryString);

            this.userService.UpdateUserInfo(userId, user.UserName, user.Email, user.FirstName, user.LastName, user.PhoneNumber, user.Address, user.IsDeleted);

            var pagesCount = this.userService.GetPagesCount(query);
            var currentPage = this.GetPage(page, pagesCount);
            var users = this.userService.GetUsersByPage(query, currentPage, AppConstants.AdminPageUsersPageSize);
            var mappedUsers = this.mappingService.Map<IEnumerable<UserViewModel>>(users);
            var model = this.mappingService.Map<UserManagerViewModel>(mappedUsers);
            model = this.AssignViewParams(model, query, currentPage, pagesCount, AppConstants.AdminUserManagerBaseUrl);

            return this.PartialView("_UsersManagementPartial", model);
        }

        private int GetPage(int? page, int pagesCount)
        {
            int result;
            if (page == null || page < 1 || page > pagesCount)
            {
                result = 1;
            }
            else
            {
                result = (int)page;
            }

            return result;
        }

        private UserManagerViewModel AssignViewParams(UserManagerViewModel model, string query, int currentPage, int pagesCount, string baseUrl)
        {
            model.Query = query;
            model.CurrentPage = currentPage;
            model.PreviousPage = currentPage - 1;
            model.NextPage = currentPage + 1;
            model.PagesCount = pagesCount;
            model.BaseUrl = baseUrl;

            return model;
        }

        private int? ExtractPageFromQuery(string query)
        {
            var queryParams = query.Split('?', '&').Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim()).ToList();

            var pageQuery = queryParams.FirstOrDefault(x => x.Contains("page"));
            if (pageQuery == null)
            {
                return null;
            }

            int page;
            var isParsable = int.TryParse(pageQuery.Split('=').ToList().Last(), out page);

            if (page == 0)
            {
                return null;
            }

            return page;
        }

        private string ExtractSearchQueryFromQuery(string query)
        {
            var queryParams = query.Split('?', '&').Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim()).ToList();

            var searchQuery = queryParams.FirstOrDefault(x => x.Contains("query"));
            if (searchQuery == null)
            {
                return null;
            }

            var result = searchQuery.Split('=').ToList().Last();

            return result;
        }
    }
}