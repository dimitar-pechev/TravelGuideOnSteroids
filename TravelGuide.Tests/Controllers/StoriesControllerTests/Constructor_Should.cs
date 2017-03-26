using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Areas.Blog.Controllers;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Stories.Contracts;
using TravelGuide.Tests.Controllers.StoriesControllerTests.Mocks;

namespace TravelGuide.Tests.Controllers.StoriesControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedStoryServiceIsNull()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StoriesController(null, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedMappingServiceIsNull()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StoriesController(storyServiceMock.Object, null, userServiceMock.Object, utilsMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedUserServiceIsNull()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, null, utilsMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedUtilsIsNull()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, null));
        }

        [Test]
        public void ReturnCorrectInstance_WhenParamsAreValid()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act
            var controller = new StoriesController(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.IsInstanceOf<StoriesController>(controller);
        }

        [Test]
        public void AssignStoriesServiceCorrectly_WhenParamsAreValid()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act
            var controller = new StoriesControllerMock(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.AreSame(storyServiceMock.Object, controller.StoryService);
        }

        [Test]
        public void AssignMappingServiceCorrectly_WhenParamsAreValid()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act
            var controller = new StoriesControllerMock(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.AreSame(mappingServiceMock.Object, controller.MappingService);
        }

        [Test]
        public void AssignUserServiceCorrectly_WhenParamsAreValid()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act
            var controller = new StoriesControllerMock(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.AreSame(userServiceMock.Object, controller.UserService);
        }

        [Test]
        public void AssignUtilsServiceCorrectly_WhenParamsAreValid()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act
            var controller = new StoriesControllerMock(storyServiceMock.Object, mappingServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.AreSame(utilsMock.Object, controller.Utils);
        }
    }
}
