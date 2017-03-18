﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TravelGuide.Data.Contracts;
using TravelGuide.Models.Gallery;
using TravelGuide.Services.Factories;
using TravelGuide.Services.Gallery.Contacts;

namespace TravelGuide.Services.Gallery
{
    public class GalleryImageService : IGalleryImageService
    {
        protected readonly ITravelGuideContext context;
        protected readonly IGalleryImageFactory imageFactory;
        protected readonly IGalleryLikeFactory likeFactory;
        protected readonly IGalleryCommentFactory commentFactory;

        public GalleryImageService(ITravelGuideContext context, IGalleryImageFactory imageFactory,
            IGalleryLikeFactory likeFactory, IGalleryCommentFactory commentFactory)
        {
            if (context == null)
            {
                throw new ArgumentNullException("Context cannot be null!");
            }

            if (imageFactory == null)
            {
                throw new ArgumentNullException("Image factory cannot be null!");
            }

            if (likeFactory == null)
            {
                throw new ArgumentNullException("Like factory cannot be null!");
            }

            if (commentFactory == null)
            {
                throw new ArgumentNullException("Comment factory cannot be null!");
            }

            this.context = context;
            this.imageFactory = imageFactory;
            this.likeFactory = likeFactory;
            this.commentFactory = commentFactory;
        }

        public void AddComment(string id, string content, Guid imageId)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException();
            }

            var user = this.context.Users.Find(id);

            if (user == null)
            {
                throw new InvalidOperationException();
            }
            
            var comment = this.commentFactory.CreateGalleryComment(user.Id, user, content, imageId);
            var image = this.context.GalleryImages.Find(imageId);
            image.Comments.Add(comment);
            this.context.SaveChanges();
        }

        public void ToggleLike(string id, Guid imageId)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException();
            }
            
            var like = this.likeFactory.CreateGalleryLike(id, imageId);
            var image = this.context.GalleryImages.Find(imageId);

            GalleryLike dbLike = null;
            foreach (var item in image.Likes)
            {
                if (item.UserId == id)
                {
                    dbLike = item;
                    break;
                }
            }

            if (dbLike != null)
            {
                this.context.GalleryLikes.Remove(dbLike);
            }
            else
            {
                image.Likes.Add(like);
            }

            this.context.SaveChanges();
        }

        public IEnumerable<GalleryImage> GetAllGalleryImages()
        {
            return this.context.GalleryImages.ToList();
        }

        public IEnumerable<GalleryImage> GetAllNotDeletedGalleryImagesOrderedByDate()
        {
            return this.context
                .GalleryImages
                .Include(im => im.Likes)
                .Include(im => im.Comments)
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreatedOn)
                .ToList();
        }

        public GalleryImage GetGalleryImageById(Guid id)
        {
            var image = this.context.GalleryImages.Find(id);
            return image;
        }

        public void DeleteImage(GalleryImage image)
        {
            if (image == null)
            {
                throw new ArgumentNullException();
            }

            var dbImage = this.context.GalleryImages.Find(image.Id);
            dbImage.IsDeleted = true;

            this.context.SaveChanges();
        }

        public void AddNewImage(string id, string title, string imageUrl)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(imageUrl))
            {
                throw new ArgumentNullException();
            }

            var user = this.context.Users.Find(id);

            if (user == null)
            {
                throw new InvalidOperationException();
            }
            
            var image = this.imageFactory.CreateGalleryImage(title, imageUrl, user.Id, user);

            this.context.GalleryImages.Add(image);
            this.context.SaveChanges();
        }

        public void DeleteComment(string commentId)
        {
            if (string.IsNullOrEmpty(commentId))
            {
                throw new ArgumentNullException();
            }

            var parsedId = Guid.Parse(commentId);
            var comment = this.context.GalleryComments.Find(parsedId);

            if (comment == null)
            {
                throw new InvalidOperationException();
            }

            this.context.GalleryComments.Remove(comment);

            this.context.SaveChanges();
        }
    }
}