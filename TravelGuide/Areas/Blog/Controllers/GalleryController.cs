using Microsoft.AspNet.Identity;
using System;
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

        public ActionResult Edit(Guid? id)
        {
            var model = this.galleryService.GetGalleryImageById((Guid)id);

            return this.View(model);
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Create(CreateImageViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var userId = this.User.Identity.GetUserId();
            this.galleryService.AddNewImage(userId, model.Title, model.ImageUrl);

            return this.RedirectToAction("Index");
        }
    }
}