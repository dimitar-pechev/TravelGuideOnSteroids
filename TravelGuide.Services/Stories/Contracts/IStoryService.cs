namespace TravelGuide.Services.Stories.Contracts
{
    public interface IStoryService
    {
        void CreateStory(string title, string content, string relatedDestination, string imageUrl, string userId);
    }
}
