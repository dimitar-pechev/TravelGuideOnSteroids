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
    public class GetRequestsForUser_Should
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("wrong id")]
        public void ReturnOnlyRequestsWithThePassedUserId(string wrongId)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var userId = "userId";

            var requestWithCorrectUserId = new Request() { UserId = userId };
            var requestWithIncorrectUserId = new Request() { UserId = wrongId };

            var requestsMock = new InMemoryDbSet<Request>(true) { requestWithCorrectUserId, requestWithIncorrectUserId };

            contextMock.Setup(x => x.Requests).Returns(requestsMock);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            var page = 1;
            var pageSize = 5;

            // Act
            var requests = service.GetRequestsForUser(userId, page, pageSize);

            // Assert
            Assert.AreEqual(1, requests.Count());
            Assert.AreSame(requestWithCorrectUserId, requests.First());
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("wrong id")]
        public void ReturnNoRequests_WhenTheThereAreNoRequestsWithMatchingUserId(string wrongId)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var userId = "userId";

            var requestWithIncorrectUserId = new Request() { UserId = wrongId };

            var requestsMock = new InMemoryDbSet<Request>(true) { requestWithIncorrectUserId, requestWithIncorrectUserId };

            contextMock.Setup(x => x.Requests).Returns(requestsMock);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            var page = 1;
            var pageSize = 5;

            // Act
            var requests = service.GetRequestsForUser(userId, page, pageSize);

            // Assert
            Assert.AreEqual(0, requests.Count());
        }
    }
}
