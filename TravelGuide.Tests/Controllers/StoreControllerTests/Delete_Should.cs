using System;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TravelGuide.Areas.Store.Controllers;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Store.Contracts;

namespace TravelGuide.Tests.Controllers.StoreControllerTests
{
    [TestFixture]
    public class Delete_Should
    {
        [Test]
        public void RedirectToIndexAction_WhenParamsAreValid()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoreItemsController(storeServiceMock.Object, mappingServiceMock.Object, utilsMock.Object);

            // Act & Assert
            controller.WithCallTo(x => x.Delete(Guid.NewGuid()))
                .ShouldRedirectTo(x => x.Index(null, null));
        }

        [Test]
        public void CallDeleteItemMethod_WhenParamsAreValid()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoreItemsController(storeServiceMock.Object, mappingServiceMock.Object, utilsMock.Object);
            storeServiceMock.Setup(x => x.DeleteItem(It.IsAny<Guid>()));

            // Act
            controller.Delete(Guid.NewGuid());

            // Assert
            storeServiceMock.Verify(x => x.DeleteItem(It.IsAny<Guid>()), Times.Once);
        }
    }
}