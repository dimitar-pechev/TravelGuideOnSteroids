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
    public class PostCreate_Should
    {
        [Test]
        public void ReturnDefaultView_WhenModelStateIsInvalid()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoreItemsController(storeServiceMock.Object, mappingServiceMock.Object, utilsMock.Object);
            controller.ViewData.ModelState.AddModelError("adasd", "asda");

            var model = new CreateEditItemViewModel();

            // Act & Assert
            controller.WithCallTo(x => x.Create(model))
                .ShouldRenderDefaultView()
                .WithModel<CreateEditItemViewModel>(x => x == model);
        }

        [Test]
        public void RedirectToIndexAction_WhenParamsAreValid()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoreItemsController(storeServiceMock.Object, mappingServiceMock.Object, utilsMock.Object);

            var model = new CreateEditItemViewModel();

            // Act & Assert
            controller.WithCallTo(x => x.Create(model))
                .ShouldRedirectTo(x => x.Index(null, null));
        }

        [Test]
        public void CallAddNewItemMethod_WhenParamsAreValid()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoreItemsController(storeServiceMock.Object, mappingServiceMock.Object, utilsMock.Object);
            storeServiceMock.Setup(x => x.AddNewItem(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            var model = new CreateEditItemViewModel();

            // Act
            controller.Create(model);

            // Assert
            storeServiceMock.Verify(x => x.AddNewItem(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}