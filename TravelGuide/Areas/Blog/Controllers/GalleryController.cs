﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TravelGuide.Areas.Blog.ViewModels;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Gallery.Contacts;

namespace TravelGuide.Areas.Blog.Controllers
{
    public class GalleryController : Controller
    {
        private const int PageSize = 12;
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
            var images = this.galleryService.GetAllNotDeletedGalleryImagesOrderedByDate();

            if (!string.IsNullOrEmpty(query))
            {
                images = images.Where(x => x.Title.ToLower().Contains(query.ToLower())).ToList();
            }

            var modelledImages = this.mappingService.Map<IEnumerable<GalleryItemViewModel>>(images);
            var model = this.mappingService.Map<GalleryListViewModel>(modelledImages);
            model.PagesCount = (int)Math.Ceiling((decimal)model.Images.Count() / PageSize);

            if (page == null || page < 1 || page > model.PagesCount)
            {
                page = 1;
            }

            model.Images = model.Images.Skip(PageSize * ((int)page - 1)).Take(PageSize).ToList();

            model.CurrentPage = (int)page;
            model.PreviousPage = (int)page - 1;
            model.NextPage = (int)page + 1;
            model.Query = query;

            return this.View(model);
        }

        public ActionResult Search(string query, int? page)
        {
            var images = this.galleryService.GetAllNotDeletedGalleryImagesOrderedByDate();

            if (!string.IsNullOrEmpty(query))
            {
                images = images.Where(x => x.Title.ToLower().Contains(query.ToLower())).ToList();
            }

            var modelledImages = this.mappingService.Map<IEnumerable<GalleryItemViewModel>>(images);
            var model = this.mappingService.Map<GalleryListViewModel>(modelledImages);
            model.PagesCount = (int)Math.Ceiling((decimal)model.Images.Count() / PageSize);

            if (page == null || page < 1 || page > model.PagesCount)
            {
                page = 1;
            }

            model.Images = model.Images.Skip(PageSize * ((int)page - 1)).Take(PageSize).ToList();

            model.CurrentPage = (int)page;
            model.PreviousPage = (int)page - 1;
            model.NextPage = (int)page + 1;
            model.Query = query;

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

            var images = this.galleryService.GetAllNotDeletedGalleryImagesOrderedByDate();

            if (!string.IsNullOrEmpty(query))
            {
                images = images.Where(x => x.Title.ToLower().Contains(query.ToLower())).ToList();
            }

            var modelledImages = this.mappingService.Map<IEnumerable<GalleryItemViewModel>>(images);
            var model = this.mappingService.Map<GalleryListViewModel>(modelledImages);
            model.PagesCount = (int)Math.Ceiling((decimal)model.Images.Count() / PageSize);

            if (page == null || page < 1 || page > model.PagesCount)
            {
                page = 1;
            }

            model.Images = model.Images.Skip(PageSize * ((int)page - 1)).Take(PageSize).ToList();

            model.CurrentPage = (int)page;
            model.PreviousPage = (int)page - 1;
            model.NextPage = (int)page + 1;
            model.Query = query;

            return this.RedirectToAction("Index");
        }
    }
}