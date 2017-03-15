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
    public class EditItem_Should
    {
        [Test]
        public void ReturnFalse_WhenPassedPriceIsNotParsable()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();
            var service = new StoreService(contextMock.Object, factoryMock.Object);
            var item = new StoreItem();

            var itemName = "itemName";
            var description = "description";
            var destinationFor = "Iliolousti Ellada";
            var imageUrl = "url";
            var brand = "brand";
            var price = "1asdas0";

            // Act
            var isParsed = service.EditItem(item, itemName, description, destinationFor, imageUrl, brand, price);

            // Assert
            Assert.IsFalse(isParsed);
        }

        [Test]
        public void ThrowInvalidOperationException_WhenNoSuchItemIsFound()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();
            var service = new StoreService(contextMock.Object, factoryMock.Object);
            var item = new StoreItem();

            var itemName = "itemName";
            var description = "description";
            var destinationFor = "Iliolousti Ellada";
            var imageUrl = "url";
            var brand = "brand";
            var price = "10";

            contextMock.Setup(x => x.StoreItems.Find(item.Id)).Returns((StoreItem)null);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.EditItem(item, itemName, description, destinationFor, imageUrl, brand, price));
        }

        [Test]
        public void ReturnsTrue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();
            var service = new StoreService(contextMock.Object, factoryMock.Object);
            var item = new StoreItem();

            var itemName = "itemName";
            var description = "description";
            var destinationFor = "Iliolousti Ellada";
            var imageUrl = "url";
            var brand = "brand";
            var price = "10";

            contextMock.Setup(x => x.StoreItems.Find(item.Id)).Returns(item);

            // Act
            var isSuccessful = service.EditItem(item, itemName, description, destinationFor, imageUrl, brand, price);

            // Assert
            Assert.IsTrue(isSuccessful);
        }

        [Test]
        public void AssignCorrectNameValue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();
            var service = new StoreService(contextMock.Object, factoryMock.Object);
            var item = new StoreItem();

            var itemName = "itemName";
            var description = "description";
            var destinationFor = "Iliolousti Ellada";
            var imageUrl = "url";
            var brand = "brand";
            var price = "10";

            contextMock.Setup(x => x.StoreItems.Find(item.Id)).Returns(item);

            // Act
            var isSuccessful = service.EditItem(item, itemName, description, destinationFor, imageUrl, brand, price);

            // Assert
            Assert.AreEqual(itemName, item.ItemName);
        }

        [Test]
        public void AssignCorrectDescriptionValue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();
            var service = new StoreService(contextMock.Object, factoryMock.Object);
            var item = new StoreItem();

            var itemName = "itemName";
            var description = "description";
            var destinationFor = "Iliolousti Ellada";
            var imageUrl = "url";
            var brand = "brand";
            var price = "10";

            contextMock.Setup(x => x.StoreItems.Find(item.Id)).Returns(item);

            // Act
            var isSuccessful = service.EditItem(item, itemName, description, destinationFor, imageUrl, brand, price);

            // Assert
            Assert.AreEqual(description, item.Description);
        }

        [Test]
        public void AssignCorrectRelatedDestinationValue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();
            var service = new StoreService(contextMock.Object, factoryMock.Object);
            var item = new StoreItem();

            var itemName = "itemName";
            var description = "description";
            var destinationFor = "Iliolousti Ellada";
            var imageUrl = "url";
            var brand = "brand";
            var price = "10";

            contextMock.Setup(x => x.StoreItems.Find(item.Id)).Returns(item);

            // Act
            var isSuccessful = service.EditItem(item, itemName, description, destinationFor, imageUrl, brand, price);

            // Assert
            Assert.AreEqual(destinationFor, item.DestinationFor);
        }

        [Test]
        public void AssignCorrectImageUrlValue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();
            var service = new StoreService(contextMock.Object, factoryMock.Object);
            var item = new StoreItem();

            var itemName = "itemName";
            var description = "description";
            var destinationFor = "Iliolousti Ellada";
            var imageUrl = "url";
            var brand = "brand";
            var price = "10";

            contextMock.Setup(x => x.StoreItems.Find(item.Id)).Returns(item);

            // Act
            var isSuccessful = service.EditItem(item, itemName, description, destinationFor, imageUrl, brand, price);

            // Assert
            Assert.AreEqual(imageUrl, item.ImageUrl);
        }

        [Test]
        public void AssignCorrectBrandValue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();
            var service = new StoreService(contextMock.Object, factoryMock.Object);
            var item = new StoreItem();

            var itemName = "itemName";
            var description = "description";
            var destinationFor = "Iliolousti Ellada";
            var imageUrl = "url";
            var brand = "brand";
            var price = "10";

            contextMock.Setup(x => x.StoreItems.Find(item.Id)).Returns(item);

            // Act
            var isSuccessful = service.EditItem(item, itemName, description, destinationFor, imageUrl, brand, price);

            // Assert
            Assert.AreEqual(brand, item.Brand);
        }

        [Test]
        public void AssignCorrectPriceValue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();
            var service = new StoreService(contextMock.Object, factoryMock.Object);
            var item = new StoreItem();

            var itemName = "itemName";
            var description = "description";
            var destinationFor = "Iliolousti Ellada";
            var imageUrl = "url";
            var brand = "brand";
            var price = "10";

            contextMock.Setup(x => x.StoreItems.Find(item.Id)).Returns(item);

            // Act
            var isSuccessful = service.EditItem(item, itemName, description, destinationFor, imageUrl, brand, price);

            // Assert
            Assert.AreEqual(double.Parse(price), item.Price);
        }
    }
}
