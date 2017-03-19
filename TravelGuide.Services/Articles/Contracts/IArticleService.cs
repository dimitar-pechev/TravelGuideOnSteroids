using System;
using System.Collections.Generic;
using TravelGuide.Models.Articles;

namespace TravelGuide.Services.Articles.Contracts
{
    public interface IArticleService
    {
        IEnumerable<Article> GetFilteredArticlesByPage(string query, int page, int pageSize);

        int GetPagesCount(string query);

        IEnumerable<Article> GetAllArticles();

        IEnumerable<Article> GetAllNotDeletedArticlesOrderedByDate();

        IEnumerable<Article> GetArticlesByKeyword(string keyword);

        Article GetArticleById(Guid id);

        void CreateArticle(string id, string title, string city, string country, string contentMain,
            string contentMustSee, string contentBudgetTips, string contentAccomodation,
            string primaryImageUrl, string secondImageUrl, string thirdImageUrl, string coverImageUrl);

        void EditArticle(Guid articleID, string title, string city, string country, string contentMain,
            string contentMustSee, string contentBudgetTips, string contentAccomodation,
            string primaryImageUrl, string secondImageUrl, string thirdImageUrl, string coverImageUrl);

        void DeleteArticle(Article article);

        void DeleteComment(string commentId);

        void AddComment(string id, string content, Guid articleId);
    }
}
