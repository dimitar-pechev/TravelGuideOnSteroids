using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TravelGuide.Areas.Blog.ViewModels;
using TravelGuide.Common;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Gallery.Contacts;

namespace TravelGuide.Areas.Blog.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IGalleryImageService galleryService;
        private readonly IUserService userService;
        private readonly IMappingService mappingService;

        public GalleryController(IGalleryImageService galleryService, IMappingService mappingService, IUserService userService)
        {
            this.galleryService = galleryService;
            this.mappingService = mappingService;
            this.userService = userService;
        }

        public ActionResult Index(string query, int? page)
        {
            var pagesCount = this.galleryService.GetPagesCount(query);
            var currentPage = this.GetPage(page, pagesCount);
            var images = this.galleryService.GetFilteredImagesByPage(query, currentPage, AppConstants.GalleryPageSize);
            var mappedImages = this.mappingService.Map<IEnumerable<GalleryItemViewModel>>(images);
            var model = this.mappingService.Map<GalleryListViewModel>(mappedImages);

            model = this.AssignViewParams(model, query, currentPage, pagesCount);

            return this.View(model);
        }

        public ActionResult Search(string query, int? page)
        {
            var pagesCount = this.galleryService.GetPagesCount(query);
            var currentPage = this.GetPage(page, pagesCount);
            var images = this.galleryService.GetFilteredImagesByPage(query, currentPage, AppConstants.GalleryPageSize);
            var mappedImages = this.mappingService.Map<IEnumerable<GalleryItemViewModel>>(images);
            var model = this.mappingService.Map<GalleryListViewModel>(mappedImages);

            model = this.AssignViewParams(model, query, currentPage, pagesCount);

            return this.PartialView("_GalleryListPartial", model);
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

        [HttpPost]
        public ActionResult Comment(Guid imageId, GalleryItemViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                // TODO: Fix. Not working corrctly.
                return this.RedirectToAction("Index");
            }

            var userId = this.User.Identity.GetUserId();
            this.galleryService.AddComment(userId, model.NewCommentContent, imageId);

            var image = this.galleryService.GetGalleryImageById(imageId);
            model = this.mappingService.Map<GalleryItemViewModel>(image);

            return this.PartialView("_ImageCommentBoxPartial", model);
        }

        [HttpPost]
        public ActionResult ToggleLike(Guid? imageId)
        {
            var userId = this.User.Identity.GetUserId();
            this.galleryService.ToggleLike(userId, (Guid)imageId);

            var image = this.galleryService.GetGalleryImageById((Guid)imageId);
            var model = this.mappingService.Map<GalleryItemViewModel>(image);

            return this.PartialView("_LikeButtonPartial", model);
        }

        [HttpDelete]
        public ActionResult DeleteComment(Guid? commentId, Guid? imageId)
        {
            this.galleryService.DeleteComment(commentId.ToString());

            var image = this.galleryService.GetGalleryImageById((Guid)imageId);
            var model = this.mappingService.Map<GalleryItemViewModel>(image);

            return this.PartialView("_ImageCommentBoxPartial", model);
        }

        [HttpPost]
        public ActionResult DeleteImage(Guid? imageId, string query, int? page)
        {
            this.galleryService.DeleteImage((Guid)imageId);
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

        private GalleryListViewModel AssignViewParams(GalleryListViewModel model, string query, int currentPage, int pagesCount)
        {
            model.Query = query;
            model.CurrentPage = currentPage;
            model.PreviousPage = currentPage - 1;
            model.NextPage = currentPage + 1;
            model.PagesCount = pagesCount;

            return model;
        }
    }
}