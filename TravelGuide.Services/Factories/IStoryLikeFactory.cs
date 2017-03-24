using System;
using TravelGuide.Models.Stories;

namespace TravelGuide.Services.Factories
{
    public interface IStoryLikeFactory
    {
        StoryLike CreateStoreLike(string userId, Guid storyId);
    }
}
