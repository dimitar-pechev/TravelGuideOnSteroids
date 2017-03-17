using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelGuide.Areas.Store.ViewModels;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Store.Contracts;

namespace TravelGuide.Areas.Store.Controllers
{
    public class CartController : Controller
    {
        private const string CookieName = "store-items";
        private readonly ICartService cartService;
        private readonly IUserService userService;
        private readonly IMappingService mappingService;

        public CartController(ICartService cartService, IUserService userService, IMappingService mappingService)
        {
            this.cartService = cartService;
            this.userService = userService;
            this.mappingService = mappingService;
        }

        public ActionResult Index()
        {
            var cookie = this.Request.Cookies[CookieName + this.User.Identity.Name];
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
                quantity = 1;
            }

            var cookiePrev = this.Request.Cookies.Get(CookieName + this.User.Identity.Name);
            var cookie = this.cartService.WriteCookie(cookiePrev, this.User.Identity.Name, itemId.ToString(), quantity.ToString());
            this.Response.SetCookie(cookie);
        }
    }
}