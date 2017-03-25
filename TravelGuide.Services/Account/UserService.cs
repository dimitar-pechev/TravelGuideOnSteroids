using System;
using System.Collections.Generic;
using System.Linq;
using TravelGuide.Common;
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

        public void UpdateUserInfoByUser(string id, string firstName, string lastName, string phone, string address)
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
            
            user.FirstName = firstName;
            user.LastName = lastName;
            user.PhoneNumber = phone;
            user.Address = address;

            this.context.SaveChanges();
        }

        public IEnumerable<User> GetUsersByPage(string query, int page, int pageSize)
        {
            var users = new List<User>();
            if (!string.IsNullOrEmpty(query))
            {
                users = this.context.Users
                     .Where(x => x.UserName.ToLower().Contains(query.ToLower()) ||
                     x.Email.ToLower().Contains(query.ToLower()) ||
                     x.FirstName.ToLower().Contains(query.ToLower()) ||
                     x.LastName.ToLower().Contains(query.ToLower()) ||
                     x.Address.ToLower().Contains(query.ToLower()))
                     .ToList()
                     .OrderByDescending(x => x.RegisteredOn)
                     .Skip((page - 1) * pageSize)
                     .Take(pageSize)
                     .ToList();
            }
            else
            {
                users = this.context.Users
                    .ToList()
                    .OrderByDescending(x => x.RegisteredOn)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }

            return users;
        }

        public int GetPagesCount(string query)
        {
            int usersCount;
            if (!string.IsNullOrEmpty(query))
            {
                usersCount = this.context.Users
                    .Where(x => x.UserName.ToLower().Contains(query.ToLower()) ||
                     x.Email.ToLower().Contains(query.ToLower()) ||
                     x.FirstName.ToLower().Contains(query.ToLower()) ||
                     x.LastName.ToLower().Contains(query.ToLower()) ||
                     x.Address.ToLower().Contains(query.ToLower()))
                    .Count();
            }
            else
            {
                usersCount = this.context.Users
                    .Count();
            }

            int pagesCount;
            if (usersCount == 0)
            {
                pagesCount = 1;
            }
            else
            {
                pagesCount = (int)Math.Ceiling((decimal)usersCount / AppConstants.AdminPageUsersPageSize);
            }

            return pagesCount;
        }
    }
}
