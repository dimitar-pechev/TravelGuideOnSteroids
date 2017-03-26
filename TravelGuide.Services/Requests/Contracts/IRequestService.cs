using System.Collections.Generic;
using TravelGuide.Models.Requests;
using TravelGuide.Models.Store;

namespace TravelGuide.Services.Requests.Contracts
{
    public interface IRequestService
    {
        int GetRequestsPagesCount(string userId);

        IEnumerable<Request> GetRequestsForUser(string userId, int page);

        void MakeRequest(IEnumerable<StoreItem> items, string id, string firstName, string lastName, string phone, string address);

        IEnumerable<Request> GetAllRequests();

        void ChangeStatus(string id);

        IEnumerable<Request> GetRequestsByPage(string query, int page, int pageSize);

        int GetTotalPagesCount(string query);
    }
}
