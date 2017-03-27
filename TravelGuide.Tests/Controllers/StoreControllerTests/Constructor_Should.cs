using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Areas.Store.Controllers;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Store.Contracts;
using TravelGuide.Tests.Controllers.StoreControllerTests.Mocks;

namespace TravelGuide.Tests.Controllers.StoreControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedStoreServiceIsNull()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StoreItemsController(null, mappingServiceMock.Object, utilsMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedMappingServiceIsNull()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StoreItemsController(storeServiceMock.Object, null, utilsMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedUtilsServiceIsNull()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StoreItemsController(storeServiceMock.Object, mappingServiceMock.Object, null));
        }

        [Test]
        public void ReturnCorrectInstance_WhenParamsAreValid()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act
            var controller = new StoreItemsController(storeServiceMock.Object, mappingServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.IsInstanceOf<StoreItemsController>(controller);
        }

        [Test]
        public void CorrectlyAssignStoreService_WhenParamsAreValid()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act
            var controller = new StoreControllerMock(storeServiceMock.Object, mappingServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.AreSame(storeServiceMock.Object, controller.StoreService);
        }

        [Test]
        public void CorrectlyAssignMappingService_WhenParamsAreValid()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act
            var controller = new StoreControllerMock(storeServiceMock.Object, mappingServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.AreSame(mappingServiceMock.Object, controller.MappingService);
        }

        [Test]
        public void CorrectlyAssignUtilsService_WhenParamsAreValid()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act
            var controller = new StoreControllerMock(storeServiceMock.Object, mappingServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.AreSame(utilsMock.Object, controller.Utils);
        }
    }
}
