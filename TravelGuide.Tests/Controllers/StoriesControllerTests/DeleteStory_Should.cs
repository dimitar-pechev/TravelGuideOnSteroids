using Moq;
using NUnit.Framework;
using System;
using TestStack.FluentMVCTesting;
using TravelGuide.Areas.Blog.Controllers;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Stories.Contracts;

namespace TravelGuide.Tests.Controllers.StoriesControllerTests
{
    [TestFixture]
    public class DeleteStory_Should
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
            controller.WithCallTo(x => x.DeleteStory(null))
                .ShouldRedirectTo(x => x.Index(null, null));
        }

        [Test]
        public void RedirectToIndexAction_WhenPassedIdIsNotNull()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Act & Assert
            controller.WithCallTo(x => x.DeleteStory(Guid.NewGuid()))
                .ShouldRedirectTo(x => x.Index(null, null));
        }

        [Test]
        public void CallDeleteStoryMethod_WhenParamsAreValid()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            storyServiceMock.Setup(x => x.DeleteStory(It.IsAny<Guid>()));

            // Act
            controller.DeleteStory(Guid.NewGuid());

            // Assert
            storyServiceMock.Verify(x => x.DeleteStory(It.IsAny<Guid>()), Times.Once);
        }
    }
}