using TravelGuide.Common.Contracts;
using TravelGuide.Models.Store;

namespace TravelGuide.Areas.Store.ViewModels
{
    public class IndexViewModel : IMapFrom<StoreItem>
    {
        public string ItemName { get; set; }

        public string ImageUrl { get; set; }

        public string Brand { get; set; }

        public double Price { get; set; }

        public bool InStock { get; set; }
    }
}