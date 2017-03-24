using System;
using AutoMapper;
using TravelGuide.Common.Contracts;
using TravelGuide.Models.Stories;

namespace TravelGuide.Areas.Blog.ViewModels
{
    public class StoryItemViewModel : IMapFrom<Story>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string RelatedDestination { get; set; }

        public int CommentsCount { get; set; }

        public int LikesCount { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Story, StoryItemViewModel>()
                .ForMember(dest => dest.LikesCount, s => s.MapFrom(src => src.Likes.Count))
                .ForMember(dest => dest.CommentsCount, s => s.MapFrom(src => src.Comments.Count))
                .ForMember(dest => dest.Author, s => s.MapFrom(src => src.User.UserName));
        }
    }
}