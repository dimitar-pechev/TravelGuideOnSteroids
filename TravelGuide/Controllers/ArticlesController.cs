using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TravelGuide.Common;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Articles.Contracts;
using TravelGuide.Services.Store.Contracts;
using TravelGuide.ViewModels.ArticlesViewModels;

namespace TravelGuide.Controllers
{
    [Authorize(Roles = "admin")]
    public class ArticlesController : Controller
    {
        private readonly IArticleService articleService;
        private readonly IMappingService mappingService;
        private readonly IStoreService storeService;

        public ArticlesController(IArticleService articleService, IMappingService mappingService, IStoreService storeService)
        {
            this.articleService = articleService;
            this.mappingService = mappingService;
            this.storeService = storeService;
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
            var storeItems = this.storeService.GetItemsByKeyword(article.City);
            var model = this.mappingService.Map<ArticleDetailsViewModel>(article);
            model.StoreItems = storeItems;

            return this.View(model);
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
        [ValidateAntiForgeryToken]
        public ActionResult EditArticle(CreateEditArticleViewModel article)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(article);
            }

            this.articleService.EditArticle(article.Id, article.Title, article.City, article.Country, article.ContentMain, article.ContentMustSee, article.ContentBudgetTips, article.ContentAccomodation, article.PrimaryImageUrl, article.SecondImageUrl, article.ThirdImageUrl, article.CoverImageUrl);

            return this.RedirectToAction("Details", new { id = article.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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