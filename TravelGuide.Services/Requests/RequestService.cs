﻿using System;
using System.Collections.Generic;
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

        public void ChangeStatus(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException();
            }

            var parsedId = Guid.Parse(id);
            var request = this.context.Requests.Find(parsedId);

            if (request == null)
            {
                throw new InvalidOperationException();
            }

            request.Status = "Confirmed!";
            this.context.SaveChanges();
        }

        public IEnumerable<Request> GetRequestsForUser(string userId, int page)
        {
            var requests = this.context.Requests
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.CreatedOn)
                .ToList()
                .Skip((page - 1) * AppConstants.ProfilePageCount)
                .Take(AppConstants.ProfilePageCount)
                .ToList();
            return requests;
        }

        public int GetRequestsPagesCount(string userId)
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
                pagesCount = (int)Math.Ceiling((decimal)requestsCount / AppConstants.ProfilePageCount);
            }

            return pagesCount;
        }
    }
}
