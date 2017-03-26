using System;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TravelGuide.Common.Contracts;
using TravelGuide.Controllers;
using TravelGuide.Models.Articles;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Articles.Contracts;
using TravelGuide.Services.Store.Contracts;

namespace TravelGuide.Tests.Controllers.ArticlesControllerTests
{
    [TestFixture]
    public class DeleteArticle_Should
    {
        [Test]
        public void RedirectToIndexAction_WhenPassedIdIsNull()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Act & Assert
            controller.WithCallTo(x => x.DeleteArticle(null))
                .ShouldRedirectTo(x => x.Index(null, null));
        }

        [Test]
        public void RedirectToIndexAction_WhenParamsAreValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var id = Guid.NewGuid();
            var article = new Article();

            articleServiceMock.Setup(x => x.GetArticleById(id)).Returns(article);
            articleServiceMock.Setup(x => x.DeleteArticle(article));

            // Act & Assert
            controller.WithCallTo(x => x.DeleteArticle(id))
                .ShouldRedirectTo(x => x.Index(null, null));
        }

        [Test]
        public void CallGetArticleById_WhenParamsAreValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var id = Guid.NewGuid();
            var article = new Article();

            articleServiceMock.Setup(x => x.GetArticleById(id)).Returns(article);
            articleServiceMock.Setup(x => x.DeleteArticle(article));

            // Act
            controller.DeleteArticle(id);

            // Assert
            articleServiceMock.Verify(x => x.GetArticleById(id), Times.Once);
        }

        [Test]
        public void CallDeleteArticleMethod_WhenParamsAreValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var id = Guid.NewGuid();
            var article = new Article();

            articleServiceMock.Setup(x => x.GetArticleById(id)).Returns(article);
            articleServiceMock.Setup(x => x.DeleteArticle(article));

            // Act
            controller.DeleteArticle(id);

            // Assert
            articleServiceMock.Verify(x => x.DeleteArticle(article), Times.Once);
        }
    }
}
