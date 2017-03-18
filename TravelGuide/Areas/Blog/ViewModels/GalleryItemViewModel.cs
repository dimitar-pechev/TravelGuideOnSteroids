using System;
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

        public int CommentsCount { get; set; }

        public int LikesCount { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<GalleryImage, GalleryItemViewModel>()
                .ForMember(dest => dest.CommentsCount, src => src.MapFrom(s => s.Comments.Count))
                .ForMember(dest => dest.LikesCount, src => src.MapFrom(s => s.Likes.Count));
        }
    }
}