using System;
using FakeDbSet;
using Moq;
using NUnit.Framework;
using TravelGuide.Data.Contracts;
using TravelGuide.Models;
using TravelGuide.Models.Requests;
using TravelGuide.Models.Store;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Factories;
using TravelGuide.Services.Requests;

namespace TravelGuide.Tests.Services.Requests.RequestServiceTests
{
    [TestFixture]
    public class GetTotalPagesCount_Should
    {
        [Test]
        [TestCase(0, 1)]
        [TestCase(3, 1)]
        [TestCase(4, 2)]
        [TestCase(5, 2)]
        [TestCase(6, 2)]
        [TestCase(7, 3)]
        public void ReturnCorrectNumberOfPages_WhenThereIsNoQueryPresent(int itemsCount, int expectedPagesCount)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var requestsMock = new InMemoryDbSet<Request>(true);

            for (int i = 0; i < itemsCount; i++)
            {
                requestsMock.Add(new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "xxx" }, FirstName = "xxx", LastName = "svv", CreatedOn = DateTime.Now.AddSeconds(1) });
            }

            contextMock.Setup(x => x.Requests).Returns(requestsMock);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            string query = null;
            var pageSize = 3;

            // Act
            var pagesCount = service.GetTotalPagesCount(query, pageSize);

            // Assert
            Assert.AreEqual(expectedPagesCount, pagesCount);
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(3, 1)]
        [TestCase(4, 2)]
        [TestCase(5, 2)]
        [TestCase(6, 2)]
        [TestCase(7, 3)]
        public void ReturnCorrectNumberOfPages_WhenThereIsAQueryPresentAndAMatchInItemName(int itemsCount, int expectedPagesCount)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var requestsMock = new InMemoryDbSet<Request>(true);

            for (int i = 0; i < itemsCount; i++)
            {
                requestsMock.Add(new Request() { StoreItem = new StoreItem() { ItemName = "ass" }, User = new User() { UserName = "xxx" }, FirstName = "xxx", LastName = "svv", CreatedOn = DateTime.Now.AddSeconds(1) });
            }

            contextMock.Setup(x => x.Requests).Returns(requestsMock);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            string query = "A";
            var pageSize = 3;

            // Act
            var pagesCount = service.GetTotalPagesCount(query, pageSize);

            // Assert
            Assert.AreEqual(expectedPagesCount, pagesCount);
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(3, 1)]
        [TestCase(4, 2)]
        [TestCase(5, 2)]
        [TestCase(6, 2)]
        [TestCase(7, 3)]
        public void ReturnCorrectNumberOfPages_WhenThereIsAQueryPresentAndAMatchInUserName(int itemsCount, int expectedPagesCount)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var requestsMock = new InMemoryDbSet<Request>(true);

            for (int i = 0; i < itemsCount; i++)
            {
                requestsMock.Add(new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "xax" }, FirstName = "xxx", LastName = "svv", CreatedOn = DateTime.Now.AddSeconds(1) });
            }

            contextMock.Setup(x => x.Requests).Returns(requestsMock);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            string query = "A";
            var pageSize = 3;

            // Act
            var pagesCount = service.GetTotalPagesCount(query, pageSize);

            // Assert
            Assert.AreEqual(expectedPagesCount, pagesCount);
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(3, 1)]
        [TestCase(4, 2)]
        [TestCase(5, 2)]
        [TestCase(6, 2)]
        [TestCase(7, 3)]
        public void ReturnCorrectNumberOfPages_WhenThereIsAQueryPresentAndAMatchInFirstName(int itemsCount, int expectedPagesCount)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var requestsMock = new InMemoryDbSet<Request>(true);

            for (int i = 0; i < itemsCount; i++)
            {
                requestsMock.Add(new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "xxx" }, FirstName = "xAx", LastName = "svv", CreatedOn = DateTime.Now.AddSeconds(1) });
            }

            contextMock.Setup(x => x.Requests).Returns(requestsMock);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            string query = "A";
            var pageSize = 3;

            // Act
            var pagesCount = service.GetTotalPagesCount(query, pageSize);

            // Assert
            Assert.AreEqual(expectedPagesCount, pagesCount);
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(3, 1)]
        [TestCase(4, 1)]
        [TestCase(7, 1)]
        public void ReturnCorrectNumberOfPages_WhenThereIsAQueryPresentButNoMatchingRequests(int itemsCount, int expectedPagesCount)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var requestsMock = new InMemoryDbSet<Request>(true);

            for (int i = 0; i < itemsCount; i++)
            {
                requestsMock.Add(new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "xxx" }, FirstName = "xxx", LastName = "sxv", CreatedOn = DateTime.Now.AddSeconds(1) });
            }

            contextMock.Setup(x => x.Requests).Returns(requestsMock);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            string query = "A";
            var pageSize = 3;

            // Act
            var pagesCount = service.GetTotalPagesCount(query, pageSize);

            // Assert
            Assert.AreEqual(expectedPagesCount, pagesCount);
        }
    }
}