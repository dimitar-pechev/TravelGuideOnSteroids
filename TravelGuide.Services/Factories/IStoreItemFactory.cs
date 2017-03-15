using TravelGuide.Models.Store;

namespace TravelGuide.Services.Factories
{
    public interface IStoreItemFactory
    {
        StoreItem CreateStoreItem(string itemName, string description, string destFor, string imageUrl, string brand, double price);
    }
}
