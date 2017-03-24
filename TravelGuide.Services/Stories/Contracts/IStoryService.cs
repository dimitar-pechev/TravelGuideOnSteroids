using System;
using System.Collections.Generic;
using TravelGuide.Models.Stories;

namespace TravelGuide.Services.Stories.Contracts
{
    public interface IStoryService
    {
        void DeleteComment(Guid commentId);

        void AddComment(Guid storyId, string userId, string content);

        void ToggleLike(Guid storyId, string userId);

        bool IsStoryLiked(Guid storyId, string userId);

        Story GetById(Guid id);

        int GetPagesCount(string query);

        IEnumerable<Story> GetStoriesByPage(string query, int page, int pageSize);

        void CreateStory(string title, string content, string relatedDestination, string imageUrl, string userId);
    }
}
