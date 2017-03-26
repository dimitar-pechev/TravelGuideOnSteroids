using Moq;
using NUnit.Framework;
using System;
using System.Security.Principal;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;
using TravelGuide.Areas.Blog.Controllers;
using TravelGuide.Areas.Blog.ViewModels;
using TravelGuide.Common.Contracts;
using TravelGuide.Models.Stories;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Stories.Contracts;

namespace TravelGuide.Tests.Controllers.StoriesControllerTests
{
    [TestFixture]
    public class LikeStory_Should
    {
        [Test]
        public void ReturnPartialViewWithRespectiveModel_WhenParamsAreValid()
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

            storyServiceMock.Setup(x => x.ToggleLike(It.IsAny<Guid>(), It.IsAny<string>()));
            var story = new Story();
            storyServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(story);
            var model = new StoryDetailsViewModel();
            mappingServiceMock.Setup(x => x.Map<StoryDetailsViewModel>(story)).Returns(model);

            // Act & Assert
            controller.WithCallTo(x => x.LikeStory(Guid.NewGuid()))
                .ShouldRenderPartialView("_UnlikeButtonStoryPartial")
                .WithModel<StoryDetailsViewModel>(x => x == model);
        }

        [Test]
        public void CallToggleLikeMethod_WhenParamsAreValid()
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

            storyServiceMock.Setup(x => x.ToggleLike(It.IsAny<Guid>(), It.IsAny<string>()));
            var story = new Story();
            storyServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(story);
            var model = new StoryDetailsViewModel();
            mappingServiceMock.Setup(x => x.Map<StoryDetailsViewModel>(story)).Returns(model);

            // Act
            controller.LikeStory(Guid.NewGuid());

            // Assert
            storyServiceMock.Verify(x => x.ToggleLike(It.IsAny<Guid>(), It.IsAny<string>()), Times.Once);
        }
    }
}

//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult LikeStory(Guid storyId)
//{
//    var userId = this.User.Identity.GetUserId();
//    this.storyService.ToggleLike(storyId, userId);
//    var story = this.storyService.GetById(storyId);
//    var model = this.mappingService.Map<StoryDetailsViewModel>(story);

//    return this.PartialView("_UnlikeButtonStoryPartial", model);
//}