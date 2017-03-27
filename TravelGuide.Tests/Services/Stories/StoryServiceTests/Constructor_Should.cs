using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Data.Contracts;
using TravelGuide.Services.Factories;
using TravelGuide.Services.Stories;

namespace TravelGuide.Tests.Services.Stories.StoryServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedContextIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var storyFactoryMock = new Mock<IStoryFactory>();
            var likesFactoryMock = new Mock<IStoryLikeFactory>();
            var commentsFactoryMock = new Mock<IStoryCommentFactory>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StoryService(null, storyFactoryMock.Object, likesFactoryMock.Object, commentsFactoryMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedStoryFactoryIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var storyFactoryMock = new Mock<IStoryFactory>();
            var likesFactoryMock = new Mock<IStoryLikeFactory>();
            var commentsFactoryMock = new Mock<IStoryCommentFactory>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StoryService(contextMock.Object, null, likesFactoryMock.Object, commentsFactoryMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedLikesFactoryIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var storyFactoryMock = new Mock<IStoryFactory>();
            var likesFactoryMock = new Mock<IStoryLikeFactory>();
            var commentsFactoryMock = new Mock<IStoryCommentFactory>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StoryService(contextMock.Object, storyFactoryMock.Object, null, commentsFactoryMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedCommentsFactoryIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var storyFactoryMock = new Mock<IStoryFactory>();
            var likesFactoryMock = new Mock<IStoryLikeFactory>();
            var commentsFactoryMock = new Mock<IStoryCommentFactory>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StoryService(contextMock.Object, storyFactoryMock.Object, likesFactoryMock.Object, null));
        }

        [Test]
        public void ReturnCorrectInstance_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var storyFactoryMock = new Mock<IStoryFactory>();
            var likesFactoryMock = new Mock<IStoryLikeFactory>();
            var commentsFactoryMock = new Mock<IStoryCommentFactory>();

            // Act
            var service = new StoryService(contextMock.Object, storyFactoryMock.Object, likesFactoryMock.Object, commentsFactoryMock.Object);

            // Assert
            Assert.IsInstanceOf<StoryService>(service);
        }
    }
}
