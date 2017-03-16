using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Data.Contracts;
using TravelGuide.Services.Factories;
using TravelGuide.Services.Store;
using TravelGuide.Tests.Services.Store.Mocks;

namespace TravelGuide.Tests.Services.Store.StoreServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedContextIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StoreService(null, factoryMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedFactoryIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StoreService(contextMock.Object, null));
        }

        [Test]
        public void ReturnInstanceOfStoreService_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();

            // Act
            var service = new StoreService(contextMock.Object, factoryMock.Object);

            // Assert
            Assert.IsInstanceOf<StoreService>(service);
        }

        [Test]
        public void AssignContextValueProperly_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();

            // Act
            var service = new StoreServiceMock(contextMock.Object, factoryMock.Object);

            // Assert
            Assert.AreSame(contextMock.Object, service.Context);
        }

        [Test]
        public void AssignFactoryValueProperly_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();

            // Act
            var service = new StoreServiceMock(contextMock.Object, factoryMock.Object);

            // Assert
            Assert.AreSame(factoryMock.Object, service.Factory);
        }
    }
}
