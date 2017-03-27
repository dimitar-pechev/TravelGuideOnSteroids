using System;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TravelGuide.Areas.Store.Controllers;
using TravelGuide.Areas.Store.ViewModels;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Store.Contracts;

namespace TravelGuide.Tests.Controllers.StoreControllerTests
{
    [TestFixture]
    public class PostEdit_Should
    {
        [Test]
        public void RedirectToAction_WhenParamsAreValid()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoreItemsController(storeServiceMock.Object, mappingServiceMock.Object, utilsMock.Object);

            var model = new CreateEditItemViewModel();
            storeServiceMock.Setup(x => x.EditItem(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            // Act & Assert
            controller.WithCallTo(x => x.Edit(model))
                .ShouldRedirectTo(x => x.Details(model.Id));
        }

        [Test]
        public void CallEditItemMethod_WhenParamsAreValid()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoreItemsController(storeServiceMock.Object, mappingServiceMock.Object, utilsMock.Object);

            var model = new CreateEditItemViewModel();
            storeServiceMock.Setup(x => x.EditItem(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            // Act
            controller.Edit(model);

            // Assert
            storeServiceMock.Verify(x => x.EditItem(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ReturnDefaultViewWithModel_WhenModelStateIsInvalid()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoreItemsController(storeServiceMock.Object, mappingServiceMock.Object, utilsMock.Object);

            var model = new CreateEditItemViewModel();
            storeServiceMock.Setup(x => x.EditItem(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            controller.ViewData.ModelState.AddModelError("asdasd", "asdasdasd");

            // Act & Assert
            controller.WithCallTo(x => x.Edit(model))
                .ShouldRenderDefaultView()
                .WithModel<CreateEditItemViewModel>(x => x == model);
        }
    }
}