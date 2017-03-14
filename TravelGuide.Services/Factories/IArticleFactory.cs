using TravelGuide.Models;
using TravelGuide.Models.Articles;

namespace TravelGuide.Services.Factories
{
    public interface IArticleFactory
    {
        Article CreateArticle(User user, string userId, string title, string city, string country, string contentMain,
            string contentMustSee, string contentBudgetTips, string contentAccomodation,
            string primaryImageUrl, string secondImageUrl, string thirdImageUrl, string coverImageUrl);
    }
}
