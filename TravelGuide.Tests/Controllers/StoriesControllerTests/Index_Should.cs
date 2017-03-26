using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Mvc;
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
    public class Index_Should
    {
        [Test]
        public void ReturnDefaultViewWithCorrectModel_WhenParamsAreValid()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            storyServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);

            var stories = new List<Story>();
            storyServiceMock.Setup(x => x.GetStoriesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(stories);

            var mappedStories = new List<StoryItemViewModel>();
            mappingServiceMock.Setup(x => x.Map<IEnumerable<StoryItemViewModel>>(stories)).Returns(mappedStories);

            var model = new StoriesListViewModel();
            mappingServiceMock.Setup(x => x.Map<StoriesListViewModel>(mappedStories)).Returns(model);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            utilsMock.Setup(x => x.AssignViewParams(model, It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(model);

            // Act & Assert
            controller.WithCallTo(x => x.Index(null, null))
                .ShouldRenderDefaultView()
                .WithModel<StoriesListViewModel>(x => x == model);
        }

        [Test]
        public void CallGetPagesCount_WhenParamsAreValid()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            storyServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);

            var stories = new List<Story>();
            storyServiceMock.Setup(x => x.GetStoriesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(stories);

            var mappedStories = new List<StoryItemViewModel>();
            mappingServiceMock.Setup(x => x.Map<IEnumerable<StoryItemViewModel>>(stories)).Returns(mappedStories);

            var model = new StoriesListViewModel();
            mappingServiceMock.Setup(x => x.Map<StoriesListViewModel>(mappedStories)).Returns(model);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            utilsMock.Setup(x => x.AssignViewParams(model, It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(model);

            // Act
            controller.Index(null, null);

            // Assert
            storyServiceMock.Verify(x => x.GetPagesCount(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CallGetPage_WhenParamsAreValid()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            storyServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);

            var stories = new List<Story>();
            storyServiceMock.Setup(x => x.GetStoriesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(stories);

            var mappedStories = new List<StoryItemViewModel>();
            mappingServiceMock.Setup(x => x.Map<IEnumerable<StoryItemViewModel>>(stories)).Returns(mappedStories);

            var model = new StoriesListViewModel();
            mappingServiceMock.Setup(x => x.Map<StoriesListViewModel>(mappedStories)).Returns(model);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            utilsMock.Setup(x => x.AssignViewParams(model, It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(model);

            // Act
            controller.Index(null, null);

            // Assert
            utilsMock.Verify(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void CallGetStoriesByPage_WhenParamsAreValid()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            storyServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);

            var stories = new List<Story>();
            storyServiceMock.Setup(x => x.GetStoriesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(stories);

            var mappedStories = new List<StoryItemViewModel>();
            mappingServiceMock.Setup(x => x.Map<IEnumerable<StoryItemViewModel>>(stories)).Returns(mappedStories);

            var model = new StoriesListViewModel();
            mappingServiceMock.Setup(x => x.Map<StoriesListViewModel>(mappedStories)).Returns(model);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            utilsMock.Setup(x => x.AssignViewParams(model, It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(model);

            // Act
            controller.Index(null, null);

            // Assert
            storyServiceMock.Verify(x => x.GetStoriesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void CallMapToStoryItem_WhenParamsAreValid()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            storyServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);

            var stories = new List<Story>();
            storyServiceMock.Setup(x => x.GetStoriesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(stories);

            var mappedStories = new List<StoryItemViewModel>();
            mappingServiceMock.Setup(x => x.Map<IEnumerable<StoryItemViewModel>>(stories)).Returns(mappedStories);

            var model = new StoriesListViewModel();
            mappingServiceMock.Setup(x => x.Map<StoriesListViewModel>(mappedStories)).Returns(model);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            utilsMock.Setup(x => x.AssignViewParams(model, It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(model);

            // Act
            controller.Index(null, null);

            // Assert
            mappingServiceMock.Verify(x => x.Map<IEnumerable<StoryItemViewModel>>(stories), Times.Once);
        }

        [Test]
        public void CallMapToStoriesList_WhenParamsAreValid()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            storyServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);

            var stories = new List<Story>();
            storyServiceMock.Setup(x => x.GetStoriesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(stories);

            var mappedStories = new List<StoryItemViewModel>();
            mappingServiceMock.Setup(x => x.Map<IEnumerable<StoryItemViewModel>>(stories)).Returns(mappedStories);

            var model = new StoriesListViewModel();
            mappingServiceMock.Setup(x => x.Map<StoriesListViewModel>(mappedStories)).Returns(model);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            utilsMock.Setup(x => x.AssignViewParams(model, It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(model);

            // Act
            controller.Index(null, null);

            // Assert
            mappingServiceMock.Verify(x => x.Map<StoriesListViewModel>(mappedStories), Times.Once);
        }

        [Test]
        public void CallAssignValues_WhenParamsAreValid()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            storyServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);

            var stories = new List<Story>();
            storyServiceMock.Setup(x => x.GetStoriesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(stories);

            var mappedStories = new List<StoryItemViewModel>();
            mappingServiceMock.Setup(x => x.Map<IEnumerable<StoryItemViewModel>>(stories)).Returns(mappedStories);

            var model = new StoriesListViewModel();
            mappingServiceMock.Setup(x => x.Map<StoriesListViewModel>(mappedStories)).Returns(model);

            var controllerContext = new Mock<ControllerContext>();
            var user = new Mock<IPrincipal>();
            user.Setup(p => p.IsInRole("admin")).Returns(true);
            user.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(user.Object);
            controller.ControllerContext = controllerContext.Object;

            var userModel = new User();
            userServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userModel);
            utilsMock.Setup(x => x.AssignViewParams(model, It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(model);

            // Act
            controller.Index(null, null);

            // Assert
            utilsMock.Verify(x => x.AssignViewParams(model, It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }
    }
}