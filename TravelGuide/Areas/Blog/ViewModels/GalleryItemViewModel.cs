using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using TravelGuide.Common.Contracts;
using TravelGuide.Models.Gallery;

namespace TravelGuide.Areas.Blog.ViewModels
{
    public class GalleryItemViewModel : IMapFrom<GalleryImage>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "Content")]
        public string NewCommentContent { get; set; }

        public int CommentsCount { get; set; }

        public int LikesCount { get; set; }

        public string UserPhoto { get; set; }

        public string Username { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<GalleryComment> Comments { get; set; }

        public IEnumerable<GalleryLike> Likes { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<GalleryImage, GalleryItemViewModel>()
                .ForMember(dest => dest.CommentsCount, src => src.MapFrom(s => s.Comments.Count))
                .ForMember(dest => dest.LikesCount, src => src.MapFrom(s => s.Likes.Count))
                .ForMember(dest => dest.Username, src => src.MapFrom(s => s.User.UserName));
        }
    }
}