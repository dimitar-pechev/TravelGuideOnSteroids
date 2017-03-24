using System;
using TravelGuide.Models.Abstractions;

namespace TravelGuide.Models.Stories
{
    public class StoryLike : Like
    {
        public StoryLike()
            : base()
        {
        }

        public StoryLike(string userId, Guid storyId)
            : base()
        {
            this.UserId = userId;
            this.StoryId = storyId;
        }

        public Guid StoryId { get; set; }

        public virtual Story Story { get; set; }
    }
}
