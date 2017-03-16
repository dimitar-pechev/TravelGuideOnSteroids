using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Services.Store;
using TravelGuide.Services.Store.Contracts;
using TravelGuide.Tests.Services.Store.Mocks;

namespace TravelGuide.Tests.Services.Store.CartServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedServiceIsNull()
        {
            // Arrange
            var serviceMock = new Mock<IStoreService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CartService(null));
        }

        [Test]
        public void AssignCorrectServiceValue_WhenInputParamIsValid()
        {
            // Arrange
            var serviceMock = new Mock<IStoreService>();

            // Act
            var service = new CartServiceMock(serviceMock.Object);

            // Assert
            Assert.AreSame(serviceMock.Object, service.StoreService);
        }

        [Test]
        public void ReturnCartServiceInstance_WhenInputParamIsValid()
        {
            // Arrange
            var serviceMock = new Mock<IStoreService>();

            // Act
            var service = new CartService(serviceMock.Object);

            // Assert
            Assert.IsInstanceOf<CartService>(service);
        }
    }
}
