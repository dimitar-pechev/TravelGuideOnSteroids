using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TravelGuide.Areas.Blog.ViewModels;
using TravelGuide.Common;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Stories.Contracts;

namespace TravelGuide.Areas.Blog.Controllers
{
    public class StoriesController : Controller
    {
        private readonly IStoryService storyService;
        private readonly IMappingService mappingService;
        private readonly IUserService userService;

        public StoriesController(IStoryService storyService, IMappingService mappingService, IUserService userService)
        {
            this.storyService = storyService;
            this.mappingService = mappingService;
            this.userService = userService;
        }

        public ActionResult Index(string query, int? page)
        {
            var pagesCount = this.storyService.GetPagesCount(query);
            var currentPage = this.GetPage(page, pagesCount);
            var stories = this.storyService.GetStoriesByPage(query, currentPage, AppConstants.StoriesPageSize);
            var mappedStories = this.mappingService.Map<IEnumerable<StoryItemViewModel>>(stories);
            var model = this.mappingService.Map<StoriesListViewModel>(mappedStories);
            model = this.AssignViewParams(model, query, currentPage, pagesCount, AppConstants.StorisBaseUrl);

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string query, int? page)
        {
            var pagesCount = this.storyService.GetPagesCount(query);
            var currentPage = this.GetPage(page, pagesCount);
            var stories = this.storyService.GetStoriesByPage(query, currentPage, AppConstants.StoriesPageSize);
            var mappedStories = this.mappingService.Map<IEnumerable<StoryItemViewModel>>(stories);
            var model = this.mappingService.Map<StoriesListViewModel>(mappedStories);
            model = this.AssignViewParams(model, query, currentPage, pagesCount, AppConstants.StorisBaseUrl);

            return this.PartialView("_StoriesListPartial", model);
        }

        [HttpGet]
        public ActionResult CreateStory()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult CreateStory(CreateEditStoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var userId = this.User.Identity.GetUserId();

            this.storyService.CreateStory(model.Title, model.Content, model.RelatedDestination, model.ImageUrl, userId);

            return this.RedirectToAction("Index");
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Index");
            }

            var story = this.storyService.GetById((Guid)id);

            if (story == null)
            {
                return this.HttpNotFound();
            }

            var model = this.mappingService.Map<StoryDetailsViewModel>(story);

            var userId = this.User.Identity.GetUserId();
            var currentUser = this.userService.GetById(userId);
            if (currentUser != null)
            {
                model.CurrentUser = currentUser;
                model.IsStoryLiked = this.storyService.IsStoryLiked(story.Id, currentUser.Id);
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LikeStory(Guid storyId)
        {
            var userId = this.User.Identity.GetUserId();
            this.storyService.ToggleLike(storyId, userId);
            var story = this.storyService.GetById(storyId);
            var model = this.mappingService.Map<StoryDetailsViewModel>(story);

            return this.PartialView("_UnlikeButtonStoryPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnlikeStory(Guid storyId)
        {
            var userId = this.User.Identity.GetUserId();
            this.storyService.ToggleLike(storyId, userId);
            var story = this.storyService.GetById(storyId);
            var model = this.mappingService.Map<StoryDetailsViewModel>(story);

            return this.PartialView("_LikeButtonStoryPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(StoryDetailsViewModel model, Guid? storyId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.PartialView("_StoryCommentBoxPartial", model);
            }

            var userId = this.User.Identity.GetUserId();

            this.storyService.AddComment((Guid)storyId, userId, model.NewCommentContent);

            var story = this.storyService.GetById((Guid)storyId);
            model = this.mappingService.Map<StoryDetailsViewModel>(story);

            var user = this.userService.GetById(userId);
            model.CurrentUser = user;

            return this.PartialView("_StoryCommentBoxPartial", model);
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment(Guid commentId, Guid storyId)
        {
            this.storyService.DeleteComment(commentId);
            var story = this.storyService.GetById(storyId);
            var model = this.mappingService.Map<StoryDetailsViewModel>(story);

            var userId = this.User.Identity.GetUserId();
            var user = this.userService.GetById(userId);

            model.CurrentUser = user;

            return this.PartialView("_StoryCommentBoxPartial", model);
        }

        public ActionResult EditStory(Guid? storyId)
        {
            if (storyId == null)
            {
                this.RedirectToAction("Index");
            }

            var story = this.storyService.GetById((Guid)storyId);

            if (story == null)
            {
                return this.HttpNotFound();
            }

            var model = this.mappingService.Map<CreateEditStoryViewModel>(story);

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStory(CreateEditStoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.storyService.EditStory(model.Id, model.Title, model.Content, model.RelatedDestination, model.ImageUrl);

            var story = this.storyService.GetById(model.Id);
            var viewModel = this.mappingService.Map<StoryDetailsViewModel>(story);
            var userId = this.User.Identity.GetUserId();
            var currentUser = this.userService.GetById(userId);
            if (currentUser != null)
            {
                viewModel.CurrentUser = currentUser;
                viewModel.IsStoryLiked = this.storyService.IsStoryLiked(story.Id, currentUser.Id);
            }

            return this.RedirectToAction("Details", viewModel);
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

        private StoriesListViewModel AssignViewParams(StoriesListViewModel model, string query, int currentPage, int pagesCount, string baseUrl)
        {
            model.Query = query;
            model.CurrentPage = currentPage;
            model.PreviousPage = currentPage - 1;
            model.NextPage = currentPage + 1;
            model.PagesCount = pagesCount;
            model.BaseUrl = baseUrl;

            return model;
        }
    }
}