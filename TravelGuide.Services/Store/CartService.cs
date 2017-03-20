using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelGuide.Common;
using TravelGuide.Models.Store;
using TravelGuide.Services.Store.Contracts;

namespace TravelGuide.Services.Store
{
    public class CartService : ICartService
    {
        protected readonly IStoreService service;

        public CartService(IStoreService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException();
            }

            this.service = service;
        }

        public IEnumerable<StoreItem> ExtractItemsFromCookie(HttpCookie cookie)
        {
            var items = new List<StoreItem>();
            if (cookie == null || !cookie.Name.Contains(AppConstants.CartCookieName))
            {
                return items;
            }

            var ids = cookie.Value.Split(',').Where(x => !string.IsNullOrEmpty(x)).ToList();

            foreach (var id in ids)
            {
                var parsedId = Guid.Parse(id);
                var item = this.service.GetStoreItemById(parsedId);
                items.Add(item);
            }

            return items;
        }

        public HttpCookie WriteCookie(HttpCookie cookie, string username, string itemId, string quantity)
        {
            int parsedQuantity;
            var isParsed = int.TryParse(quantity, out parsedQuantity);

            if (!isParsed)
            {
                throw new ArgumentException();
            }

            var items = new List<string>();
            for (int i = 0; i < parsedQuantity; i++)
            {
                items.Add(itemId);
            }

            if (cookie == null || !cookie.Name.Contains(AppConstants.CartCookieName))
            {
                cookie = new HttpCookie(AppConstants.CartCookieName + username, string.Join(",", items));
            }
            else
            {
                var temp = cookie.Value;
                cookie.Value = $"{temp},{string.Join(",", items)}";
            }

            cookie.Expires = DateTime.Now.AddDays(7);
            return cookie;
        }

        public HttpCookie DeleteItemFromCookie(HttpCookie cookie, string itemId)
        {
            if (cookie == null || !cookie.Name.Contains(AppConstants.CartCookieName))
            {
                throw new ArgumentNullException("Passed cookie cannot be null!");
            }

            var ids = cookie.Value.Split(',').ToList();
            ids.Remove(itemId);
            var cookieContent = string.Join(",", ids);

            cookie.Value = cookieContent;
            return cookie;
        }

        public HttpCookie GetClearedCookie(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException();
            }

            return new HttpCookie(AppConstants.CartCookieName + username, string.Empty);
        }
    }
}
