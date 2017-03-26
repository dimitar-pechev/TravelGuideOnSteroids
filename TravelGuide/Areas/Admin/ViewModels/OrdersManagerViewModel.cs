using System.Collections.Generic;
using AutoMapper;
using TravelGuide.Common.Contracts;

namespace TravelGuide.Areas.Admin.ViewModels
{
    public class OrdersManagerViewModel : IPager, IHaveCustomMappings
    {
        public IEnumerable<OrderViewModel> Orders { get; set; }

        public string BaseUrl { get; set; }

        public int CurrentPage { get; set; }

        public int NextPage { get; set; }

        public int PagesCount { get; set; }

        public int PreviousPage { get; set; }

        public string Query { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<IEnumerable<OrderViewModel>, OrdersManagerViewModel>()
                .ForMember(dest => dest.Orders, s => s.MapFrom(src => src));
        }
    }
}