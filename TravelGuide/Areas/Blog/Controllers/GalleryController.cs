using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TravelGuide.Areas.Blog.ViewModels;
using TravelGuide.Common;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Gallery.Contacts;

namespace TravelGuide.Areas.Blog.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        public ActionResult Index(string query, int? page)
        {
            var pagesCount = this.galleryService.GetPagesCount(query);
            var currentPage = this.GetPage(page, pagesCount);
            var images = this.galleryService.GetFilteredImagesByPage(query, currentPage, AppConstants.GalleryPageSize);
            var mappedImages = this.mappingService.Map<IEnumerable<GalleryItemViewModel>>(images);

            var currentUser = this.userService.GetById(this.User.Identity.GetUserId());
            if (currentUser != null)
            {
                foreach (var image in mappedImages)
                {
                    image.IsImageLiked = this.galleryService.IsImageLiked(currentUser.Id, image.Id);
                    image.CurrentUserId = currentUser.Id;
                }
            }

            var model = this.mappingService.Map<GalleryListViewModel>(mappedImages);

            model = this.AssignViewParams(model, query, currentPage, pagesCount, AppConstants.GalleryListBaseUrl);

            return this.View(model);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string query, int? page)
        {
            var pagesCount = this.galleryService.GetPagesCount(query);
            var currentPage = this.GetPage(page, pagesCount);
            var images = this.galleryService.GetFilteredImagesByPage(query, currentPage, AppConstants.GalleryPageSize);
            var mappedImages = this.mappingService.Map<IEnumerable<GalleryItemViewModel>>(images);
            var model = this.mappingService.Map<GalleryListViewModel>(mappedImages);

            var currentUser = this.userService.GetById(this.User.Identity.GetUserId());
            if (currentUser != null)
            {
                foreach (var image in mappedImages)
                {
                    image.IsImageLiked = this.galleryService.IsImageLiked(currentUser.Id, image.Id);
                    image.CurrentUserId = currentUser.Id;
                }
            }

            model = this.AssignViewParams(model, query, currentPage, pagesCount, AppConstants.GalleryListBaseUrl);

            return this.PartialView("_GalleryListPartial", model);
        }

        [AllowAnonymous]
        public ActionResult PopulateModal(Guid? imageId)
        {
            if (imageId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var image = this.galleryService.GetGalleryImageById((Guid)imageId);

            if (image == null)
            {
                return this.HttpNotFound();
            }

            var model = this.mappingService.Map<GalleryItemViewModel>(image);

            var surroundingImageIds = this.galleryService.GetSurroundingImageIds(image);
            model.PreviousImageId = surroundingImageIds.Item1;
            model.NextImageId = surroundingImageIds.Item2;

            var currentUserId = this.User.Identity.GetUserId();
            if (currentUserId != null)
            {
                model.IsImageLiked = this.galleryService.IsImageLiked(currentUserId, model.Id);
                model.CurrentUserId = currentUserId;
            }

            return this.PartialView("_ImageDetailsPartial", model);
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        public ActionResult Comment(Guid imageId, GalleryItemViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.PartialView("_ImageCommentBoxPartial", model);
            }

            var userId = this.User.Identity.GetUserId();
            this.galleryService.AddComment(userId, model.NewCommentContent, imageId);

            var image = this.galleryService.GetGalleryImageById(imageId);
            model = this.mappingService.Map<GalleryItemViewModel>(image);
            model.CurrentUserId = this.User.Identity.GetUserId();

            return this.PartialView("_ImageCommentBoxPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LikeImage(Guid? imageId)
        {
            if (imageId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var image = this.galleryService.GetGalleryImageById((Guid)imageId);

            if (image == null)
            {
                return this.HttpNotFound();
            }

            var userId = this.User.Identity.GetUserId();

            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            this.galleryService.ToggleLike(userId, (Guid)imageId);

            image = this.galleryService.GetGalleryImageById((Guid)imageId);
            var model = this.mappingService.Map<GalleryItemViewModel>(image);

            return this.PartialView("_UnlikeButtonPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnlikeImage(Guid? imageId)
        {
            if (imageId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var image = this.galleryService.GetGalleryImageById((Guid)imageId);

            if (image == null)
            {
                return this.HttpNotFound();
            }

            var userId = this.User.Identity.GetUserId();

            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            this.galleryService.ToggleLike(userId, (Guid)imageId);

            image = this.galleryService.GetGalleryImageById((Guid)imageId);
            var model = this.mappingService.Map<GalleryItemViewModel>(image);

            return this.PartialView("_LikeButtonPartial", model);
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment(Guid? commentId, Guid? imageId)
        {
            if (imageId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var image = this.galleryService.GetGalleryImageById((Guid)imageId);

            if (image == null)
            {
                return this.HttpNotFound();
            }

            if (commentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            this.galleryService.DeleteComment(commentId.ToString());

            image = this.galleryService.GetGalleryImageById((Guid)imageId);
            var model = this.mappingService.Map<GalleryItemViewModel>(image);
            model.CurrentUserId = this.User.Identity.GetUserId();

            return this.PartialView("_ImageCommentBoxPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteImage(Guid? imageId)
        {
            if (imageId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var image = this.galleryService.GetGalleryImageById((Guid)imageId);

            if (image == null)
            {
                return this.HttpNotFound();
            }

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

        private GalleryListViewModel AssignViewParams(GalleryListViewModel model, string query, int currentPage, int pagesCount, string baseUrl)
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