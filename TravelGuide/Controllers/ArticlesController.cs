using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TravelGuide.Common.Contracts;
using TravelGuide.Models.Articles;
using TravelGuide.Services.Articles.Contracts;
using TravelGuide.ViewModels.ArticlesViewModels;

namespace TravelGuide.Controllers
{
    public class ArticlesController : Controller
    {
        private const int PageSize = 5;
        private readonly IArticleService articleService;
        private readonly IMappingService mappingService;

        public ArticlesController(IArticleService service, IMappingService mappingService)
        {
            this.articleService = service;
            this.mappingService = mappingService;
        }

        public ActionResult Index(string query, int? page)
        {
            IEnumerable<Article> articles = new List<Article>();
            if (!string.IsNullOrEmpty(query))
            {
                articles = this.articleService.GetArticlesByKeyword(query);
                this.ViewBag.Query = query;
            }
            else
            {
                articles = this.articleService.GetAllNotDeletedArticlesOrderedByDate();
            }

            var pagesCount = Math.Ceiling((decimal)articles.Count() / PageSize);
            this.ViewBag.PagesCount = pagesCount;

            page = this.GetPage(page, pagesCount);

            this.ViewBag.CurrentPage = page;
            articles = articles.Skip(((int)page - 1) * PageSize).Take(PageSize).ToList();

            return this.View(articles);
        }

        public ActionResult Search(string query, int? page)
        {
            IEnumerable<Article> articles = new List<Article>();
            if (!string.IsNullOrEmpty(query))
            {
                articles = this.articleService.GetArticlesByKeyword(query);
                this.ViewBag.Query = query;
            }
            else
            {
                articles = this.articleService.GetAllNotDeletedArticlesOrderedByDate();
            }

            var pagesCount = Math.Ceiling((decimal)articles.Count() / PageSize);
            this.ViewBag.PagesCount = pagesCount;

            page = this.GetPage(page, pagesCount);

            this.ViewBag.CurrentPage = page;
            articles = articles.Skip(((int)page - 1) * PageSize).Take(PageSize).ToList();

            return this.PartialView("_ArticlesListPartial", articles);
        }

        public ActionResult CreateArticle()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult CreateArticle(CreateEditArticleViewModel article)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(article);
            }

            var userId = this.User.Identity.GetUserId();
            this.articleService.CreateArticle(userId, article.Title, article.City, article.Country, article.ContentMain, article.ContentMustSee, article.ContentBudgetTips, article.ContentAccomodation, article.PrimaryImageUrl, article.SecondImageUrl, article.ThirdImageUrl, article.CoverImageUrl);

            return this.RedirectToAction("Index");
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Index");
            }

            var article = this.articleService.GetArticleById((Guid)id);

            return this.View(article);
        }

        public ActionResult EditArticle(Guid? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Index");
            }

            var article = this.articleService.GetArticleById((Guid)id);

            var articleViewModel = this.mappingService.Map<CreateEditArticleViewModel>(article);

            return this.View(articleViewModel);
        }

        [HttpPost]
        public ActionResult EditArticle(CreateEditArticleViewModel article)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(article);
            }

            this.articleService.EditArticle(article.Id, article.Title, article.City, article.Country, article.ContentMain, article.ContentMustSee, article.ContentBudgetTips, article.ContentAccomodation, article.PrimaryImageUrl, article.SecondImageUrl, article.ThirdImageUrl, article.CoverImageUrl);

            return this.RedirectToAction("Details", new { id = article.Id });
        }

        public ActionResult DeleteArticle(Guid? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Index");
            }

            var article = this.articleService.GetArticleById((Guid)id);

            this.articleService.DeleteArticle(article);

            return this.RedirectToAction("Index");
        }

        protected int GetPage(int? page, decimal pagesCount)
        {
            if (page == null || page < 1 || page > pagesCount)
            {
                page = 1;
            }

            return (int)page;
        }
    }
}