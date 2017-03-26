using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Areas.Blog.Controllers;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Gallery.Contacts;
using TravelGuide.Tests.Controllers.GalleryControllerTests.Mocks;

namespace TravelGuide.Tests.Controllers.GalleryControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedGalleryServiceIsNull()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new GalleryController(null, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedMappingServiceIsNull()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new GalleryController(galleryServiceMock.Object, null, userServiceMock.Object, utilsMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedUserServiceIsNull()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, null, utilsMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedUtilitiesServiceIsNull()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, null));
        }

        [Test]
        public void ReturnCorrectControllerInstance_WhenPassedParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act
            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.IsInstanceOf<GalleryController>(controller);
        }

        [Test]
        public void CorrectlyAssignGalleryService_WhenPassedParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act
            var controller = new GalleryControllerMock(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.AreSame(galleryServiceMock.Object, controller.GalleryService);
        }

        [Test]
        public void CorrectlyAssignUserService_WhenPassedParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act
            var controller = new GalleryControllerMock(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.AreSame(mappingServiceMock.Object, controller.MappingService);
        }

        [Test]
        public void CorrectlyAssignMappingService_WhenPassedParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act
            var controller = new GalleryControllerMock(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.AreSame(userServiceMock.Object, controller.UserService);
        }

        [Test]
        public void CorrectlyAssignUtilsService_WhenPassedParamsAreValid()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act
            var controller = new GalleryControllerMock(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.AreSame(utilsMock.Object, controller.Utils);
        }
    }
}