using System;
using TravelGuide.Models.Store;

namespace TravelGuide.Models.Requests
{
    public class Request
    {
        public Request()
        {
            this.Id = Guid.NewGuid();
            this.Status = "Awaiting confirmation";
            this.CreatedOn = DateTime.Now;
        }

        public Request(Guid itemId, StoreItem item, string userId, User user, string firstName, string lastName, string phone, string address)
            : this()
        {
            this.StoreItemId = itemId;
            this.StoreItem = item;
            this.UserId = userId;
            this.User = user;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Phone = phone;
            this.Address = address;
        }

        public Guid Id { get; set; }

        public string Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid StoreItemId { get; set; }

        public virtual StoreItem StoreItem { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }
}
