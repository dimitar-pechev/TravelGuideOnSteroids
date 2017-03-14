using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Data.Contracts;
using TravelGuide.Services.Articles;
using TravelGuide.Services.Factories;
using TravelGuide.Tests.Services.Articles.Mocks;

namespace TravelGuide.Tests.Services.Articles.ArticleServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedDbContextIsNull()
        {
            // Arrange
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new ArticleService(null, factoryMock.Object, commentFactoryMock.Object));
            StringAssert.Contains("DdContext", ex.Message);
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedFactoryIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new ArticleService(contextMock.Object, null, commentFactoryMock.Object));
            StringAssert.Contains("factory", ex.Message);
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedCommentFactoryIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new ArticleService(contextMock.Object, factoryMock.Object, null));
            StringAssert.Contains("factory", ex.Message);
        }

        [Test]
        public void ReturnAnArticleServiceInstance_WhenPassedParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            // Act
            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Assert
            Assert.IsInstanceOf<ArticleService>(service);
        }

        [Test]
        public void AssignCorrectContextValue_WhenPassedParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            // Act
            var service = new ArticleServiceMock(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Assert
            Assert.AreSame(contextMock.Object, service.Context);
        }

        [Test]
        public void AssignCorrectFactoryValue_WhenPassedParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            // Act
            var service = new ArticleServiceMock(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Assert
            Assert.AreSame(factoryMock.Object, service.ArticleFactory);
        }

        [Test]
        public void AssignCorrectCommentFactoryValue_WhenPassedParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            // Act
            var service = new ArticleServiceMock(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Assert
            Assert.AreSame(commentFactoryMock.Object, service.CommentFactory);
        }
    }
}
