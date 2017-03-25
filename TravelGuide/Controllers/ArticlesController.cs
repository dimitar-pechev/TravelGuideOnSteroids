using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TravelGuide.Common;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Articles.Contracts;
using TravelGuide.Services.Store.Contracts;
using TravelGuide.ViewModels.ArticlesViewModels;

namespace TravelGuide.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        private readonly IArticleService articleService;
        private readonly IMappingService mappingService;
        private readonly IStoreService storeService;
        private readonly IUserService userService;
        private readonly IUtilitiesService utils;

        public ArticlesController(IArticleService articleService, IMappingService mappingService, IStoreService storeService, IUserService userService, IUtilitiesService utils)
        {
            this.articleService = articleService;
            this.mappingService = mappingService;
            this.storeService = storeService;
            this.userService = userService;
            this.utils = utils;
        }

        [AllowAnonymous]
        public ActionResult Index(string query, int? page)
        {
            var pagesCount = this.articleService.GetPagesCount(query);
            var currentPage = this.utils.GetPage(page, pagesCount);
            var articles = this.articleService.GetFilteredArticlesByPage(query, currentPage, AppConstants.ArticlePageSize);
            var mappedArticles = this.mappingService.Map<IEnumerable<ArticleItemViewModel>>(articles);
            var model = this.mappingService.Map<ArticlesListViewModel>(mappedArticles);
            model = this.utils.AssignViewParams(model, query, currentPage, pagesCount, AppConstants.ArticlesBaseUrl);

            return this.View(model);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string query, int? page)
        {
            var pagesCount = this.articleService.GetPagesCount(query);
            var currentPage = this.utils.GetPage(page, pagesCount);
            var articles = this.articleService.GetFilteredArticlesByPage(query, currentPage, AppConstants.ArticlePageSize);
            var mappedArticles = this.mappingService.Map<IEnumerable<ArticleItemViewModel>>(articles);
            var model = this.mappingService.Map<ArticlesListViewModel>(mappedArticles);
            model = this.utils.AssignViewParams(model, query, currentPage, pagesCount, AppConstants.ArticlesBaseUrl);

            return this.PartialView("_ArticlesListPartial", model);
        }

        public ActionResult CreateArticle()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
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

            var userId = this.User.Identity.GetUserId();
            var user = this.userService.GetById(userId);
            var article = this.articleService.GetArticleById((Guid)id);
            var storeItems = this.storeService.GetItemsByKeyword(article.City);
            var model = this.mappingService.Map<ArticleDetailsViewModel>(article);
            model.StoreItems = storeItems;
            model.CurrentUser = user;
            model.ProfilePicSize = AppConstants.ArticleProfilePicSize;

            return this.View(model);
        }

        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(ArticleDetailsViewModel model, Guid itemId)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException();
            }

            var userId = this.User.Identity.GetUserId();
            var user = this.userService.GetById(userId);
            this.articleService.AddComment(userId, model.NewCommentContent, itemId);
            var article = this.articleService.GetArticleById(itemId);
            model = this.mappingService.Map<ArticleDetailsViewModel>(article);
            model.CurrentUser = user;
            model.ProfilePicSize = AppConstants.ArticleProfilePicSize;

            return this.PartialView("_CommentBoxPartial", model);
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment(Guid itemId, string commentId)
        {
            this.articleService.DeleteComment(commentId);

            var userId = this.User.Identity.GetUserId();
            var user = this.userService.GetById(userId);

            var article = this.articleService.GetArticleById(itemId);
            var model = this.mappingService.Map<ArticleDetailsViewModel>(article);
            model.CurrentUser = user;
            model.ProfilePicSize = AppConstants.ArticleProfilePicSize;

            return this.PartialView("_CommentBoxPartial", model);
        }
    }
}