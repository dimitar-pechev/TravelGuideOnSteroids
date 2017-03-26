using System;
using System.Security.Principal;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
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
    public class Comment_Should
    {
        [Test]
        public void ThrowInvalidArgumentException_WhenModelStateIsInvalid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            controller.ViewData.ModelState.AddModelError("myError", "errriasdas");
            var model = new ArticleDetailsViewModel();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => controller.Comment(model, Guid.NewGuid()));
        }

        [Test]
        public void RenderPartialViewWithCorrectModel_WhenModelStateIsValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var model = new ArticleDetailsViewModel();
            var userModel = new User();
            var article = new Article();

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            articleServiceMock.Setup(x => x.AddComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>()));
            articleServiceMock.Setup(x => x.GetArticleById(It.IsAny<Guid>()));
            mappingServiceMock.Setup(x => x.Map<ArticleDetailsViewModel>(It.IsAny<Article>())).Returns(model);

            // Act & Assert
            controller.WithCallTo(x => x.Comment(model, Guid.NewGuid()))
                .ShouldRenderPartialView("_CommentBoxPartial")
                .WithModel<ArticleDetailsViewModel>();
        }

        [Test]
        public void CallGetUserByIdMethod_WhenModelStateIsValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var model = new ArticleDetailsViewModel();
            var userModel = new User();
            var article = new Article();

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            articleServiceMock.Setup(x => x.AddComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>()));
            articleServiceMock.Setup(x => x.GetArticleById(It.IsAny<Guid>()));
            mappingServiceMock.Setup(x => x.Map<ArticleDetailsViewModel>(It.IsAny<Article>())).Returns(model);

            // Act
            controller.Comment(model, Guid.NewGuid());

            // Assert
            userServiceMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CallAddCommentMethod_WhenModelStateIsValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var model = new ArticleDetailsViewModel();
            var userModel = new User();
            var article = new Article();

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            articleServiceMock.Setup(x => x.AddComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>()));
            articleServiceMock.Setup(x => x.GetArticleById(It.IsAny<Guid>()));
            mappingServiceMock.Setup(x => x.Map<ArticleDetailsViewModel>(It.IsAny<Article>())).Returns(model);

            // Act
            controller.Comment(model, Guid.NewGuid());

            // Assert
            articleServiceMock.Verify(x => x.AddComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void CallGetArticleByIdMethod_WhenModelStateIsValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var model = new ArticleDetailsViewModel();
            var userModel = new User();
            var article = new Article();

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            articleServiceMock.Setup(x => x.AddComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>()));
            articleServiceMock.Setup(x => x.GetArticleById(It.IsAny<Guid>()));
            mappingServiceMock.Setup(x => x.Map<ArticleDetailsViewModel>(It.IsAny<Article>())).Returns(model);

            // Act
            controller.Comment(model, Guid.NewGuid());

            // Assert
            articleServiceMock.Verify(x => x.GetArticleById(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void CallMappingService_WhenModelStateIsValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var model = new ArticleDetailsViewModel();
            var userModel = new User();
            var article = new Article();

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            articleServiceMock.Setup(x => x.AddComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>()));
            articleServiceMock.Setup(x => x.GetArticleById(It.IsAny<Guid>()));
            mappingServiceMock.Setup(x => x.Map<ArticleDetailsViewModel>(It.IsAny<Article>())).Returns(model);

            // Act
            controller.Comment(model, Guid.NewGuid());

            // Assert
            mappingServiceMock.Verify(x => x.Map<ArticleDetailsViewModel>(It.IsAny<Article>()), Times.Once);
        }
    }
}