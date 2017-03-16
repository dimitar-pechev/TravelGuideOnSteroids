using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelGuide.Services.Store.Contracts;

namespace TravelGuide.Areas.Store.Controllers
{
    public class CartController : Controller
    {
        private const string CookieName = "store-items";
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public ActionResult Index()
        {
            return this.View();
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