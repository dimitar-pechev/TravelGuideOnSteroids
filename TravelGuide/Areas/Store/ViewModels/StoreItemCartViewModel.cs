using System;
using TravelGuide.Common.Contracts;
using TravelGuide.Models.Store;

namespace TravelGuide.Areas.Store.ViewModels
{
    public class StoreItemCartViewModel : IMapFrom<StoreItem>
    {
        public Guid Id { get; set; }

        public string ItemName { get; set; }

        public string Brand { get; set; }

        public string DestinationFor { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }
    }
}