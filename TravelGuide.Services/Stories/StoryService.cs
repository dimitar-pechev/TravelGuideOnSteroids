using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TravelGuide.Common;
using TravelGuide.Data.Contracts;
using TravelGuide.Models.Stories;
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

        public Story GetById(Guid id)
        {
            var story = this.context.Stories.Find(id);
            return story;
        }

        public int GetPagesCount(string query)
        {
            int storiesCount;
            if (!string.IsNullOrEmpty(query))
            {
                storiesCount = this.context.Stories
                    .Where(x => (x.Title.ToLower().Contains(query.ToLower()) ||
                    x.RelatedDestination.ToLower().Contains(query.ToLower())) &&
                    !x.IsDeleted)
                    .Count();
            }
            else
            {
                storiesCount = this.context.Stories
                    .Where(x => !x.IsDeleted)
                    .Count();
            }

            int pagesCount;
            if (storiesCount == 0)
            {
                pagesCount = 1;
            }
            else
            {
                pagesCount = (int)Math.Ceiling((decimal)storiesCount / AppConstants.StoriesPageSize);
            }

            return pagesCount;
        }

        public IEnumerable<Story> GetStoriesByPage(string query, int page, int pageSize)
        {
            var images = new List<Story>();
            if (!string.IsNullOrEmpty(query))
            {
                images = this.context.Stories
                     .Include(x => x.Comments)
                     .Include(x => x.Likes)
                     .Where(x => (x.Title.ToLower().Contains(query.ToLower()) ||
                     x.RelatedDestination.ToLower().Contains(query.ToLower())) &&
                     !x.IsDeleted)
                     .ToList()
                     .OrderByDescending(x => x.CreatedOn)
                     .Skip((page - 1) * pageSize)
                     .Take(pageSize)
                     .ToList();
            }
            else
            {
                images = this.context.Stories
                    .Include(x => x.Comments)
                    .Include(x => x.Likes)
                    .Where(x => !x.IsDeleted)
                    .ToList()
                    .OrderByDescending(x => x.CreatedOn)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }

            return images;
        }
    }
}
