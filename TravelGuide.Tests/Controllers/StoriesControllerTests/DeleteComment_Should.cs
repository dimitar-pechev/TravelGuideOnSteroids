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
    public class DeleteComment_Should
    {
        [Test]
        public void RetundPartialViewWithRespectiveModel_WhenParamsAreValid()
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

            storyServiceMock.Setup(x => x.DeleteComment(It.IsAny<Guid>()));

            var story = new Story();
            storyServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(story);

            var model = new StoryDetailsViewModel();
            mappingServiceMock.Setup(x => x.Map<StoryDetailsViewModel>(story)).Returns(model);

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            // Act & Assert
            controller.WithCallTo(x => x.DeleteComment(Guid.NewGuid(), Guid.NewGuid()))
                .ShouldRenderPartialView("_CommentBoxPartial")
                .WithModel<StoryDetailsViewModel>();
        }

        [Test]
        public void CallDeleteCommentMethod_WhenParamsAreValid()
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

            storyServiceMock.Setup(x => x.DeleteComment(It.IsAny<Guid>()));

            var story = new Story();
            storyServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(story);

            var model = new StoryDetailsViewModel();
            mappingServiceMock.Setup(x => x.Map<StoryDetailsViewModel>(story)).Returns(model);

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            // Act
            controller.DeleteComment(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            storyServiceMock.Verify(x => x.DeleteComment(It.IsAny<Guid>()), Times.Once);
        }
    }
}