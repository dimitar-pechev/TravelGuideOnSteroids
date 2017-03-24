using System;
using System.Collections.Generic;
using TravelGuide.Models.Stories;

namespace TravelGuide.Services.Stories.Contracts
{
    public interface IStoryService
    {
        Story GetById(Guid id);

        int GetPagesCount(string query);

        IEnumerable<Story> GetStoriesByPage(string query, int page, int pageSize);

        void CreateStory(string title, string content, string relatedDestination, string imageUrl, string userId);
    }
}
