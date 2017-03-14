using System;
using System.Collections.Generic;
using System.Linq;
using TravelGuide.Data.Contracts;
using TravelGuide.Models.Articles;
using TravelGuide.Services.Articles.Contracts;
using TravelGuide.Services.Factories;

namespace TravelGuide.Services.Articles
{
    public class ArticleService : IArticleService
    {
        protected readonly ITravelGuideContext context;
        protected readonly IArticleFactory articleFactory;
        protected readonly IArticleCommentFactory commentFactory;

        public ArticleService(ITravelGuideContext context, IArticleFactory articleFactory, IArticleCommentFactory commentFactory)
        {
            if (context == null)
            {
                throw new ArgumentNullException("Passed DdContext cannot be null!");
            }

            if (articleFactory == null)
            {
                throw new ArgumentNullException("Passed factory cannot be null!");
            }

            if (commentFactory == null)
            {
                throw new ArgumentNullException("Passed factory cannot be null!");
            }

            this.context = context;
            this.articleFactory = articleFactory;
            this.commentFactory = commentFactory;
        }

        public void CreateArticle(string id, string title, string city, string country, string contentMain,
            string contentMustSee, string contentBudgetTips, string contentAccomodation,
            string primaryImageUrl, string secondImageUrl, string thirdImageUrl, string coverImageUrl)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("Username cannot be null!");
            }

            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("Title cannot be null!");
            }

            if (string.IsNullOrEmpty(city))
            {
                throw new ArgumentNullException("City name cannot be null!");
            }

            if (string.IsNullOrEmpty(country))
            {
                throw new ArgumentNullException("Country name cannot be null!");
            }

            if (string.IsNullOrEmpty(contentMain))
            {
                throw new ArgumentNullException("Content cannot be null!");
            }

            if (string.IsNullOrEmpty(contentMustSee))
            {
                throw new ArgumentNullException("Content cannot be null!");
            }

            if (string.IsNullOrEmpty(contentBudgetTips))
            {
                throw new ArgumentNullException("Content cannot be null!");
            }

            if (string.IsNullOrEmpty(contentAccomodation))
            {
                throw new ArgumentNullException("Content cannot be null!");
            }

            if (string.IsNullOrEmpty(primaryImageUrl))
            {
                throw new ArgumentNullException("ImageUrl cannot be null!");
            }

            if (string.IsNullOrEmpty(secondImageUrl))
            {
                throw new ArgumentNullException("ImageUrl cannot be null!");
            }

            if (string.IsNullOrEmpty(thirdImageUrl))
            {
                throw new ArgumentNullException("ImageUrl cannot be null!");
            }

            if (string.IsNullOrEmpty(coverImageUrl))
            {
                throw new ArgumentNullException("ImageUrl cannot be null!");
            }

            var user = this.context.Users.Find(id);
            if (user == null)
            {
                throw new InvalidOperationException("Only logged in users can create articles!");
            }

            var article = this.articleFactory.CreateArticle(user, user.Id, title, city, country, contentMain,
                contentMustSee, contentBudgetTips, contentAccomodation, primaryImageUrl, secondImageUrl, thirdImageUrl, coverImageUrl);

            this.context.Articles.Add(article);
            this.context.SaveChanges();
        }

        public void DeleteArticle(Article article)
        {
            if (article == null)
            {
                throw new InvalidOperationException("The passed article is null!");
            }

            var dbArticle = this.context.Articles.Find(article.Id);

            if (dbArticle == null)
            {
                throw new InvalidOperationException("The passed article is not present in the database!");
            }

            dbArticle.IsDeleted = true;
            this.context.SaveChanges();
        }

        public void EditArticle(Guid articleId, string title, string city, string country, string contentMain,
            string contentMustSee, string contentBudgetTips, string contentAccomodation,
            string primaryImageUrl, string secondImageUrl, string thirdImageUrl, string coverImageUrl)
        {
            var article = this.context.Articles.Find(articleId);

            if (article == null)
            {
                throw new InvalidOperationException("No such article found!");
            }

            article.Title = title;
            article.City = city;
            article.Country = country;
            article.ContentMain = contentMain;
            article.ContentMustSee = contentMustSee;
            article.ContentBudgetTips = contentBudgetTips;
            article.ContentAccomodation = contentAccomodation;
            article.PrimaryImageUrl = primaryImageUrl;
            article.SecondImageUrl = secondImageUrl;
            article.ThirdImageUrl = thirdImageUrl;
            article.CoverImageUrl = coverImageUrl;

            this.context.SaveChanges();
        }

        public IEnumerable<Article> GetAllArticles()
        {
            return this.context.Articles.ToList();
        }

        public IEnumerable<Article> GetAllNotDeletedArticlesOrderedByDate()
        {
            return this.context
                .Articles
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreatedOn)
                .ToList();
        }

        public Article GetArticleById(Guid id)
        {
            var article = this.context.Articles.Find(id);
            return article;
        }

        public void AddComment(string id, string content, Guid articleId)
        {
            var user = this.context.Users.Find(id);

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            var comment = this.commentFactory.CreateArticleComment(user.Id, user, content, articleId);
            var article = this.context.Articles.Find(articleId);
            article.Comments.Add(comment);

            this.context.SaveChanges();
        }

        public IEnumerable<Article> GetArticlesByKeyword(string keyword)
        {
            if (string.IsNullOrEmpty(keyword) || string.IsNullOrEmpty(keyword.Trim()))
            {
                return this.context.Articles.Where(x => !x.IsDeleted).ToList();
            }

            var keywordToLower = keyword.ToLower();
            var articles = this.context.Articles
                .Where(x => x.City.ToLower().Contains(keywordToLower) ||
                x.Country.ToLower().Contains(keywordToLower) ||
                x.Title.ToLower().Contains(keywordToLower))
                .Where(x => !x.IsDeleted)
                .ToList();

            return articles;
        }

        public void DeleteComment(string commentId)
        {
            if (string.IsNullOrEmpty(commentId))
            {
                throw new ArgumentNullException();
            }

            var id = Guid.Parse(commentId);
            var comment = this.context.Comments.Find(id);

            if (comment == null)
            {
                throw new InvalidOperationException();
            }

            this.context.Comments.Remove(comment);
            this.context.SaveChanges();
        }
    }
}
