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
    public class DeleteArticle_Should
    {
        [Test]
        public void ThrowInvalidOperationException_WhenPassedArticleIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => service.DeleteArticle(null));
            StringAssert.Contains("null", ex.Message);
        }

        [Test]
        public void ThrowInvalidOperationException_WhenNoSuchArticleIsFound()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();
            var article = new Article();

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);
            contextMock.Setup(x => x.Articles.Find(It.IsAny<Guid>())).Returns((Article)null);

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => service.DeleteArticle(article));
            StringAssert.Contains("database", ex.Message);
        }

        [Test]
        public void CorrectlySetDeletedPropery_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();
            var article = new Article();

            article.IsDeleted = false;
            var initialValue = article.IsDeleted;
            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);
            contextMock.Setup(x => x.Articles.Find(It.IsAny<Guid>())).Returns(article);

            // Act
            service.DeleteArticle(article);

            // Assert
            Assert.AreEqual(initialValue, false);
            Assert.AreEqual(article.IsDeleted, true);
        }

        [Test]
        public void CallSaveChanges_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();
            var article = new Article();

            article.IsDeleted = false;
            var initialValue = article.IsDeleted;
            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);
            contextMock.Setup(x => x.Articles.Find(It.IsAny<Guid>())).Returns(article);

            // Act
            service.DeleteArticle(article);

            // Assert
            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
