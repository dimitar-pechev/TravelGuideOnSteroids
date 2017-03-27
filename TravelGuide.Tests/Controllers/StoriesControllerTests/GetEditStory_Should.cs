using System;
using Moq;
using NUnit.Framework;
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
    public class GetEditStory_Should
    {
        [Test]
        public void RedirectToIndexAction_WhenPassedIdIsNull()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Act & Assert
            controller.WithCallTo(x => x.EditStory((Guid?)null))
                .ShouldRedirectTo(x => x.Index(null, null));
        }

        [Test]
        public void ReturnHttpNotFound_WhenNoSucStoryIsFound()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Act & Assert
            controller.WithCallTo(x => x.EditStory(Guid.NewGuid()))
                .ShouldGiveHttpStatus(404);
        }

        [Test]
        public void RenderDefaultView_WhenParamsAreValid()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var story = new Story();
            storyServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(story);

            var model = new CreateEditStoryViewModel();
            mappingServiceMock.Setup(x => x.Map<CreateEditStoryViewModel>(story)).Returns(model);

            // Act & Assert
            controller.WithCallTo(x => x.EditStory(Guid.NewGuid()))
                .ShouldRenderDefaultView()
                .WithModel<CreateEditStoryViewModel>(x => x == model);
        }
    }
}