using TravelGuide.Areas.Blog.Controllers;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Stories.Contracts;

namespace TravelGuide.Tests.Controllers.StoriesControllerTests.Mocks
{
    public class StoriesControllerMock : StoriesController
    {
        public StoriesControllerMock(IStoryService storyService, IMappingService mappingService, IUserService userService, IUtilitiesService utils)
            : base(storyService, mappingService, userService, utils)
        {
        }

        public IStoryService StoryService
        {
            get
            {
                return this.storyService;
            }
        }

        public IMappingService MappingService
        {
            get
            {
                return this.mappingService;
            }
        }

        public IUserService UserService
        {
            get
            {
                return this.userService;
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
