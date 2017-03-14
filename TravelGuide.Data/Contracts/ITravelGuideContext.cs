using System.Data.Entity;
using TravelGuide.Models;
using TravelGuide.Models.Articles;

namespace TravelGuide.Data.Contracts
{
    public interface ITravelGuideContext
    {
        IDbSet<Article> Articles { get; set; }

        IDbSet<ArticleComment> Comments { get; set; }

        IDbSet<User> Users { get; set; }

        int SaveChanges();
    }
}
