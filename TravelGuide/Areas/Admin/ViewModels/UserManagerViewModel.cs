using System.Collections.Generic;
using AutoMapper;
using TravelGuide.Common.Contracts;
using TravelGuide.Shared;

namespace TravelGuide.Areas.Admin.ViewModels
{
    public class UserManagerViewModel : IPager, IMapFrom<IEnumerable<UserViewModel>>, IHaveCustomMappings
    {
        public IEnumerable<UserViewModel> Users { get; set; }

        public string BaseUrl { get; set; }

        public int CurrentPage { get; set; }

        public int NextPage { get; set; }

        public int PagesCount { get; set; }

        public int PreviousPage { get; set; }

        public string Query { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<IEnumerable<UserViewModel>, UserManagerViewModel>()
              .ForMember(dest => dest.Users, src => src.MapFrom(s => s));
        }
    }
}