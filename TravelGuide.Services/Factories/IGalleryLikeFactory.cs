using System;
using TravelGuide.Models.Gallery;

namespace TravelGuide.Services.Factories
{
    public interface IGalleryLikeFactory
    {
        GalleryLike CreateGalleryLike(string userId, Guid imageId);
    }
}
