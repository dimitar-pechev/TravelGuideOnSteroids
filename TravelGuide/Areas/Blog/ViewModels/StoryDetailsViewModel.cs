using System;
using System.Collections.Generic;
using AutoMapper;
using TravelGuide.Common.Contracts;
using TravelGuide.Models;
using TravelGuide.Models.Stories;

namespace TravelGuide.Areas.Blog.ViewModels
{
    public class StoryDetailsViewModel : IMapFrom<Story>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string RelatedDestination { get; set; }

        public string ImageUrl { get; set; }

        public virtual User Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<StoryComment> Comments { get; set; }

        public virtual ICollection<StoryLike> Likes { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Story, StoryDetailsViewModel>()
                .ForMember(dest => dest.Author, s => s.MapFrom(src => src.User));
        }
    }
}