using TravelGuide.Services.Store;
using TravelGuide.Services.Store.Contracts;

namespace TravelGuide.Tests.Services.Store.Mocks
{
    public class CartServiceMock : CartService
    {
        public CartServiceMock(IStoreService service)
            : base(service)
        {
        }

        public IStoreService StoreService
        {
            get
            {
                return this.service;
            }
        }
    }
}
