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
    public class GetStoreItemById_Should
    {
        [Test]
        public void ReturnCorrectItemInstance_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();
            var item = new StoreItem();

            contextMock.Setup(x => x.StoreItems.Find(It.IsAny<Guid>())).Returns(item);
            var service = new StoreService(contextMock.Object, factoryMock.Object);

            // Act
            var dbItem = service.GetStoreItemById(Guid.NewGuid());

            // Assert
            Assert.AreSame(item, dbItem);
        }
    }
}
