using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Data.Contracts;
using TravelGuide.Services.Factories;
using TravelGuide.Services.Gallery;
using TravelGuide.Tests.Services.Gallery.Mocks;

namespace TravelGuide.Tests.Services.Gallery.GalleryImageServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedContextIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new GalleryImageService(null, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object));
            StringAssert.Contains("Context", ex.Message);
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedImgFactoryIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new GalleryImageService(contextMock.Object, null, likeFactoryMock.Object, commentFactoryMock.Object));
            StringAssert.Contains("Image", ex.Message);
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedLikeFactoryIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new GalleryImageService(contextMock.Object, imageFactoryMock.Object, null, commentFactoryMock.Object));
            StringAssert.Contains("Like", ex.Message);
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedCommentFactoryIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, null));
            StringAssert.Contains("Comment", ex.Message);
        }

        [Test]
        public void ReturnImageServiceInstance_WhenPassedParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();

            // Act
            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Assert
            Assert.IsInstanceOf<GalleryImageService>(service);
        }

        [Test]
        public void CorrectlyAssignContext_WhenPassedParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();

            // Act
            var service = new GalleryImageServiceMock(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Assert
            Assert.AreSame(contextMock.Object, service.Context);
        }

        [Test]
        public void CorrectlyAssignImgFactory_WhenPassedParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();

            // Act
            var service = new GalleryImageServiceMock(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Assert
            Assert.AreSame(imageFactoryMock.Object, service.ImageFactory);
        }

        [Test]
        public void CorrectlyAssignCommentFactory_WhenPassedParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();

            // Act
            var service = new GalleryImageServiceMock(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Assert
            Assert.AreSame(commentFactoryMock.Object, service.CommentFactory);
        }

        [Test]
        public void CorrectlyAssignLikeFactory_WhenPassedParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();

            // Act
            var service = new GalleryImageServiceMock(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Assert
            Assert.AreSame(likeFactoryMock.Object, service.LikeFactory);
        }
    }
}
