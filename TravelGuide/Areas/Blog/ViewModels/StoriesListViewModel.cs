using System;
using System.Collections.Generic;
using AutoMapper;
using TravelGuide.Common.Contracts;
using TravelGuide.Shared;

namespace TravelGuide.Areas.Blog.ViewModels
{
    public class StoriesListViewModel : IMapFrom<IEnumerable<StoryItemViewModel>>, IHaveCustomMappings, IPager
    {
        public IEnumerable<StoryItemViewModel> Stories { get; set; }

        public string BaseUrl { get; set; }

        public int CurrentPage { get; set; }

        public int NextPage { get; set; }

        public int PagesCount { get; set; }

        public int PreviousPage { get; set; }

        public string Query { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<IEnumerable<StoryItemViewModel>, StoriesListViewModel>()
                .ForMember(dest => dest.Stories, s => s.MapFrom(src => src));
        }
    }
}