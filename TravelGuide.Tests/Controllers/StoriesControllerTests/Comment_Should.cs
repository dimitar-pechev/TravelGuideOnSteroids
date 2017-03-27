using System;
using System.Security.Principal;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TravelGuide.Areas.Blog.Controllers;
using TravelGuide.Areas.Blog.ViewModels;
using TravelGuide.Common.Contracts;
using TravelGuide.Models;
using TravelGuide.Models.Stories;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Stories.Contracts;

namespace TravelGuide.Tests.Controllers.StoriesControllerTests
{
    [TestFixture]
    public class Comment_Should
    {
        [Test]
        public void ThrowInvalidOperationException_WhenModelStateIsInvalid()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var model = new StoryDetailsViewModel();
            controller.ViewData.ModelState.AddModelError("asdasda", "asdasdas");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => controller.Comment(model, Guid.NewGuid()));
        }

        [Test]
        public void ReturnPartialViewWithRespectiveViewModel_WhenParamsAreValid()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            storyServiceMock.Setup(x => x.AddComment(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()));

            var story = new Story();
            storyServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(story);

            var model = new StoryDetailsViewModel();
            mappingServiceMock.Setup(x => x.Map<StoryDetailsViewModel>(story)).Returns(model);

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            // Act & Assert
            controller.WithCallTo(x => x.Comment(model, Guid.NewGuid()))
                .ShouldRenderPartialView("_CommentBoxPartial")
               .WithModel<StoryDetailsViewModel>(x => x == model);
        }

        [Test]
        public void CallCommentMethod_WhenParamsAreValid()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            storyServiceMock.Setup(x => x.AddComment(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()));

            var story = new Story();
            storyServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(story);

            var model = new StoryDetailsViewModel();
            mappingServiceMock.Setup(x => x.Map<StoryDetailsViewModel>(story)).Returns(model);

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            // Act
            controller.Comment(model, Guid.NewGuid());

            // Assert
            storyServiceMock.Verify(x => x.AddComment(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}