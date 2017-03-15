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
    public class AddNewItem_Should
    {
        [Test]
        public void ThrowNullArgumentException_WhenPassedItemNameIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();

            var service = new StoreService(contextMock.Object, factoryMock.Object);

            var description = "description";
            var destinationFor = "Iliolousti Ellada";
            var imageUrl = "url";
            var brand = "brand";
            var price = "10";

            // Act
            Assert.Throws<ArgumentNullException>(() => service.AddNewItem(null, description, destinationFor, imageUrl, brand, price));
        }

        [Test]
        public void ThrowNullArgumentException_WhenPassedDescriptionIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();

            var service = new StoreService(contextMock.Object, factoryMock.Object);

            var itemName = "itemName";
            var destinationFor = "Iliolousti Ellada";
            var imageUrl = "url";
            var brand = "brand";
            var price = "10";

            // Act
            Assert.Throws<ArgumentNullException>(() => service.AddNewItem(itemName, null, destinationFor, imageUrl, brand, price));
        }

        [Test]
        public void ThrowNullArgumentException_WhenPassedRelatedDestinationIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();

            var service = new StoreService(contextMock.Object, factoryMock.Object);

            var itemName = "itemName";
            var description = "description";
            var imageUrl = "url";
            var brand = "brand";
            var price = "10";

            // Act
            Assert.Throws<ArgumentNullException>(() => service.AddNewItem(itemName, description, null, imageUrl, brand, price));
        }

        [Test]
        public void ThrowNullArgumentException_WhenPassedImageUrlIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();

            var service = new StoreService(contextMock.Object, factoryMock.Object);

            var itemName = "itemName";
            var description = "description";
            var destinationFor = "Iliolousti Ellada";
            var brand = "brand";
            var price = "10";

            // Act
            Assert.Throws<ArgumentNullException>(() => service.AddNewItem(itemName, description, destinationFor, null, brand, price));
        }

        [Test]
        public void ThrowNullArgumentException_WhenPassedBrandIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();

            var service = new StoreService(contextMock.Object, factoryMock.Object);

            var itemName = "itemName";
            var description = "description";
            var destinationFor = "Iliolousti Ellada";
            var imageUrl = "url";
            var price = "10";

            // Act
            Assert.Throws<ArgumentNullException>(() => service.AddNewItem(itemName, description, destinationFor, imageUrl, null, price));
        }

        [Test]
        public void ThrowNullArgumentException_WhenPassedPriceIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();

            var service = new StoreService(contextMock.Object, factoryMock.Object);

            var itemName = "itemName";
            var description = "description";
            var destinationFor = "Iliolousti Ellada";
            var imageUrl = "url";
            var brand = "brand";

            // Act
            Assert.Throws<ArgumentNullException>(() => service.AddNewItem(itemName, description, destinationFor, imageUrl, brand, null));
        }

        [Test]
        public void ReturnFalse_WhenPassedPriceIsNotParsableToDouble()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();

            var service = new StoreService(contextMock.Object, factoryMock.Object);

            var itemName = "itemName";
            var description = "description";
            var destinationFor = "Iliolousti Ellada";
            var imageUrl = "url";
            var brand = "brand";
            var price = "asdas";

            // Act
            var isParsed = service.AddNewItem(itemName, description, destinationFor, imageUrl, brand, price);

            // Assert
            Assert.IsFalse(isParsed);
        }

        [Test]
        public void CallFactoryMethod_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();

            var service = new StoreService(contextMock.Object, factoryMock.Object);

            var itemName = "itemName";
            var description = "description";
            var destinationFor = "Iliolousti Ellada";
            var imageUrl = "url";
            var brand = "brand";
            var price = "10";

            factoryMock.Setup(x => x.CreateStoreItem(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>()));
            contextMock.Setup(x => x.StoreItems.Add(It.IsAny<StoreItem>()));

            // Act
            var isParsed = service.AddNewItem(itemName, description, destinationFor, imageUrl, brand, price);

            // Assert
            factoryMock.Verify(x => x.CreateStoreItem(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>()), Times.Once);
        }

        [Test]
        public void CallSaveChanges_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();

            var service = new StoreService(contextMock.Object, factoryMock.Object);

            var itemName = "itemName";
            var description = "description";
            var destinationFor = "Iliolousti Ellada";
            var imageUrl = "url";
            var brand = "brand";
            var price = "10";

            factoryMock.Setup(x => x.CreateStoreItem(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>()));
            contextMock.Setup(x => x.StoreItems.Add(It.IsAny<StoreItem>()));

            // Act
            var isParsed = service.AddNewItem(itemName, description, destinationFor, imageUrl, brand, price);

            // Assert
            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void ReturnTrue_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IStoreItemFactory>();

            var service = new StoreService(contextMock.Object, factoryMock.Object);

            var itemName = "itemName";
            var description = "description";
            var destinationFor = "Iliolousti Ellada";
            var imageUrl = "url";
            var brand = "brand";
            var price = "10";

            factoryMock.Setup(x => x.CreateStoreItem(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>()));
            contextMock.Setup(x => x.StoreItems.Add(It.IsAny<StoreItem>()));

            // Act
            var isSuccessful = service.AddNewItem(itemName, description, destinationFor, imageUrl, brand, price);

            // Assert
            Assert.IsTrue(isSuccessful);
        }
    }
}
