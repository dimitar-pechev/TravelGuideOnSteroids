using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TravelGuide.Common;
using TravelGuide.Data.Contracts;
using TravelGuide.Models.Requests;
using TravelGuide.Models.Store;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Factories;
using TravelGuide.Services.Requests.Contracts;

namespace TravelGuide.Services.Requests
{
    public class RequestService : IRequestService
    {
        protected readonly ITravelGuideContext context;
        protected readonly IUserService userService;
        protected readonly IRequestFactory factory;

        public RequestService(ITravelGuideContext context, IUserService userService, IRequestFactory factory)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }

            if (userService == null)
            {
                throw new ArgumentNullException();
            }

            if (factory == null)
            {
                throw new ArgumentNullException();
            }

            this.context = context;
            this.userService = userService;
            this.factory = factory;
        }

        public void MakeRequest(IEnumerable<StoreItem> items, string id, string firstName, string lastName, string phone, string address)
        {
            if (items == null)
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(phone))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(address))
            {
                throw new ArgumentNullException();
            }

            var user = this.context.Users.Find(id);
            if (user == null)
            {
                throw new InvalidOperationException();
            }

            foreach (var item in items)
            {
                var request = this.factory.CreateRequest(item.Id, item, user.Id, user, firstName, lastName, phone, address);
                this.context.Requests.Add(request);
            }

            this.context.SaveChanges();
        }

        public IEnumerable<Request> GetAllRequests()
        {
            var requests = this.context.Requests.ToList();
            return requests;
        }

        public IEnumerable<Request> GetRequestsForUser(string userId, int page, int pageSize)
        {
            var requests = this.context.Requests
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.CreatedOn)
                .ToList()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return requests;
        }

        public int GetRequestsPagesCountForUser(string userId, int pageSize)
        {
            var requestsCount = this.context.Requests
                .Where(x => x.UserId == userId)
                .Count();

            int pagesCount;
            if (requestsCount == 0)
            {
                pagesCount = 1;
            }
            else
            {
                pagesCount = (int)Math.Ceiling((decimal)requestsCount / pageSize);
            }

            return pagesCount;
        }

        public IEnumerable<Request> GetRequestsByPage(string query, int page, int pageSize)
        {
            var requests = new List<Request>();
            if (!string.IsNullOrEmpty(query))
            {
                requests = this.context.Requests
                     .Include(x => x.StoreItem)
                     .Where(x => x.User.UserName.ToLower().Contains(query.ToLower()) ||
                     x.StoreItem.ItemName.ToLower().Contains(query.ToLower()) ||
                     x.FirstName.ToLower().Contains(query.ToLower()) ||
                     x.LastName.ToLower().Contains(query.ToLower()))
                     .ToList()
                     .OrderByDescending(x => x.CreatedOn)
                     .Skip((page - 1) * pageSize)
                     .Take(pageSize)
                     .ToList();
            }
            else
            {
                requests = this.context.Requests
                    .Include(x => x.StoreItem)
                    .ToList()
                    .OrderByDescending(x => x.CreatedOn)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }

            return requests;
        }

        public int GetTotalPagesCount(string query, int pageSize)
        {
            int requestsCount;
            if (!string.IsNullOrEmpty(query))
            {
                requestsCount = this.context.Requests
                   .Where(x => x.User.UserName.ToLower().Contains(query.ToLower()) ||
                     x.StoreItem.ItemName.ToLower().Contains(query.ToLower()) ||
                     x.FirstName.ToLower().Contains(query.ToLower()) ||
                     x.LastName.ToLower().Contains(query.ToLower()))
                    .Count();
            }
            else
            {
                requestsCount = this.context.Requests
                    .Count();
            }

            int pagesCount;
            if (requestsCount == 0)
            {
                pagesCount = 1;
            }
            else
            {
                pagesCount = (int)Math.Ceiling((decimal)requestsCount / pageSize);
            }

            return pagesCount;
        }

        public Request GetById(Guid orderId)
        {
            var request = this.context.Requests.Find(orderId);
            return request;
        }

        public void UpdateRequestInfo(Guid orderId, string firstName, string lastName, string phone, string address, bool status)
        {
            var request = this.context.Requests.Find(orderId);

            if (request == null)
            {
                throw new InvalidOperationException("No such request found!");
            }

            var parsedStatus = status == true ? "Confirmed!" : "Awaiting Confirmation";

            request.Address = address;
            request.Phone = phone;
            request.FirstName = firstName;
            request.LastName = lastName;
            request.Status = parsedStatus;

            this.context.SaveChanges();
        }
    }
}
