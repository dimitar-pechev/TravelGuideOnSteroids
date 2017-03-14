using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using TravelGuide.Data.Contracts;
using TravelGuide.Models;
using TravelGuide.Models.Articles;
using TravelGuide.Services.Articles;
using TravelGuide.Services.Factories;

namespace TravelGuide.Tests.Services.Articles.ArticleServiceTests
{
    [TestFixture]
    public class AddComment_Should
    {
        public void ThrowInvalidOperationException_WhenUserIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            contextMock.Setup(x => x.Users.Find(It.IsAny<Guid>())).Returns((User)null);

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.AddComment("id", "content", Guid.NewGuid()));
        }

        [Test]
        public void AddCommentToArticleInstance_WhenInputIsValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();
            var article = new Article();
            var comment = new ArticleComment();
            var user = new User();

            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns(user);
            commentFactoryMock.Setup(x => x.CreateArticleComment(It.IsAny<string>(), It.IsAny<User>(), It.IsAny<string>(), It.IsAny<Guid>())).Returns(comment);
            contextMock.Setup(x => x.Articles.Find(It.IsAny<Guid>())).Returns(article);

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act
            service.AddComment("id", "content", Guid.NewGuid());

            // Assert
            Assert.AreSame(article.Comments.First(), comment);
        }

        [Test]
        public void CallSaveChanges_WhenInputIsValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();
            var article = new Article();
            var comment = new ArticleComment();
            var user = new User();

            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns(user);
            commentFactoryMock.Setup(x => x.CreateArticleComment(It.IsAny<string>(), It.IsAny<User>(), It.IsAny<string>(), It.IsAny<Guid>())).Returns(comment);
            contextMock.Setup(x => x.Articles.Find(It.IsAny<Guid>())).Returns(article);

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act
            service.AddComment("id", "content", Guid.NewGuid());

            // Assert
            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
