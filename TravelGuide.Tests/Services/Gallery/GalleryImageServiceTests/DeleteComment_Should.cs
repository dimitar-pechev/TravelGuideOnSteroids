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
    public class DeleteComment_Should
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

            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.DeleteComment(id));
        }

        [Test]
        public void ThrowInvalidOperationException_WhenPassedIdIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();
            var id = Guid.NewGuid().ToString();

            contextMock.Setup(x => x.GalleryComments.Find(It.IsAny<Guid>())).Returns((GalleryComment)null);
            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.DeleteComment(id));
        }

        [Test]
        public void CallRemoveMethod_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();
            var id = Guid.NewGuid().ToString();
            var comment = new GalleryComment();

            contextMock.Setup(x => x.GalleryComments.Find(It.IsAny<Guid>())).Returns(comment);
            contextMock.Setup(x => x.GalleryComments.Remove(comment));
            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act
            service.DeleteComment(id);

            // Assert
            contextMock.Verify(x => x.GalleryComments.Remove(comment), Times.Once);
        }

        [Test]
        public void CallSaveChanges_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();
            var id = Guid.NewGuid().ToString();
            var comment = new GalleryComment();

            contextMock.Setup(x => x.GalleryComments.Find(It.IsAny<Guid>())).Returns(comment);
            contextMock.Setup(x => x.GalleryComments.Remove(comment));
            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act
            service.DeleteComment(id);

            // Assert
            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
