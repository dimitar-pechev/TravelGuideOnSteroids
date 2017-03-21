using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TravelGuide.Areas.Store.ViewModels;
using TravelGuide.Common;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Requests.Contracts;
using TravelGuide.Services.Store.Contracts;

namespace TravelGuide.Areas.Store.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly IUserService userService;
        private readonly IMappingService mappingService;
        private readonly IRequestService requestService;

        public CartController(ICartService cartService, IUserService userService, IMappingService mappingService, IRequestService requestService)
        {
            this.cartService = cartService;
            this.userService = userService;
            this.mappingService = mappingService;
            this.requestService = requestService;
        }

        public ActionResult Index()
        {
            var cookie = this.Request.Cookies[AppConstants.CartCookieName + this.User.Identity.Name];
            var items = this.cartService.ExtractItemsFromCookie(cookie);
            var user = this.userService.GetById(this.User.Identity.GetUserId());

            var model = this.mappingService.Map<CartViewModel>(user);
            var itemsViewModelCollection = this.mappingService.Map<IEnumerable<StoreItemCartViewModel>>(items);
            model.StoreItems = itemsViewModelCollection;

            return this.View(model);
        }

        [HttpPost]
        public void AddToCart(Guid? itemId, int? quantity)
        {
            if (quantity == null || quantity < 1 || quantity > 10)
            {
                throw new ArgumentException("You must specify the desired quantity (in the range between 1 and 10)!");
            }

            var cookiePrev = this.Request.Cookies.Get(AppConstants.CartCookieName + this.User.Identity.Name);
            var cookie = this.cartService.WriteCookie(cookiePrev, this.User.Identity.Name, itemId.ToString(), quantity.ToString());
            this.Response.SetCookie(cookie);
        }

        [HttpPost]
        public ActionResult RemoveItem(Guid? id)
        {
            var cookie = this.Request.Cookies[AppConstants.CartCookieName + this.User.Identity.Name];

            cookie = this.cartService.DeleteItemFromCookie(cookie, id.ToString());

            var items = this.cartService.ExtractItemsFromCookie(cookie);
            var user = this.userService.GetById(this.User.Identity.GetUserId());

            var model = this.mappingService.Map<CartViewModel>(user);
            var itemsViewModelCollection = this.mappingService.Map<IEnumerable<StoreItemCartViewModel>>(items);
            model.StoreItems = itemsViewModelCollection;

            this.Response.Cookies.Add(cookie);

            return this.PartialView("_CartItemsPartial", model);
        }

        [HttpPost]
        public ActionResult CheckOut(CartViewModel model)
        {
            var cookie = this.Request.Cookies[AppConstants.CartCookieName + this.User.Identity.Name];
            var user = this.userService.GetById(this.User.Identity.GetUserId());

            var items = this.cartService.ExtractItemsFromCookie(cookie);

            this.requestService.MakeRequest(items, this.User.Identity.GetUserId(), model.FirstName, model.LastName, model.PhoneNumber, model.Address);

            cookie = this.cartService.GetClearedCookie(this.User.Identity.Name);
            this.Response.Cookies.Add(cookie);

            return this.PartialView("_CartItemsPartial", model);
        }
    }
}