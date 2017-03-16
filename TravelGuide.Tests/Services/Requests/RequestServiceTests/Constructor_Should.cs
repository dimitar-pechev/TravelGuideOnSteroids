using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Data.Contracts;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Factories;
using TravelGuide.Services.Requests;
using TravelGuide.Tests.Services.Requests.Mocks;

namespace TravelGuide.Tests.Services.Requests.RequestServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedContextIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RequestService(null, userServiceMock.Object, factoryMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedServiceIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RequestService(contextMock.Object, null, factoryMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedFactoryIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RequestService(contextMock.Object, userServiceMock.Object, null));
        }

        [Test]
        public void CorrectlyAssignContextValue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            // Act
            var service = new RequestServiceMock(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Assert
            Assert.AreSame(contextMock.Object, service.Context);
        }

        [Test]
        public void CorrectlyAssignServiceValue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            // Act
            var service = new RequestServiceMock(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Assert
            Assert.AreSame(userServiceMock.Object, service.UserService);
        }

        [Test]
        public void CorrectlyAssignFactoryValue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            // Act
            var service = new RequestServiceMock(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Assert
            Assert.AreSame(factoryMock.Object, service.Factory);
        }

        [Test]
        public void ReturnARequestServiceInstance_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            // Act
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Assert
            Assert.IsInstanceOf<RequestService>(service);
        }
    }
}
