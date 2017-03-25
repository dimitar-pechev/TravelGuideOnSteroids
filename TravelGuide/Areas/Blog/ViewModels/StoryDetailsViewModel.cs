using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using TravelGuide.Common.Contracts;
using TravelGuide.Models;
using TravelGuide.Models.Abstractions.Contracts;
using TravelGuide.Models.Stories;

namespace TravelGuide.Areas.Blog.ViewModels
{
    public class StoryDetailsViewModel : IMapFrom<Story>, IHaveCustomMappings, ICommentable
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string RelatedDestination { get; set; }

        public string ImageUrl { get; set; }

        public virtual User Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public User CurrentUser { get; set; }

        public bool IsStoryLiked { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3)]
        [Display(Name = "Content")]
        public string NewCommentContent { get; set; }

        public int ProfilePicSize { get; set; }

        public virtual IEnumerable<IComment> Comments { get; set; }

        public virtual IEnumerable<StoryLike> Likes { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Story, StoryDetailsViewModel>()
                .ForMember(dest => dest.Author, s => s.MapFrom(src => src.User));
        }
    }
}