using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TravelGuide.Common.Contracts;
using TravelGuide.Controllers;
using TravelGuide.Models;
using TravelGuide.Models.Articles;
using TravelGuide.Models.Store;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Articles.Contracts;
using TravelGuide.Services.Store.Contracts;
using TravelGuide.ViewModels.ArticlesViewModels;

namespace TravelGuide.Tests.Controllers.ArticlesControllerTests.Mocks
{
    [TestFixture]
    public class Details_Should
    {
        [Test]
        public void RedirectBackToIndex_WhenPassedIdIsNull()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Act & Assert
            controller.WithCallTo(x => x.Details(null))
                .ShouldRedirectTo(x => x.Index(null, null));
        }

        [Test]
        public void RenderDefaultViewWithCorrectViewModek_WhenPassedIdIsValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContextMock.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContextMock.Object;

            var id = Guid.NewGuid();
            var userModel = new User();
            var article = new Article();
            var storeItems = new List<StoreItem>();
            var model = new ArticleDetailsViewModel();

            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            articleServiceMock.Setup(x => x.GetArticleById(It.IsAny<Guid>())).Returns(article);
            storeServiceMock.Setup(x => x.GetItemsByKeyword(It.IsAny<string>())).Returns(storeItems);
            mappingServiceMock.Setup(x => x.Map<ArticleDetailsViewModel>(It.IsAny<Article>())).Returns(model);

            // Act & Assert
            controller.WithCallTo(x => x.Details(id))
                .ShouldRenderDefaultView()
                .WithModel<ArticleDetailsViewModel>(x => x == model);
        }

        [Test]
        public void CallUserService_WhenPassedIdIsValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContextMock.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContextMock.Object;

            var id = Guid.NewGuid();
            var userModel = new User();
            var article = new Article();
            var storeItems = new List<StoreItem>();
            var model = new ArticleDetailsViewModel();

            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            articleServiceMock.Setup(x => x.GetArticleById(It.IsAny<Guid>())).Returns(article);
            storeServiceMock.Setup(x => x.GetItemsByKeyword(It.IsAny<string>())).Returns(storeItems);
            mappingServiceMock.Setup(x => x.Map<ArticleDetailsViewModel>(It.IsAny<Article>())).Returns(model);

            // Act
            controller.Details(id);

            // Assert
            userServiceMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CallGetArticleByID_WhenPassedIdIsValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContextMock.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContextMock.Object;

            var id = Guid.NewGuid();
            var userModel = new User();
            var article = new Article();
            var storeItems = new List<StoreItem>();
            var model = new ArticleDetailsViewModel();

            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            articleServiceMock.Setup(x => x.GetArticleById(It.IsAny<Guid>())).Returns(article);
            storeServiceMock.Setup(x => x.GetItemsByKeyword(It.IsAny<string>())).Returns(storeItems);
            mappingServiceMock.Setup(x => x.Map<ArticleDetailsViewModel>(It.IsAny<Article>())).Returns(model);

            // Act
            controller.Details(id);

            // Assert
            articleServiceMock.Verify(x => x.GetArticleById(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void CallGetItemsByKeyword_WhenPassedIdIsValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContextMock.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContextMock.Object;

            var id = Guid.NewGuid();
            var userModel = new User();
            var article = new Article();
            var storeItems = new List<StoreItem>();
            var model = new ArticleDetailsViewModel();

            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            articleServiceMock.Setup(x => x.GetArticleById(It.IsAny<Guid>())).Returns(article);
            storeServiceMock.Setup(x => x.GetItemsByKeyword(It.IsAny<string>())).Returns(storeItems);
            mappingServiceMock.Setup(x => x.Map<ArticleDetailsViewModel>(It.IsAny<Article>())).Returns(model);

            // Act
            controller.Details(id);

            // Assert
            storeServiceMock.Verify(x => x.GetItemsByKeyword(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CallMappingService_WhenPassedIdIsValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContextMock.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContextMock.Object;

            var id = Guid.NewGuid();
            var userModel = new User();
            var article = new Article();
            var storeItems = new List<StoreItem>();
            var model = new ArticleDetailsViewModel();

            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            articleServiceMock.Setup(x => x.GetArticleById(It.IsAny<Guid>())).Returns(article);
            storeServiceMock.Setup(x => x.GetItemsByKeyword(It.IsAny<string>())).Returns(storeItems);
            mappingServiceMock.Setup(x => x.Map<ArticleDetailsViewModel>(It.IsAny<Article>())).Returns(model);

            // Act
            controller.Details(id);

            // Assert
            mappingServiceMock.Verify(x => x.Map<ArticleDetailsViewModel>(It.IsAny<Article>()), Times.Once);
        }
    }
}