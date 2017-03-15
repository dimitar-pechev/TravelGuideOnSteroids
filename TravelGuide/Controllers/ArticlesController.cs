using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TravelGuide.Models.Articles;
using TravelGuide.Services.Articles.Contracts;
using TravelGuide.ViewModels.ArticlesViewModels;

namespace TravelGuide.Controllers
{
    public class ArticlesController : Controller
    {
        private const int PageSize = 3;
        private readonly IArticleService service;

        public ArticlesController(IArticleService service)
        {
            this.service = service;
        }

        public ActionResult Index(string query, int? page)
        {
            IEnumerable<Article> articles = new List<Article>();
            if (!string.IsNullOrEmpty(query))
            {
                articles = this.service.GetArticlesByKeyword(query);
                this.ViewBag.Query = query;
            }
            else
            {
                articles = this.service.GetAllArticles();
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
                articles = this.service.GetArticlesByKeyword(query);
                this.ViewBag.Query = query;
            }
            else
            {
                articles = this.service.GetAllArticles();
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
        public ActionResult CreateArticle(CreateArticleViewModel article)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(article);
            }

            var userId = this.User.Identity.GetUserId();
            this.service.CreateArticle(userId, article.Title, article.City, article.Country, article.ContentMain, article.ContentMustSee, article.ContentBudgetTips, article.ContentAccomodation, article.PrimaryImageUrl, article.SecondImageUrl, article.ThirdImageUrl, article.CoverImageUrl);

            return this.RedirectToAction("Index");
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Index");
            }

            var article = this.service.GetArticleById((Guid)id);

            return this.View(article);
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