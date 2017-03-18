using System;
using System.Collections.Generic;

namespace TravelGuide.Models.Gallery
{
    public class GalleryImage
    {
        public GalleryImage()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.Now;
            this.IsDeleted = false;
            this.Comments = new HashSet<GalleryComment>();
            this.Likes = new HashSet<GalleryLike>();
        }

        public GalleryImage(string title, string imageUrl, string userId, User user)
            : this()
        {
            this.Title = title;
            this.ImageUrl = imageUrl;
            this.UserId = userId;
            this.User = user;
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<GalleryComment> Comments { get; set; }

        public virtual ICollection<GalleryLike> Likes { get; set; }
    }
}
