using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Data.Contracts;
using TravelGuide.Models.Store;
using TravelGuide.Services.Factories;
using TravelGuide.Services.Store;

namespace TravelGuide.Tests.Services.Store.StoreServiceTests
{
    [TestFixture]
    public class DeleteItem_Should
    {
        [Test]
        public void ThrowInvalidOperationException_WhenNoSuchItemWasFound()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();

            var id = Guid.NewGuid();
            var item = new StoreItem();

            contextMock.Setup(x => x.StoreItems.Find(It.IsAny<Guid>())).Returns((StoreItem)null);
            var service = new StoreService(contextMock.Object, factoryMock.Object);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.DeleteItem(id));
        }

        [Test]
        public void ChangeInstanceProperty_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();

            var id = Guid.NewGuid();
            var item = new StoreItem();
            item.IsDeleted = false;
            var initialValue = item.IsDeleted;

            contextMock.Setup(x => x.StoreItems.Add(It.IsAny<StoreItem>()));
            contextMock.Setup(x => x.StoreItems.Find(It.IsAny<Guid>())).Returns(item);
            var service = new StoreService(contextMock.Object, factoryMock.Object);

            // Act
            service.DeleteItem(id);

            // Assert
            Assert.IsFalse(initialValue);
            Assert.IsTrue(item.IsDeleted);
        }
    }
}
