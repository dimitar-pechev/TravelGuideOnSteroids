using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TravelGuide.Areas.Store.Controllers;
using TravelGuide.Areas.Store.ViewModels;
using TravelGuide.Common.Contracts;
using TravelGuide.Models.Store;
using TravelGuide.Services.Store.Contracts;

namespace TravelGuide.Tests.Controllers.StoreControllerTests
{
    [TestFixture]
    public class Search_Should
    {
        [Test]
        public void ReturnPartialViewWithRespectiveModel()
        {
            // Arrange
            var storeServiceMock = new Mock<IStoreService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new StoreItemsController(storeServiceMock.Object, mappingServiceMock.Object, utilsMock.Object);

            storeServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);

            var items = new List<StoreItem>();
            storeServiceMock.Setup(x => x.GetFilteredItemsByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(items);
            var mappedItems = new List<StoreItemViewModel>();
            mappingServiceMock.Setup(x => x.Map<IEnumerable<StoreItemViewModel>>(items)).Returns(mappedItems);
            var model = new StoreListViewModel();
            mappingServiceMock.Setup(x => x.Map<StoreListViewModel>(mappedItems)).Returns(model);
            utilsMock.Setup(x => x.AssignViewParams(model, It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(model);

            // Act & Assert
            controller.WithCallTo(x => x.Search(null, null))
                .ShouldRenderPartialView("_StoreItemsListPartial")
                .WithModel<StoreListViewModel>(x => x == model);
        }
    }
}
