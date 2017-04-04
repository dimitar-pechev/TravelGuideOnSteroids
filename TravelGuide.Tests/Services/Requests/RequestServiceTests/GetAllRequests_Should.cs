using System.Linq;
using FakeDbSet;
using Moq;
using NUnit.Framework;
using TravelGuide.Data.Contracts;
using TravelGuide.Models.Requests;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Factories;
using TravelGuide.Services.Requests;

namespace TravelGuide.Tests.Services.Requests.RequestServiceTests
{
    [TestFixture]
    public class GetAllRequests_Should
    {
        [Test]
        public void ReturnAnEmptyCollection_WhenThereAreNoRequests()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();
            var requestsMock = new InMemoryDbSet<Request>(true);

            contextMock.Setup(x => x.Requests).Returns(requestsMock);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Act
            var requests = service.GetAllRequests();

            // Assert
            Assert.AreEqual(0, requests.Count());
        }

        [Test]
        public void ReturnACollectionWithTwoItems_WhenThereAreTwoRequestsInTotal()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();
            var requestsMock = new InMemoryDbSet<Request>(true) { new Request(), new Request() };

            contextMock.Setup(x => x.Requests).Returns(requestsMock);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Act
            var requests = service.GetAllRequests();

            // Assert
            Assert.AreEqual(2, requests.Count());
        }
    }
}
