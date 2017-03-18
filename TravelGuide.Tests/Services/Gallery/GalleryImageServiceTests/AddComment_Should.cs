using System;
using System.Linq;
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
    public class AddComment_Should
    {
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ThrowArgumentNullException_WhenPassedIdIsNull(string id)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();

            var content = "some content";
            var guid = Guid.NewGuid();

            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.AddComment(id, content, guid));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ThrowArgumentNullException_WhenPassedContentIsNull(string content)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();

            var id = "some name";
            var guid = Guid.NewGuid();

            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.AddComment(id, content, guid));
        }

        [Test]
        public void ThrowInvalidOperationException_WhenNoSuchUser()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();

            var id = "some name";
            var content = "some content";
            var guid = Guid.NewGuid();

            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns((User)null);

            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.AddComment(id, content, guid));
        }

        [Test]
        public void AddCommentToRespectiveImage_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();
            var comment = new GalleryComment();
            var image = new GalleryImage();
            var user = new User();

            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns(user);
            commentFactoryMock.Setup(x => x.CreateGalleryComment(It.IsAny<string>(), It.IsAny<User>(), It.IsAny<string>(), It.IsAny<Guid>()))
                .Returns(comment);
            contextMock.Setup(x => x.GalleryImages.Find(It.IsAny<Guid>())).Returns(image);

            var id = "some name";
            var content = "some content";
            var guid = Guid.NewGuid();

            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act
            service.AddComment(id, content, guid);

            // Assert
            Assert.AreSame(comment, image.Comments.First());
        }

        [Test]
        public void MakeACallToSaveChanges_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();
            var comment = new GalleryComment();
            var image = new GalleryImage();
            var user = new User();

            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns(user);
            commentFactoryMock.Setup(x => x.CreateGalleryComment(It.IsAny<string>(), It.IsAny<User>(), It.IsAny<string>(), It.IsAny<Guid>()))
                .Returns(comment);
            contextMock.Setup(x => x.GalleryImages.Find(It.IsAny<Guid>())).Returns(image);

            var id = "some name";
            var content = "some content";
            var guid = Guid.NewGuid();

            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act
            service.AddComment(id, content, guid);

            // Assert
            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
