using System;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TravelGuide.Areas.Blog.Controllers;
using TravelGuide.Common.Contracts;
using TravelGuide.Models.Gallery;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Gallery.Contacts;

namespace TravelGuide.Tests.Controllers.GalleryControllerTests
{
    [TestFixture]
    public class DeleteImage_Should
    {
        [Test]
        public void ReturnBadRequest_WhenNoIdIsPassed()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Act & Assert
            controller.WithCallTo(x => x.DeleteImage(null))
                .ShouldGiveHttpStatus(400);
        }

        [Test]
        public void ReturnBadRequest_WhenNoImageIsFound()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Act & Assert
            controller.WithCallTo(x => x.DeleteImage(Guid.NewGuid()))
                .ShouldGiveHttpStatus(404);
        }

        [Test]
        public void RedirectToIndexAction_WhenImageIsDeletedSuccessfully()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var image = new GalleryImage();
            galleryServiceMock.Setup(x => x.GetGalleryImageById(It.IsAny<Guid>())).Returns(image);

            // Act & Assert
            controller.WithCallTo(x => x.DeleteImage(Guid.NewGuid()))
                .ShouldRedirectTo(x => x.Index(null, null));
        }

        [Test]
        public void CallDeleteMethod_WhenImageIsDeletedSuccessfully()
        {
            // Arrange
            var galleryServiceMock = new Mock<IGalleryImageService>();
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new GalleryController(galleryServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var image = new GalleryImage();
            galleryServiceMock.Setup(x => x.GetGalleryImageById(It.IsAny<Guid>())).Returns(image);

            // Act
            controller.DeleteImage(Guid.NewGuid());

            // Assert
            galleryServiceMock.Verify(x => x.DeleteImage(It.IsAny<Guid>()), Times.Once);
        }
    }
}