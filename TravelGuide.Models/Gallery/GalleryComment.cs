using System;
using TravelGuide.Models.Abstractions;

namespace TravelGuide.Models.Gallery
{
    public class GalleryComment : Comment
    {
        public GalleryComment()
            : base()
        {
        }

        public GalleryComment(string userId, User user, string content, Guid imageId)
            : base()
        {
            this.UserId = userId;
            this.User = user;
            this.Content = content;
            this.GalleryImageId = imageId;
        }

        public Guid GalleryImageId { get; set; }

        public virtual GalleryImage GalleryImage { get; set; }
    }
}
