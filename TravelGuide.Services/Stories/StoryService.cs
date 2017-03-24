using System;
using TravelGuide.Data.Contracts;
using TravelGuide.Services.Factories;
using TravelGuide.Services.Stories.Contracts;

namespace TravelGuide.Services.Stories
{
    public class StoryService : IStoryService
    {
        private readonly ITravelGuideContext context;
        private readonly IStoryFactory storyFactory;

        public StoryService(ITravelGuideContext context, IStoryFactory storyFactory)
        {
            this.context = context;
            this.storyFactory = storyFactory;
        }

        public void CreateStory(string title, string content, string relatedDestination, string imageUrl, string userId)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("Story title cannot be null or empty!");
            }

            if (string.IsNullOrEmpty(relatedDestination))
            {
                throw new ArgumentNullException("Story relatedDestination cannot be null or empty!");
            }

            if (string.IsNullOrEmpty(imageUrl))
            {
                throw new ArgumentNullException("Story imageUrl cannot be null or empty!");
            }

            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException("Story content cannot be null or empty!");
            }

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("User ID cannot be null or empty!");
            }

            var user = this.context.Users.Find(userId);

            if (user == null)
            {
                throw new InvalidOperationException("Only logged in users can create stories!");
            }

            var story = this.storyFactory.CreateStory(title, content, relatedDestination, imageUrl, userId, user);
            this.context.Stories.Add(story);
            this.context.SaveChanges();
        }
    }
}
