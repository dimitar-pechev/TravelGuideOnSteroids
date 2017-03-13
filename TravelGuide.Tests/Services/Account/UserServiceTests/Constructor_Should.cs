using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Data.Contracts;
using TravelGuide.Services;
using TravelGuide.Tests.Services.Account.Mocks;

namespace TravelGuide.Tests.Services.Account.UserServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void Throw_WhenPassedContextIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UserService(null));
        }

        [Test]
        public void ReturnAnInstanceOfUserService_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();

            // Act
            var service = new UserService(contextMock.Object);

            // Assert
            Assert.IsInstanceOf<UserService>(service);
        }

        [Test]
        public void CorrectlyAssignContextValue_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();

            // Act
            var service = new UserServiceMock(contextMock.Object);

            // Assert
            Assert.AreSame(contextMock.Object, service.Context);
        }
    }
}
