using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Data.Contracts;
using TravelGuide.Models.Articles;
using TravelGuide.Services.Articles;
using TravelGuide.Services.Factories;

namespace TravelGuide.Tests.Services.Articles.ArticleServiceTests
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
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.DeleteComment(id));
        }

        [Test]
        public void ThrowInvalidOperationException_WhenNoSuchCommentIsFound()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            contextMock.Setup(x => x.Comments.Find(It.IsAny<Guid>())).Returns((ArticleComment)null);
            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.DeleteComment(Guid.NewGuid().ToString()));
        }

        [Test]
        public void CallRemoveMethod_WhenPassedValidParams()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();
            var comment = new ArticleComment();

            contextMock.Setup(x => x.Comments.Find(It.IsAny<Guid>())).Returns(comment);
            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act
            service.DeleteComment(Guid.NewGuid().ToString());

            // Assert
            contextMock.Verify(x => x.Comments.Remove(comment), Times.Once);
        }
    }
}
