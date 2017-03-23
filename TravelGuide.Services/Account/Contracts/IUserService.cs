using System.Collections.Generic;
using TravelGuide.Models;

namespace TravelGuide.Services.Account.Contracts
{
    public interface IUserService
    {
        User GetById(string id);

        IEnumerable<User> GetAllUsers();

        int GetPagesCount(string query);

        IEnumerable<User> GetUsersByPage(string query, int page, int pageSize);

        void UpdateUserInfo(string id, string username, string email, string firstName, string lastName, string phone, string address, bool isDeleted);

        void DeactivateAccount(string userId);

        void ActivateAccount(string userId);
    }
}
