﻿using System;
using System.Collections.Generic;
using TravelGuide.Models.Gallery;

namespace TravelGuide.Services.Gallery.Contacts
{
    public interface IGalleryImageService
    {
        Tuple<Guid, Guid> GetSurroundingImageIds(GalleryImage image);

        IEnumerable<GalleryImage> GetFilteredImagesByPage(string query, int page, int pageSize);

        IEnumerable<GalleryImage> GetAllGalleryImages();

        IEnumerable<GalleryImage> GetAllNotDeletedGalleryImagesOrderedByDate();

        GalleryImage GetGalleryImageById(Guid id);

        void ToggleLike(string id, Guid imageId);

        void AddComment(string id, string content, Guid imageId);

        void DeleteImage(Guid? imageId);

        void AddNewImage(string id, string title, string imageUrl);

        void DeleteComment(string commentId);

        int GetPagesCount(string query);

        bool IsImageLiked(string userId, Guid imageId);
    }
}
