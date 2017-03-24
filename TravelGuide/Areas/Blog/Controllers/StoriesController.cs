using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TravelGuide.Areas.Blog.ViewModels;
using TravelGuide.Common;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Stories.Contracts;

namespace TravelGuide.Areas.Blog.Controllers
{
    public class StoriesController : Controller
    {
        private readonly IStoryService storyService;
        private readonly IMappingService mappingService;

        public StoriesController(IStoryService storyService, IMappingService mappingService)
        {
            this.storyService = storyService;
            this.mappingService = mappingService;
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