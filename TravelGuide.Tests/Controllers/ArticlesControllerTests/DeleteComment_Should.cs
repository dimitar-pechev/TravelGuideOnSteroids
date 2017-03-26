using Moq;
using NUnit.Framework;
using System;
using System.Security.Principal;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;
using TravelGuide.Common.Contracts;
using TravelGuide.Controllers;
using TravelGuide.Models;
using TravelGuide.Models.Articles;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Articles.Contracts;
using TravelGuide.Services.Store.Contracts;
using TravelGuide.ViewModels.ArticlesViewModels;

namespace TravelGuide.Tests.Controllers.ArticlesControllerTests
{
    [TestFixture]
    public class DeleteComment_Should
    {
        [Test]
        public void ReturnPartialViewWithCorrectModel_WhenInputParamsAreValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            var article = new Article();
            var model = new ArticleDetailsViewModel();

            articleServiceMock.Setup(x => x.DeleteComment(It.IsAny<string>()));
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            articleServiceMock.Setup(x => x.GetArticleById(It.IsAny<Guid>())).Returns(article);
            mappingServiceMock.Setup(x => x.Map<ArticleDetailsViewModel>(article)).Returns(model);

            // Act & Assert
            controller.WithCallTo(x => x.DeleteComment(Guid.NewGuid(), "id"))
                .ShouldRenderPartialView("_CommentBoxPartial")
                .WithModel<ArticleDetailsViewModel>(x => x == model);
        }

        [Test]
        public void CallDeleteCommentMethod_WhenInputParamsAreValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            var article = new Article();
            var model = new ArticleDetailsViewModel();

            articleServiceMock.Setup(x => x.DeleteComment(It.IsAny<string>()));
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            articleServiceMock.Setup(x => x.GetArticleById(It.IsAny<Guid>())).Returns(article);
            mappingServiceMock.Setup(x => x.Map<ArticleDetailsViewModel>(article)).Returns(model);

            // Act
            controller.DeleteComment(Guid.NewGuid(), "some id");

            // Assert
            articleServiceMock.Verify(x => x.DeleteComment("some id"), Times.Once);
        }

        [Test]
        public void CallGetUserByIdMethod_WhenInputParamsAreValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            var article = new Article();
            var model = new ArticleDetailsViewModel();

            articleServiceMock.Setup(x => x.DeleteComment(It.IsAny<string>()));
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            articleServiceMock.Setup(x => x.GetArticleById(It.IsAny<Guid>())).Returns(article);
            mappingServiceMock.Setup(x => x.Map<ArticleDetailsViewModel>(article)).Returns(model);

            // Act
            controller.DeleteComment(Guid.NewGuid(), "some id");

            // Assert
            userServiceMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CallGetArticleByIdMethod_WhenInputParamsAreValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            var article = new Article();
            var model = new ArticleDetailsViewModel();

            articleServiceMock.Setup(x => x.DeleteComment(It.IsAny<string>()));
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            articleServiceMock.Setup(x => x.GetArticleById(It.IsAny<Guid>())).Returns(article);
            mappingServiceMock.Setup(x => x.Map<ArticleDetailsViewModel>(article)).Returns(model);

            // Act
            controller.DeleteComment(Guid.NewGuid(), "some id");

            // Assert
            articleServiceMock.Verify(x => x.GetArticleById(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void CallMappingServiceMethod_WhenInputParamsAreValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            var article = new Article();
            var model = new ArticleDetailsViewModel();

            articleServiceMock.Setup(x => x.DeleteComment(It.IsAny<string>()));
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            articleServiceMock.Setup(x => x.GetArticleById(It.IsAny<Guid>())).Returns(article);
            mappingServiceMock.Setup(x => x.Map<ArticleDetailsViewModel>(article)).Returns(model);

            // Act
            controller.DeleteComment(Guid.NewGuid(), "some id");

            // Assert
            mappingServiceMock.Verify(x => x.Map<ArticleDetailsViewModel>(article), Times.Once);
        }
    }
}