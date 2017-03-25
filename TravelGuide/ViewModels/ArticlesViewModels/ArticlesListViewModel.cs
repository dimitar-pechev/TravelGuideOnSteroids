using System.Collections.Generic;
using AutoMapper;
using TravelGuide.Common.Contracts;

namespace TravelGuide.ViewModels.ArticlesViewModels
{
    public class ArticlesListViewModel : IPager, IMapFrom<IEnumerable<ArticleItemViewModel>>, IHaveCustomMappings
    {
        public IEnumerable<ArticleItemViewModel> Articles { get; set; }

        public string BaseUrl { get; set; }

        public int CurrentPage { get; set; }

        public int NextPage { get; set; }

        public int PagesCount { get; set; }

        public int PreviousPage { get; set; }

        public string Query { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<IEnumerable<ArticleItemViewModel>, ArticlesListViewModel>()
                .ForMember(dest => dest.Articles, s => s.MapFrom(src => src));
        }
    }
}