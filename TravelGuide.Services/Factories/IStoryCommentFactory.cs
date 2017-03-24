using System;
using TravelGuide.Models;
using TravelGuide.Models.Stories;

namespace TravelGuide.Services.Factories
{
    public interface IStoryCommentFactory
    {
        StoryComment CreateStoryComment(string userId, User user, string content, Guid storyId);
    }
}
