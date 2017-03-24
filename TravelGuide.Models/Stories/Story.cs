using System;
using System.Collections.Generic;

namespace TravelGuide.Models.Stories
{
    public class Story
    {
        public Story()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.Now;
            this.IsDeleted = false;
            this.Comments = new HashSet<StoryComment>();
            this.Likes = new HashSet<StoryLike>();
        }

        public Story(string title, string content, string relatedDestination, string imageUrl, string userId, User user)
            : this()
        {
            this.Title = title;
            this.Content = content;
            this.RelatedDestination = relatedDestination;
            this.ImageUrl = imageUrl;
            this.UserId = userId;
            this.User = user;
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string RelatedDestination { get; set; }

        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<StoryComment> Comments { get; set; }

        public virtual ICollection<StoryLike> Likes { get; set; }
    }
}
