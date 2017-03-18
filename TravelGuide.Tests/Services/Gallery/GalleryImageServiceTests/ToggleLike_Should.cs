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
    public class ToggleLike_Should
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

            var imageId = Guid.NewGuid();

            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.ToggleLike(id, imageId));
        }

        [Test]
        public void CorrectlyAddLikeToImage_WhenPassedParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();

            var image = new GalleryImage();
            var like = new GalleryLike();
            var user = new User();

            like.UserId = Guid.NewGuid().ToString();

            user.Id = Guid.NewGuid().ToString();
            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns(user);
            likeFactoryMock.Setup(x => x.CreateGalleryLike(It.IsAny<string>(), It.IsAny<Guid>())).Returns(like);
            contextMock.Setup(x => x.GalleryImages.Find(It.IsAny<Guid>())).Returns(image);

            var id = "some id";
            var imageId = Guid.NewGuid();

            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act
            service.ToggleLike(id, imageId);

            // Assert
            Assert.AreSame(like, image.Likes.First());
        }

        [Test]
        public void CorrectlyRemoveLikeFromImage_WhenPassedParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();

            var image = new GalleryImage();
            var like = new GalleryLike();
            var user = new User();

            image.Likes.Add(like);
            var initialCount = image.Likes.Count();

            var imageId = Guid.NewGuid();

            like.UserId = Guid.Parse(user.Id).ToString();

            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns(user);
            likeFactoryMock.Setup(x => x.CreateGalleryLike(It.IsAny<string>(), It.IsAny<Guid>())).Returns(like);
            contextMock.Setup(x => x.GalleryImages.Find(It.IsAny<Guid>())).Returns(image);
            contextMock.Setup(x => x.GalleryLikes.Remove(like));

            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act
            service.ToggleLike(like.UserId, imageId);

            // Assert
            contextMock.Verify(x => x.GalleryLikes.Remove(like), Times.Once);
        }

        [Test]
        public void CallOnSaveChanges_WhenPassedParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var imageFactoryMock = new Mock<IGalleryImageFactory>();
            var commentFactoryMock = new Mock<IGalleryCommentFactory>();
            var likeFactoryMock = new Mock<IGalleryLikeFactory>();

            var image = new GalleryImage();
            var like = new GalleryLike();
            var user = new User();

            image.Likes.Add(like);
            var initialCount = image.Likes.Count();

            var id = "id";
            var imageId = Guid.NewGuid();

            like.UserId = Guid.Parse(user.Id).ToString();

            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns(user);
            likeFactoryMock.Setup(x => x.CreateGalleryLike(It.IsAny<string>(), It.IsAny<Guid>())).Returns(like);
            contextMock.Setup(x => x.GalleryImages.Find(It.IsAny<Guid>())).Returns(image);
            contextMock.Setup(x => x.GalleryLikes.Remove(like));

            var service = new GalleryImageService(contextMock.Object, imageFactoryMock.Object, likeFactoryMock.Object, commentFactoryMock.Object);

            // Act
            service.ToggleLike(id, imageId);

            // Assert
            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
