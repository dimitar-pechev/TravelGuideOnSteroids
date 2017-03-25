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
        private readonly IUtilitiesService utils;

        public GalleryController(IGalleryImageService galleryService, IMappingService mappingService, IUserService userService, IUtilitiesService utils)
        {
            this.galleryService = galleryService;
            this.mappingService = mappingService;
            this.userService = userService;
            this.utils = utils;
        }

        [AllowAnonymous]
        public ActionResult Index(string query, int? page)
        {
            var pagesCount = this.galleryService.GetPagesCount(query);
            var currentPage = this.utils.GetPage(page, pagesCount);
            var images = this.galleryService.GetFilteredImagesByPage(query, currentPage, AppConstants.GalleryPageSize);
            var mappedImages = this.mappingService.Map<IEnumerable<GalleryItemViewModel>>(images);

            var currentUser = this.userService.GetById(this.User.Identity.GetUserId());
            if (currentUser != null)
            {
                foreach (var image in mappedImages)
                {
                    image.IsImageLiked = this.galleryService.IsImageLiked(currentUser.Id, image.Id);
                    image.CurrentUser = currentUser;
                }
            }

            var model = this.mappingService.Map<GalleryListViewModel>(mappedImages);
            model = this.utils.AssignViewParams(model, query, currentPage, pagesCount, AppConstants.GalleryListBaseUrl);

            return this.View(model);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string query, int? page)
        {
            var pagesCount = this.galleryService.GetPagesCount(query);
            var currentPage = this.utils.GetPage(page, pagesCount);
            var images = this.galleryService.GetFilteredImagesByPage(query, currentPage, AppConstants.GalleryPageSize);
            var mappedImages = this.mappingService.Map<IEnumerable<GalleryItemViewModel>>(images);
            var model = this.mappingService.Map<GalleryListViewModel>(mappedImages);

            var currentUser = this.userService.GetById(this.User.Identity.GetUserId());
            if (currentUser != null)
            {
                foreach (var image in mappedImages)
                {
                    image.IsImageLiked = this.galleryService.IsImageLiked(currentUser.Id, image.Id);
                    image.CurrentUser = currentUser;
                }
            }

            model = this.utils.AssignViewParams(model, query, currentPage, pagesCount, AppConstants.GalleryListBaseUrl);

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

            model.ProfilePicSize = AppConstants.GalleryProfilePicSize;

            var currentUserId = this.User.Identity.GetUserId();
            var user = this.userService.GetById(currentUserId);
            if (currentUserId != null)
            {
                model.IsImageLiked = this.galleryService.IsImageLiked(currentUserId, model.Id);
                model.CurrentUser = user;
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
        public ActionResult Comment(Guid itemId, GalleryItemViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException();
            }

            var userId = this.User.Identity.GetUserId();
            this.galleryService.AddComment(userId, model.NewCommentContent, itemId);

            var image = this.galleryService.GetGalleryImageById(itemId);
            model = this.mappingService.Map<GalleryItemViewModel>(image);
            var user = this.userService.GetById(userId);
            model.CurrentUser = user;
            model.ProfilePicSize = AppConstants.GalleryProfilePicSize;

            return this.PartialView("_CommentBoxPartial", model);
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
        public ActionResult DeleteComment(Guid? commentId, Guid? itemId)
        {
            if (itemId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var image = this.galleryService.GetGalleryImageById((Guid)itemId);

            if (image == null)
            {
                return this.HttpNotFound();
            }

            if (commentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            this.galleryService.DeleteComment(commentId.ToString());

            image = this.galleryService.GetGalleryImageById((Guid)itemId);
            var model = this.mappingService.Map<GalleryItemViewModel>(image);
            var userId = this.User.Identity.GetUserId();
            var user = this.userService.GetById(userId);
            model.CurrentUser = user;
            model.ProfilePicSize = AppConstants.GalleryProfilePicSize;

            return this.PartialView("_CommentBoxPartial", model);
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
    }
}