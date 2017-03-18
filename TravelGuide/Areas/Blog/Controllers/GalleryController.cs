using System.Collections.Generic;
using System.Web.Mvc;
using TravelGuide.Areas.Blog.ViewModels;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Gallery.Contacts;

namespace TravelGuide.Areas.Blog.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IGalleryImageService galleryService;
        private readonly IMappingService mappingService;

        public GalleryController(IGalleryImageService galleryService, IMappingService mappingService)
        {
            this.galleryService = galleryService;
            this.mappingService = mappingService;
        }

        public ActionResult Index()
        {
            var images = this.galleryService.GetAllNotDeletedGalleryImagesOrderedByDate();
            var model = this.mappingService.Map<IEnumerable<GalleryListViewModel>>(images);

            return this.View(model);
        }
    }
}