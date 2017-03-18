using System.Collections.Generic;
using AutoMapper;
using TravelGuide.Common.Contracts;

namespace TravelGuide.Areas.Blog.ViewModels
{
    public class GalleryListViewModel : IMapFrom<IEnumerable<GalleryItemViewModel>>, IHaveCustomMappings
    {
        public IEnumerable<GalleryItemViewModel> Images { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage { get; set; }

        public int NextPage { get; set; }

        public string Query { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<IEnumerable<GalleryItemViewModel>, GalleryListViewModel>()
                .ForMember(dest => dest.Images, src => src.MapFrom(s => s));
        }
    }
}