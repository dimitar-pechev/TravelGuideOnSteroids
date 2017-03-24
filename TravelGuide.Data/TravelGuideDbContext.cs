using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using TravelGuide.Data.Contracts;
using TravelGuide.Models;
using TravelGuide.Models.Articles;
using TravelGuide.Models.Gallery;
using TravelGuide.Models.Requests;
using TravelGuide.Models.Store;
using TravelGuide.Models.Stories;

namespace TravelGuide.Data
{
    public class TravelGuideContext : IdentityDbContext<User>, ITravelGuideContext
    {
        public TravelGuideContext()
            : base("TravelGuideMvc")
        {
        }

        public IDbSet<Story> Stories { get; set; }

        public IDbSet<StoryComment> StoryComments { get; set; }

        public IDbSet<StoryLike> StoryLikes { get; set; }

        public IDbSet<GalleryImage> GalleryImages { get; set; }

        public IDbSet<GalleryComment> GalleryComments { get; set; }

        public IDbSet<GalleryLike> GalleryLikes { get; set; }

        public IDbSet<Request> Requests { get; set; }

        public IDbSet<StoreItem> StoreItems { get; set; }

        public IDbSet<Article> Articles { get; set; }

        public IDbSet<ArticleComment> Comments { get; set; }

        public static TravelGuideContext Create()
        {
            return new TravelGuideContext();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
