using System;

namespace TravelGuide.Models.Abstractions
{
    public abstract class Comment
    {
        public Comment()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.Now;
            this.IsDeleted = false;
        }

        public Guid Id { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
