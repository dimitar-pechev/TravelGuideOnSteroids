using System.Collections.Generic;
using TravelGuide.Models;

namespace TravelGuide.Services.Account.Contracts
{
    public interface IUserService
    {
        User GetById(string id);
        
        IEnumerable<User> GetAllUsers();

        bool IsExternalAccountActive(string username);

        void UpdateUserInfo(string id, string firstName, string lastName, string phone, string address);

        void DeactivateAccount(string userId);

        void ActivateAccount(string userId);
    }
}
