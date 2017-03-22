using System;
using System.Collections.Generic;
using System.Linq;
using TravelGuide.Data.Contracts;
using TravelGuide.Models;
using TravelGuide.Services.Account.Contracts;

namespace TravelGuide.Services
{
    public class UserService : IUserService
    {
        protected ITravelGuideContext context;

        public UserService(ITravelGuideContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }

            this.context = context;
        }

        public void DeactivateAccount(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException();
            }

            var user = this.context.Users.Find(userId);

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            user.IsDeleted = true;
            this.context.SaveChanges();
        }

        public void ActivateAccount(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException();
            }

            var user = this.context.Users.Find(userId);

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            user.IsDeleted = false;
            this.context.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = this.context.Users.ToList();
            return users;
        }

        public User GetById(string id)
        {
            var user = this.context.Users.Find(id);
            return user;
        }

        public void UpdateUserInfo(string id, string username, string email, string firstName, string lastName, string phone, string address, bool isDeleted)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException();
            }

            var user = this.context.Users.Find(id);

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException();
            }

            user.UserName = username;
            user.Email = email;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.PhoneNumber = phone;
            user.Address = address;
            user.IsDeleted = isDeleted;

            this.context.SaveChanges();
        }

        public bool IsExternalAccountActive(string username)
        {
            var user = this.context.Users                
                .FirstOrDefault(x => x.UserName == username);

            return !user.IsDeleted;
        }
    }
}
