using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Data.Contracts;
using TravelGuide.Models;
using TravelGuide.Models.Gallery;
using TravelGuide.Services.Factories;
using TravelGuide.Services.Gallery;

namespace TravelGuide.Tests.Services.Gallery.GalleryImageServiceTests
{
    [TestFixture]
    public class AddNewImage_Should
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ThrowArgumentNullException_WhenPassedIdIsNull(string id)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();

            var title = "some title";
            var url = "some url";

            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.AddNewImage(id, title, url));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ThrowArgumentNullException_WhenPassedTitleIsNull(string title)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();

            var id = "some title";
            var url = "some url";

            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.AddNewImage(id, title, url));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ThrowArgumentNullException_WhenPassedUrlIsNull(string url)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();

            var title = "some title";
            var id = "some url";

            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.AddNewImage(id, title, url));
        }

        [Test]
        public void ThrowInvalidArgumentException_WhenNoSuchUserIsFound()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();

            var title = "some title";
            var id = "some url";
            var url = "url";

            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns((User)null);
            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.AddNewImage(id, title, url));
        }

        [Test]
        public void CallImageFactory_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();
            var user = new User();

            var title = "some title";
            var id = "some url";
            var url = "url";

            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns(user);
            contextMock.Setup(x => x.GalleryImages.Add(It.IsAny<GalleryImage>()));
            imageFactoryMock.Setup(x => x.CreateGalleryImage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<User>()));
            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act
            service.AddNewImage(id, title, url);

            // Assert
            imageFactoryMock.Verify(x => x.CreateGalleryImage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void CallAddMethod_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();
            var user = new User();

            var title = "some title";
            var id = "some url";
            var url = "url";

            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns(user);
            contextMock.Setup(x => x.GalleryImages.Add(It.IsAny<GalleryImage>()));
            imageFactoryMock.Setup(x => x.CreateGalleryImage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<User>()));
            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act
            service.AddNewImage(id, title, url);

            // Assert
            contextMock.Verify(x => x.GalleryImages.Add(It.IsAny<GalleryImage>()), Times.Once);
        }

        [Test]
        public void CallSaveChanges_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();
            var user = new User();

            var title = "some title";
            var id = "some url";
            var url = "url";

            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns(user);
            contextMock.Setup(x => x.GalleryImages.Add(It.IsAny<GalleryImage>()));
            imageFactoryMock.Setup(x => x.CreateGalleryImage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<User>()));
            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act
            service.AddNewImage(id, title, url);

            // Assert
            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
