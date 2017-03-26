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
using TravelGuide.ViewModels.ArticlesViewModels;

namespace TravelGuide.Tests.Controllers.ArticlesControllerTests
{
    [TestFixture]
    public class GetEditArticle_Should
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
            controller.WithCallTo(x => x.EditArticle((Guid?)null))
                .ShouldRedirectTo(x => x.Index(null, null));
        }

        [Test]
        public void RenderDefaultView_WhenPassedIdIsNull()
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
            var model = new CreateEditArticleViewModel();

            articleServiceMock.Setup(x => x.GetArticleById(id)).Returns(article);
            mappingServiceMock.Setup(x => x.Map<CreateEditArticleViewModel>(article)).Returns(model);

            // Act & Assert
            controller.WithCallTo(x => x.EditArticle(id))
                .ShouldRenderDefaultView()
                .WithModel<CreateEditArticleViewModel>(x => x == model);
        }

        [Test]
        public void CallGetArticleByIdMethod_WhenPassedIdIsNull()
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
            var model = new CreateEditArticleViewModel();

            articleServiceMock.Setup(x => x.GetArticleById(id)).Returns(article);
            mappingServiceMock.Setup(x => x.Map<CreateEditArticleViewModel>(article)).Returns(model);

            // Act
            controller.EditArticle(id);

            // Assert
            articleServiceMock.Verify(x => x.GetArticleById(id), Times.Once);
        }

        [Test]
        public void CallMappingServiceMethod_WhenPassedIdIsNull()
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
            var model = new CreateEditArticleViewModel();

            articleServiceMock.Setup(x => x.GetArticleById(id)).Returns(article);
            mappingServiceMock.Setup(x => x.Map<CreateEditArticleViewModel>(article)).Returns(model);

            // Act
            controller.EditArticle(id);

            // Assert
            mappingServiceMock.Verify(x => x.Map<CreateEditArticleViewModel>(article), Times.Once);
        }
    }
}
