using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TravelGuide.Common;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Articles.Contracts;
using TravelGuide.ViewModels.ArticlesViewModels;

namespace TravelGuide.Controllers
{
    [Authorize(Roles = "admin")]
    public class ArticlesController : Controller
    {
        private readonly IArticleService articleService;
        private readonly IMappingService mappingService;

        public ArticlesController(IArticleService articleService, IMappingService mappingService)
        {
            this.articleService = articleService;
            this.mappingService = mappingService;
        }

        [AllowAnonymous]
        public ActionResult Index(string query, int? page)
        {
            var pagesCount = this.articleService.GetPagesCount(query);
            var currentPage = this.GetPage(page, pagesCount);

            var articles = this.articleService.GetFilteredArticlesByPage(query, currentPage, AppConstants.ArticlePageSize);

            this.ViewBag.Query = query;
            this.ViewBag.PagesCount = pagesCount;
            this.ViewBag.CurrentPage = currentPage;

            return this.View(articles);
        }

        [AllowAnonymous]
        public ActionResult Search(string query, int? page)
        {
            var pagesCount = this.articleService.GetPagesCount(query);
            var currentPage = this.GetPage(page, pagesCount);

            var articles = this.articleService.GetFilteredArticlesByPage(query, currentPage, AppConstants.ArticlePageSize);

            this.ViewBag.Query = query;
            this.ViewBag.PagesCount = pagesCount;
            this.ViewBag.CurrentPage = currentPage;

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

        [AllowAnonymous]
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
            var model = this.mappingService.Map<CreateEditArticleViewModel>(article);

            return this.View(model);
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

        private int GetPage(int? page, int pagesCount)
        {
            int result;
            if (page == null || page < 1 || page > pagesCount)
            {
                result = 1;
            }
            else
            {
                result = (int)page;
            }

            return result;
        }
    }
}