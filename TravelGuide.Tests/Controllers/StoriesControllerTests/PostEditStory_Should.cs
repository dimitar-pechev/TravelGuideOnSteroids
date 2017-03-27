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
    public class PostEditStory_Should
    {
        [Test]
        public void ReturnDefaultViewWithRespectiveViewModel_WhenModelStateIsInvalid()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            controller.ViewData.ModelState.AddModelError("asdasd", "asdas");

            var model = new CreateEditStoryViewModel();

            // Act & Assert
            controller.WithCallTo(x => x.EditStory(model))
                .ShouldRenderDefaultView()
                .WithModel<CreateEditStoryViewModel>(x => x == model);
        }

        [Test]
        public void RenderDetailsView_OnSuccessfulEdit()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var story = new Story();
            storyServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(story);

            var viewModel = new StoryDetailsViewModel();
            mappingServiceMock.Setup(x => x.Map<StoryDetailsViewModel>(story)).Returns(viewModel);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            storyServiceMock.Setup(x => x.IsStoryLiked(It.IsAny<Guid>(), It.IsAny<string>())).Returns(true);

            var model = new CreateEditStoryViewModel();

            // Act & Assert
            controller.WithCallTo(x => x.EditStory(model))
                .ShouldRenderView("Details")
                .WithModel<StoryDetailsViewModel>(x => x == viewModel);
        }

        [Test]
        public void CallEditStoryMethod_WhenParamsAreValid()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var story = new Story();
            storyServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(story);

            storyServiceMock.Setup(x => x.EditStory(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            var viewModel = new StoryDetailsViewModel();
            mappingServiceMock.Setup(x => x.Map<StoryDetailsViewModel>(story)).Returns(viewModel);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);

            storyServiceMock.Setup(x => x.IsStoryLiked(It.IsAny<Guid>(), It.IsAny<string>())).Returns(true);

            var model = new CreateEditStoryViewModel();

            // Act
            controller.EditStory(model);

            // Assert
            storyServiceMock.Verify(x => x.EditStory(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}