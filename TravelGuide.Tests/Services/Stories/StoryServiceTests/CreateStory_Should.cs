using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Data.Contracts;
using TravelGuide.Models;
using TravelGuide.Models.Stories;
using TravelGuide.Services.Factories;
using TravelGuide.Services.Stories;

namespace TravelGuide.Tests.Services.Stories.StoryServiceTests
{
    [TestFixture]
    public class CreateStory_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedTitleIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var storyFactoryMock = new Mock<IStoryFactory>();
            var likesFactoryMock = new Mock<IStoryLikeFactory>();
            var commentsFactoryMock = new Mock<IStoryCommentFactory>();

            var service = new StoryService(contextMock.Object, storyFactoryMock.Object, likesFactoryMock.Object, commentsFactoryMock.Object);

            var content = "content";
            var relatedDestination = "related";
            var imageUrl = "url";
            var userId = "id";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.CreateStory(null, content, relatedDestination, imageUrl, userId));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedContentIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var storyFactoryMock = new Mock<IStoryFactory>();
            var likesFactoryMock = new Mock<IStoryLikeFactory>();
            var commentsFactoryMock = new Mock<IStoryCommentFactory>();

            var service = new StoryService(contextMock.Object, storyFactoryMock.Object, likesFactoryMock.Object, commentsFactoryMock.Object);

            var title = "title";
            var relatedDestination = "related";
            var imageUrl = "url";
            var userId = "id";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.CreateStory(title, null, relatedDestination, imageUrl, userId));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedRelatedDestIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var storyFactoryMock = new Mock<IStoryFactory>();
            var likesFactoryMock = new Mock<IStoryLikeFactory>();
            var commentsFactoryMock = new Mock<IStoryCommentFactory>();

            var service = new StoryService(contextMock.Object, storyFactoryMock.Object, likesFactoryMock.Object, commentsFactoryMock.Object);

            var title = "title";
            var content = "content";
            var imageUrl = "url";
            var userId = "id";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.CreateStory(title, content, null, imageUrl, userId));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedImageUrlIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var storyFactoryMock = new Mock<IStoryFactory>();
            var likesFactoryMock = new Mock<IStoryLikeFactory>();
            var commentsFactoryMock = new Mock<IStoryCommentFactory>();

            var service = new StoryService(contextMock.Object, storyFactoryMock.Object, likesFactoryMock.Object, commentsFactoryMock.Object);

            var title = "title";
            var content = "content";
            var relatedDestination = "related";
            var userId = "id";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.CreateStory(title, content, relatedDestination, null, userId));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedUserIdIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var storyFactoryMock = new Mock<IStoryFactory>();
            var likesFactoryMock = new Mock<IStoryLikeFactory>();
            var commentsFactoryMock = new Mock<IStoryCommentFactory>();

            var service = new StoryService(contextMock.Object, storyFactoryMock.Object, likesFactoryMock.Object, commentsFactoryMock.Object);

            var title = "title";
            var content = "content";
            var relatedDestination = "related";
            var imageUrl = "url";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.CreateStory(title, content, relatedDestination, imageUrl, null));
        }

        [Test]
        public void CallSaveChanges_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var storyFactoryMock = new Mock<IStoryFactory>();
            var likesFactoryMock = new Mock<IStoryLikeFactory>();
            var commentsFactoryMock = new Mock<IStoryCommentFactory>();

            var service = new StoryService(contextMock.Object, storyFactoryMock.Object, likesFactoryMock.Object, commentsFactoryMock.Object);

            var title = "title";
            var content = "content";
            var relatedDestination = "related";
            var imageUrl = "url";
            var userId = "id";

            var user = new User();
            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns(user);

            var story = new Story();
            storyFactoryMock.Setup(x => x.CreateStory(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<User>())).Returns(story);
            contextMock.Setup(x => x.Stories.Add(It.IsAny<Story>()));

            // Act
            service.CreateStory(title, content, relatedDestination, imageUrl, userId);

            // Assert
            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}