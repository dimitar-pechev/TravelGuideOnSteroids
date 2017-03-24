using TravelGuide.Models;
using TravelGuide.Models.Stories;

namespace TravelGuide.Services.Factories
{
    public interface IStoryFactory
    {
        Story CreateStory(string title, string content, string relatedDestination, string imageUrl, string userId, User user);
    }
}
