using System.Collections.Generic;
using TravelGuide.Models;

namespace TravelGuide.Services.Account.Contracts
{
    public interface IUserService
    {
        User GetById(string id);
        
        IEnumerable<User> GetAllUsers();

        bool IsExternalAccountActive(string username);

        void UpdateUserInfo(string id, string username, string email, string firstName, string lastName, string phone, string address, bool isDeleted);

        void DeactivateAccount(string userId);

        void ActivateAccount(string userId);
    }
}
