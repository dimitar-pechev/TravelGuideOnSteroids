using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using TravelGuide.Areas.Blog.ViewModels;
using TravelGuide.Services.Stories.Contracts;

namespace TravelGuide.Areas.Blog.Controllers
{
    public class StoriesController : Controller
    {
        private readonly IStoryService storyService;

        public StoriesController(IStoryService storyService)
        {
            this.storyService = storyService;
        }

        public ActionResult Index()
        {
            return this.View();
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
    }
}