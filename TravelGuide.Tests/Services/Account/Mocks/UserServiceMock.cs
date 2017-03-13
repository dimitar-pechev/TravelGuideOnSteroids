using TravelGuide.Data.Contracts;
using TravelGuide.Services;

namespace TravelGuide.Tests.Services.Account.Mocks
{
    public class UserServiceMock : UserService
    {
        public UserServiceMock(ITravelGuideContext context)
            : base(context)
        {
        }

        public ITravelGuideContext Context
        {
            get
            {
                return this.context;
            }
        }
    }
}
