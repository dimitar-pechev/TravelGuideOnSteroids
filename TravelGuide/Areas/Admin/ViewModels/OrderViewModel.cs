using System;
using AutoMapper;
using TravelGuide.Common.Contracts;
using TravelGuide.Models.Requests;

namespace TravelGuide.Areas.Admin.ViewModels
{
    public class OrderViewModel : IMapFrom<Request>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public Guid ItemId { get; set; }

        public string Username { get; set; }

        public string ItemName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Request, OrderViewModel>()
                .ForMember(dest => dest.ItemName, s => s.MapFrom(src => src.StoreItem.ItemName))
                .ForMember(dest => dest.ItemId, s => s.MapFrom(src => src.StoreItem.Id))
                .ForMember(dest => dest.Username, s => s.MapFrom(src => src.User.UserName));
        }
    }
}