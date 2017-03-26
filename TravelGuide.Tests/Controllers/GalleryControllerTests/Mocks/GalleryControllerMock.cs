using TravelGuide.Areas.Blog.Controllers;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Gallery.Contacts;

namespace TravelGuide.Tests.Controllers.GalleryControllerTests.Mocks
{
    public class GalleryControllerMock : GalleryController
    {
        public GalleryControllerMock(IGalleryImageService galleryService, IMappingService mappingService, IUserService userService, IUtilitiesService utils)
            : base(galleryService, mappingService, userService, utils)
        {
        }

        public IGalleryImageService GalleryService
        {
            get
            {
                return this.galleryService;
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
