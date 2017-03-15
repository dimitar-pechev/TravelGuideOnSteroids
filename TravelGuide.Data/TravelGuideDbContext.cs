using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using TravelGuide.Data.Contracts;
using TravelGuide.Models;
using TravelGuide.Models.Articles;
using TravelGuide.Models.Store;

namespace TravelGuide.Data
{
    public class TravelGuideContext : IdentityDbContext<User>, ITravelGuideContext
    {
        public TravelGuideContext()
            : base("TravelGuideMvc")
        {
        }

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
