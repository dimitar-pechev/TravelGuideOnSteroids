using System.Data.Entity;
using TravelGuide.Models;
using TravelGuide.Models.Articles;
using TravelGuide.Models.Store;

namespace TravelGuide.Data.Contracts
{
    public interface ITravelGuideContext
    {
        IDbSet<StoreItem> StoreItems { get; set; }

        IDbSet<Article> Articles { get; set; }

        IDbSet<ArticleComment> Comments { get; set; }

        IDbSet<User> Users { get; set; }

        int SaveChanges();
    }
}
