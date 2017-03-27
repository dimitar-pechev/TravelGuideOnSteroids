using TravelGuide.Areas.Store.Controllers;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Store.Contracts;

namespace TravelGuide.Tests.Controllers.StoreControllerTests.Mocks
{
    public class StoreControllerMock : StoreItemsController
    {
        public StoreControllerMock(IStoreService storeService, IMappingService mappingService, IUtilitiesService utils)
            : base(storeService, mappingService, utils)
        {
        }

        public IStoreService StoreService
        {
            get
            {
                return this.storeService;
            }
        }

        public IMappingService MappingService
        {
            get
            {
                return this.mappingService;
            }
        }

        public IUtilitiesService Utils
        {
            get
            {
                return this.utils;
            }
        }
    }
}
