using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Mvc;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.ViewModels.AdminPageViewModels;

namespace TravelGuide.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService userService;
        private readonly IMappingService mappingService;

        public AdminController(IUserService userService, IMappingService mappingService)
        {
            this.userService = userService;
            this.mappingService = mappingService;
        }

        public ActionResult Index()
        {
            var users = this.userService.GetAllUsers();
            var model = this.mappingService.Map<IEnumerable<UserViewModel>>(users);

            return this.View(model);
        }

        public ActionResult Users()
        {
            var users = this.userService.GetAllUsers();

            return this.PartialView("_UsersManagementPartial", users);
        }

        public ActionResult UsersEditForm(UserViewModel model)
        {
            return this.PartialView("_EditUserPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserInfo(string userId, UserViewModel user)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException();
            }

            this.userService.UpdateUserInfo(userId, user.UserName, user.Email, user.FirstName, user.LastName, user.PhoneNumber, user.Address, user.IsDeleted);

            var users = this.userService.GetAllUsers();
            var model = this.mappingService.Map<IEnumerable<UserViewModel>>(users);

            return this.PartialView("_UsersManagementPartial", model);
        }
    }
}