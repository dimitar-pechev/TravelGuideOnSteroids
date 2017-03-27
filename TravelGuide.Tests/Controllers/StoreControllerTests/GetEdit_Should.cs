using Moq;
using NUnit.Framework;
using System;
using TestStack.FluentMVCTesting;
using TravelGuide.Areas.Store.Controllers;
using TravelGuide.Areas.Store.ViewModels;
using TravelGuide.Common.Contracts;
using TravelGuide.Models.Store;
using TravelGuide.Services.Store.Contracts;

namespace TravelGuide.Tests.Controllers.StoreControllerTests
{
    [TestFixture]
    public class GetEdit_Should
    {
        [Test]
        public void ReturnDefualtViewWithRespectiveViewModel_WhenParamsAreValid()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoreItemsController(storeServiceMock.Object, mappingServiceMock.Object, utilsMock.Object);

            var item = new StoreItem();
            storeServiceMock.Setup(x => x.GetStoreItemById(It.IsAny<Guid>())).Returns(item);

            var model = new CreateEditItemViewModel();
            mappingServiceMock.Setup(x => x.Map<CreateEditItemViewModel>(item)).Returns(model);

            // Act & Assert
            controller.WithCallTo(x => x.Edit(Guid.NewGuid()))
                .ShouldRenderDefaultView()
                .WithModel<CreateEditItemViewModel>(x => x == model);
        }
    }
}