using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Data.Contracts;
using TravelGuide.Models.Gallery;
using TravelGuide.Services.Factories;
using TravelGuide.Services.Gallery;

namespace TravelGuide.Tests.Services.Gallery.GalleryImageServiceTests
{
    [TestFixture]
    public class GetGalleryImageById_Should
    {
        [Test]
        public void CorrectlyReturnTargetInstance_WhenParamsAreCorrect()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();
            var image = new GalleryImage();

            contextMock.Setup(x => x.GalleryImages.Find(It.IsAny<Guid>())).Returns(image);
            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act
            var dbImage = service.GetGalleryImageById(Guid.NewGuid());

            // Assert
            Assert.AreSame(image, dbImage);
        }
    }
}
