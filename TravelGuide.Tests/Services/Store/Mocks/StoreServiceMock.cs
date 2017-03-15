using TravelGuide.Data.Contracts;
using TravelGuide.Services.Factories;
using TravelGuide.Services.Store;

namespace TravelGuide.Tests.Services.Store.Mocks
{
    public class StoreServiceMock : StoreService
    {
        public StoreServiceMock(ITravelGuideContext context, IStoreItemFactory factory)
            : base(context, factory)
        {
        }

        public ITravelGuideContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IStoreItemFactory Factory
        {
            get
            {
                return this.factory;
            }
        }
    }
}
