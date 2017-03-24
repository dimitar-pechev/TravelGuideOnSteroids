using System.Data.Entity;
using TravelGuide.Models;
using TravelGuide.Models.Articles;
using TravelGuide.Models.Gallery;
using TravelGuide.Models.Requests;
using TravelGuide.Models.Store;
using TravelGuide.Models.Stories;

namespace TravelGuide.Data.Contracts
{
    public interface ITravelGuideContext
    {
        IDbSet<Story> Stories { get; set; }

        IDbSet<StoryComment> StoryComments { get; set; }

        IDbSet<StoryLike> StoryLikes { get; set; }

        IDbSet<GalleryImage> GalleryImages { get; set; }

        IDbSet<GalleryComment> GalleryComments { get; set; }

        IDbSet<GalleryLike> GalleryLikes { get; set; }

        IDbSet<Request> Requests { get; set; }

        IDbSet<StoreItem> StoreItems { get; set; }

        IDbSet<Article> Articles { get; set; }

        IDbSet<ArticleComment> Comments { get; set; }

        IDbSet<User> Users { get; set; }

        int SaveChanges();
    }
}
