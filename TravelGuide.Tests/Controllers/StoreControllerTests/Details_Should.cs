using System;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TravelGuide.Areas.Store.Controllers;
using TravelGuide.Common.Contracts;
using TravelGuide.Models.Store;
using TravelGuide.Services.Store.Contracts;

namespace TravelGuide.Tests.Controllers.StoreControllerTests
{
    [TestFixture]
    public class Details_Should
    {
        [Test]
        public void RedirectToIndexAction_WhenPassedIdIsNull()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoreItemsController(storeServiceMock.Object, mappingServiceMock.Object, utilsMock.Object);

            // Act & Assert
            controller.WithCallTo(x => x.Details(null))
                .ShouldRedirectTo(x => x.Index(null, null));
        }

        [Test]
        public void ReturnDefaultViewWithRespectiveModel_WhenParamsAreValid()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoreItemsController(storeServiceMock.Object, mappingServiceMock.Object, utilsMock.Object);

            var storeItem = new StoreItem();
            storeServiceMock.Setup(x => x.GetStoreItemById(It.IsAny<Guid>())).Returns(storeItem);

            // Act & Assert
            controller.WithCallTo(x => x.Details(Guid.NewGuid()))
                .ShouldRenderDefaultView()
                .WithModel<StoreItem>(x => x == storeItem);
        }

        [Test]
        public void RedirectToIndexAction_WhenNoSuchItemIsFound()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoreItemsController(storeServiceMock.Object, mappingServiceMock.Object, utilsMock.Object);

            // Act & Assert
            controller.WithCallTo(x => x.Details(Guid.NewGuid()))
                .ShouldRedirectTo(x => x.Index(null, null));
        }

        [Test]
        public void CallGetItemById_WhenParamsAreValid()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoreItemsController(storeServiceMock.Object, mappingServiceMock.Object, utilsMock.Object);

            var storeItem = new StoreItem();
            storeServiceMock.Setup(x => x.GetStoreItemById(It.IsAny<Guid>())).Returns(storeItem);

            // Act
            controller.Details(Guid.NewGuid());

            // Assert
            storeServiceMock.Verify(x => x.GetStoreItemById(It.IsAny<Guid>()), Times.Once);
        }
    }
}