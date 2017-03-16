using TravelGuide.Data.Contracts;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Factories;
using TravelGuide.Services.Requests;

namespace TravelGuide.Tests.Services.Requests.Mocks
{
    public class RequestServiceMock : RequestService
    {
        public RequestServiceMock(ITravelGuideContext context, IUserService userService, IRequestFactory factory)
            : base(context, userService, factory)
        {
        }

        public ITravelGuideContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IUserService UserService
        {
            get
            {
                return this.userService;
            }
        }

        public IRequestFactory Factory
        {
            get
            {
                return this.factory;
            }
        }
    }
}
