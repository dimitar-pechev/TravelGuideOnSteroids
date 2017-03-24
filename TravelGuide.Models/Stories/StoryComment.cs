using System;
using TravelGuide.Models.Abstractions;

namespace TravelGuide.Models.Stories
{
    public class StoryComment : Comment
    {
        public StoryComment()
            : base()
        {
        }

        public StoryComment(string userId, User user, string content, Guid storyId)
            : base()
        {
            this.UserId = userId;
            this.User = user;
            this.Content = content;
            this.StoryId = storyId;
        }

        public Guid StoryId { get; set; }

        public virtual Story Story { get; set; }
    }
}
