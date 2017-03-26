using System.Collections.Generic;
using System.Web.Mvc;
using TravelGuide.Areas.Admin.ViewModels;
using TravelGuide.Common;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Requests.Contracts;

namespace TravelGuide.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrdersManagerController : Controller
    {
        private readonly IUserService userService;
        private readonly IMappingService mappingService;
        private readonly IRequestService requestService;
        private readonly IUtilitiesService utils;

        public OrdersManagerController(IUserService userService, IMappingService mappingService, IRequestService requestService, IUtilitiesService utils)
        {
            this.userService = userService;
            this.mappingService = mappingService;
            this.requestService = requestService;
            this.utils = utils;
        }

        public ActionResult Index(string query, int? page)
        {
            var pagesCount = this.requestService.GetTotalPagesCount(query);
            var currentPage = this.utils.GetPage(page, pagesCount);
            var requests = this.requestService.GetRequestsByPage(query, currentPage, AppConstants.AdminOrdersPageSize);
            var mappedRequests = this.mappingService.Map<IEnumerable<OrderViewModel>>(requests);
            var model = this.mappingService.Map<OrdersManagerViewModel>(mappedRequests);
            model = this.utils.AssignViewParams(model, query, currentPage, pagesCount, AppConstants.AdminOrdersBaseUrl);

            return this.View(model);
        }        
    }
}