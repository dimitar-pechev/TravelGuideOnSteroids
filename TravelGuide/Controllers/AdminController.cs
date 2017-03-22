using System.Web.Mvc;
using TravelGuide.Services.Account.Contracts;

namespace TravelGuide.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService userService;

        public AdminController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Index()
        {
            var users = this.userService.GetAllUsers();
            return this.View(users);
        }

        public ActionResult Users()
        {
            var users = this.userService.GetAllUsers();

            return this.PartialView("_UsersManagementPartial", users);
        }
    }
}