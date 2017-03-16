using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Data.Contracts;
using TravelGuide.Models.Store;
using TravelGuide.Services.Factories;
using TravelGuide.Tests.Services.Store.Mocks;

namespace TravelGuide.Tests.Services.Store.StoreServiceTests
{
    [TestFixture]
    public class ChangeStatus_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedStatusIsInvalid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();

            var id = Guid.NewGuid();
            var service = new StoreServiceMock(contextMock.Object, factoryMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.ChangeStatus(id, null));
        }

        [Test]
        public void AlterPassedInstance_WhenPassedStatusIsValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();

            var id = Guid.NewGuid();
            var item = new StoreItem();
            item.InStock = true;
            var initialValue = item.InStock;

            contextMock.Setup(x => x.StoreItems.Find(It.IsAny<Guid>())).Returns(item);
            var service = new StoreServiceMock(contextMock.Object, factoryMock.Object);

            // Act
            service.ChangeStatus(id, "Depleted");

            // Assert
            Assert.IsTrue(initialValue);
            Assert.IsFalse(item.InStock);
        }

        [Test]
        public void ThrowInvalidOperationException_WhenNoSuchItemIsFound()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();

            var id = Guid.NewGuid();
            var item = new StoreItem();
            item.InStock = true;
            var initialValue = item.InStock;

            contextMock.Setup(x => x.StoreItems.Find(It.IsAny<Guid>())).Returns((StoreItem)null);
            var service = new StoreServiceMock(contextMock.Object, factoryMock.Object);

            // Act
            Assert.Throws<InvalidOperationException>(() => service.ChangeStatus(id, "Depleted"));
        }
    }
}
