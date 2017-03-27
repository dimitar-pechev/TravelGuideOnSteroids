using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using TravelGuide.Areas.Store.Controllers;
using TravelGuide.Common.Contracts;
using TravelGuide.Models.Store;
using TravelGuide.Services.Store.Contracts;

namespace TravelGuide.Tests.Controllers.StoreControllerTests
{
    [TestFixture]
    public class ChangeStatus_Should
    {
        [Test]
        public void RedirectToPartial_OnSuccessfullyChangedStatus()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoreItemsController(storeServiceMock.Object, mappingServiceMock.Object, utilsMock.Object);

            var model = new StoreItem();
            storeServiceMock.Setup(x => x.GetStoreItemById(It.IsAny<Guid>())).Returns(model);
            storeServiceMock.Setup(x => x.ChangeStatus(It.IsAny<Guid>(), It.IsAny<string>()));

            // Act & Assert
            controller.WithCallTo(x => x.ChangeStatus(Guid.NewGuid(), "string"))
                .ShouldRenderPartialView("_AddToCartPartial")
                .WithModel(model);
        }

        [Test]
        public void CallChangeStatusMethod_WhenParamsAreValid()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoreItemsController(storeServiceMock.Object, mappingServiceMock.Object, utilsMock.Object);

            var model = new StoreItem();
            storeServiceMock.Setup(x => x.GetStoreItemById(It.IsAny<Guid>())).Returns(model);
            storeServiceMock.Setup(x => x.ChangeStatus(It.IsAny<Guid>(), It.IsAny<string>()));

            // Act
            controller.ChangeStatus(Guid.NewGuid(), "string");

            // Assert
            storeServiceMock.Verify(x => x.ChangeStatus(It.IsAny<Guid>(), It.IsAny<string>()), Times.Once);
        }
    }
}
