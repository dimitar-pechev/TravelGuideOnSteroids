using System;
using System.Collections.Generic;
using TravelGuide.Models.Requests;
using TravelGuide.Models.Store;

namespace TravelGuide.Services.Requests.Contracts
{
    public interface IRequestService
    {
        int GetRequestsPagesCountForUser(string userId, int pageSize);

        IEnumerable<Request> GetRequestsForUser(string userId, int page, int pageSize);

        void MakeRequest(IEnumerable<StoreItem> items, string id, string firstName, string lastName, string phone, string address);

        IEnumerable<Request> GetAllRequests();
        
        IEnumerable<Request> GetRequestsByPage(string query, int page, int pageSize);

        int GetTotalPagesCount(string query, int pageSize);

        Request GetById(Guid orderId);

        void UpdateRequestInfo(Guid orderId, string firstName, string lastName, string phone, string address, bool status);
    }
}
