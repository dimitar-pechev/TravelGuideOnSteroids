using System.Collections.Generic;
using AutoMapper;
using TravelGuide.Common.Contracts;
using TravelGuide.Shared;

namespace TravelGuide.Areas.Store.ViewModels
{
    public class StoreListViewModel : IMapFrom<IEnumerable<StoreItemViewModel>>, IHaveCustomMappings, IPager
    {
        public IEnumerable<StoreItemViewModel> Items { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage { get; set; }

        public int NextPage { get; set; }

        public string Query { get; set; }

        public string BaseUrl { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<IEnumerable<StoreItemViewModel>, StoreListViewModel>()
                .ForMember(dest => dest.Items, src => src.MapFrom(s => s));
        }
    }
}