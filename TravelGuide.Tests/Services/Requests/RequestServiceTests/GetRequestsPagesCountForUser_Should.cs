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
    public class GetRequestsPagesCountForUser_Should
    {
        [Test]
        [TestCase(0, 1)]
        [TestCase(3, 1)]
        [TestCase(4, 2)]
        [TestCase(6, 2)]
        [TestCase(7, 3)]
        public void ReturnCorrectNumberOfPages_WhenThereAreRequestsWithSuchAUserFound(int itemsCount, int expectedPagesCount)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var userId = "userId";

            var requestsMock = new InMemoryDbSet<Request>(true);
            for (int i = 0; i < itemsCount; i++)
            {
                requestsMock.Add(new Request() { UserId = userId });
            }

            contextMock.Setup(x => x.Requests).Returns(requestsMock);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Act
            var pagesCount = service.GetRequestsPagesCountForUser(userId, 3);

            // Assert
            Assert.AreEqual(expectedPagesCount, pagesCount);
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(4, 1)]
        [TestCase(7, 1)]
        public void ReturnCorrectNumberOfPages_WhenNoRequestUserIdIsMatching(int itemsCount, int expectedPagesCount)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var userId = "userId";

            var requestsMock = new InMemoryDbSet<Request>(true);
            for (int i = 0; i < itemsCount; i++)
            {
                requestsMock.Add(new Request());
            }

            contextMock.Setup(x => x.Requests).Returns(requestsMock);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Act
            var pagesCount = service.GetRequestsPagesCountForUser(userId, 3);

            // Assert
            Assert.AreEqual(expectedPagesCount, pagesCount);
        }
    }
}