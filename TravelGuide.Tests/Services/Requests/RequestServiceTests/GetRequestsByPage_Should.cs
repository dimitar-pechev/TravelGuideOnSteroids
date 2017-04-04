using System;
using System.Linq;
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
    public class GetRequestsByPage_Should
    {
        [Test]
        [TestCase(1, 3)]
        [TestCase(2, 1)]
        public void ReturnCorrectNumberOfRequests_WhenNoQueryIsPresent(int page, int expectedNumberOfItems)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();
            var requestsMock = new InMemoryDbSet<Request>(true) { new Request(), new Request(), new Request(), new Request() };

            contextMock.Setup(x => x.Requests).Returns(requestsMock);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            string query = null;
            var pageSize = 3;

            // Act
            var requests = service.GetRequestsByPage(query, page, pageSize);

            // Assert
            Assert.AreEqual(expectedNumberOfItems, requests.Count());
        }

        [Test]
        public void ReturnRequestsInCorrectOrder_WhenNoQueryIsPresent()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();
            var firstRequest = new Request() { CreatedOn = DateTime.Now.AddSeconds(1) };
            var secondRequest = new Request() { CreatedOn = DateTime.Now.AddSeconds(2) };
            var thirdRequest = new Request() { CreatedOn = DateTime.Now.AddSeconds(3) };
            var fourthRequest = new Request() { CreatedOn = DateTime.Now.AddSeconds(4) };

            var requestsMock = new InMemoryDbSet<Request>(true) { secondRequest, thirdRequest, firstRequest, fourthRequest };

            contextMock.Setup(x => x.Requests).Returns(requestsMock);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            string query = null;
            var page = 1;
            var pageSize = 3;

            // Act
            var requests = service.GetRequestsByPage(query, page, pageSize);

            // Assert
            // Items ordered by date of creation in DESCENDING order
            Assert.AreSame(fourthRequest, requests.ElementAt(0));
            Assert.AreSame(thirdRequest, requests.ElementAt(1));
            Assert.AreSame(secondRequest, requests.ElementAt(2));
        }

        [Test]
        [TestCase(1, 3)]
        [TestCase(2, 1)]
        public void ReturnCorrectNumberOfRequests_WhenThereIsAQueryPresent(int page, int expectedNumberOfItems)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();
            var firstRequest = new Request() { StoreItem = new StoreItem() { ItemName = "xsa" }, User = new User() { UserName = "a" }, CreatedOn = DateTime.Now.AddSeconds(1) };
            var secondRequest = new Request() { StoreItem = new StoreItem() { ItemName = "xsa" }, User = new User() { UserName = "asd" }, CreatedOn = DateTime.Now.AddSeconds(2) };
            var thirdRequest = new Request() { StoreItem = new StoreItem() { ItemName = "xsa" }, User = new User() { UserName = "xcc" }, CreatedOn = DateTime.Now.AddSeconds(3) };
            var fourthRequest = new Request() { StoreItem = new StoreItem() { ItemName = "xsa" }, User = new User() { UserName = "asdas" }, CreatedOn = DateTime.Now.AddSeconds(4) };

            var requestsMock = new InMemoryDbSet<Request>(true) { secondRequest, thirdRequest, firstRequest, fourthRequest };

            contextMock.Setup(x => x.Requests).Returns(requestsMock);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            string query = "a";
            var pageSize = 3;

            // Act
            var requests = service.GetRequestsByPage(query, page, pageSize);

            // Assert
            // Items ordered by date of creation in DESCENDING order
            Assert.AreEqual(expectedNumberOfItems, requests.Count());
        }

        [Test]
        public void ReturnRequestsInCorrectOrder_WhenThereIsAQueryPresent()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();
            var firstRequest = new Request() { StoreItem = new StoreItem() { ItemName = "xsa" }, User = new User() { UserName = "a" }, CreatedOn = DateTime.Now.AddSeconds(1) };
            var secondRequest = new Request() { StoreItem = new StoreItem() { ItemName = "xsa" }, User = new User() { UserName = "asd" }, CreatedOn = DateTime.Now.AddSeconds(2) };
            var thirdRequest = new Request() { StoreItem = new StoreItem() { ItemName = "xsa" }, User = new User() { UserName = "xcc" }, CreatedOn = DateTime.Now.AddSeconds(3) };
            var fourthRequest = new Request() { StoreItem = new StoreItem() { ItemName = "xsa" }, User = new User() { UserName = "asdas" }, CreatedOn = DateTime.Now.AddSeconds(4) };

            var requestsMock = new InMemoryDbSet<Request>(true) { secondRequest, thirdRequest, firstRequest, fourthRequest };

            contextMock.Setup(x => x.Requests).Returns(requestsMock);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            string query = "a";
            var page = 1;
            var pageSize = 3;

            // Act
            var requests = service.GetRequestsByPage(query, page, pageSize);

            // Assert
            // Items ordered by date of creation in DESCENDING order
            Assert.AreSame(fourthRequest, requests.ElementAt(0));
            Assert.AreSame(thirdRequest, requests.ElementAt(1));
            Assert.AreSame(secondRequest, requests.ElementAt(2));
        }

        [Test]
        [TestCase("A")]
        [TestCase("a")]
        public void ReturnCorrectRequests_WhenThereIsAQueryPresentAndAMatchInUsername(string query)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();
            var firstRequest = new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "A" }, FirstName = "xxx", LastName = "xxx", CreatedOn = DateTime.Now.AddSeconds(1) };
            var secondRequest = new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "asd" }, FirstName = "xxx", LastName = "xxx", CreatedOn = DateTime.Now.AddSeconds(2) };
            var thirdRequest = new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "xcc" }, FirstName = "xxx", LastName = "xxx", CreatedOn = DateTime.Now.AddSeconds(3) };
            var fourthRequest = new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "ss" }, FirstName = "xxx", LastName = "xxx", CreatedOn = DateTime.Now.AddSeconds(4) };

            var requestsMock = new InMemoryDbSet<Request>(true) { secondRequest, thirdRequest, firstRequest, fourthRequest };

            contextMock.Setup(x => x.Requests).Returns(requestsMock);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            var page = 1;
            var pageSize = 3;

            // Act
            var requests = service.GetRequestsByPage(query, page, pageSize);

            // Assert
            // Items ordered by date of creation in DESCENDING order
            Assert.AreEqual(2, requests.Count());
            Assert.AreSame(secondRequest, requests.ElementAt(0));
            Assert.AreSame(firstRequest, requests.ElementAt(1));
        }

        [Test]
        [TestCase("A")]
        [TestCase("a")]
        public void ReturnCorrectRequests_WhenThereIsAQueryPresentAndAMatchInStoreItemName(string query)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();
            var firstRequest = new Request() { StoreItem = new StoreItem() { ItemName = "ass" }, User = new User() { UserName = "xxx" }, FirstName = "xxx", LastName = "xxx", CreatedOn = DateTime.Now.AddSeconds(1) };
            var secondRequest = new Request() { StoreItem = new StoreItem() { ItemName = "sAs" }, User = new User() { UserName = "xxx" }, FirstName = "xxx", LastName = "xxx", CreatedOn = DateTime.Now.AddSeconds(2) };
            var thirdRequest = new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "xcc" }, FirstName = "xxx", LastName = "xxx", CreatedOn = DateTime.Now.AddSeconds(3) };
            var fourthRequest = new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "ss" }, FirstName = "xxx", LastName = "xxx", CreatedOn = DateTime.Now.AddSeconds(4) };

            var requestsMock = new InMemoryDbSet<Request>(true) { secondRequest, thirdRequest, firstRequest, fourthRequest };

            contextMock.Setup(x => x.Requests).Returns(requestsMock);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            var page = 1;
            var pageSize = 3;

            // Act
            var requests = service.GetRequestsByPage(query, page, pageSize);

            // Assert
            // Items ordered by date of creation in DESCENDING order
            Assert.AreEqual(2, requests.Count());
            Assert.AreSame(secondRequest, requests.ElementAt(0));
            Assert.AreSame(firstRequest, requests.ElementAt(1));
        }

        [Test]
        [TestCase("A")]
        [TestCase("a")]
        public void ReturnCorrectRequests_WhenThereIsAQueryPresentAndAMatchInRequestFirstName(string query)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();
            var firstRequest = new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "xxx" }, FirstName = "Axx", LastName = "xxx", CreatedOn = DateTime.Now.AddSeconds(1) };
            var secondRequest = new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "xxx" }, FirstName = "xax", LastName = "xxx", CreatedOn = DateTime.Now.AddSeconds(2) };
            var thirdRequest = new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "xcc" }, FirstName = "xxx", LastName = "xxx", CreatedOn = DateTime.Now.AddSeconds(3) };
            var fourthRequest = new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "ss" }, FirstName = "xxx", LastName = "xxx", CreatedOn = DateTime.Now.AddSeconds(4) };

            var requestsMock = new InMemoryDbSet<Request>(true) { secondRequest, thirdRequest, firstRequest, fourthRequest };

            contextMock.Setup(x => x.Requests).Returns(requestsMock);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            var page = 1;
            var pageSize = 3;

            // Act
            var requests = service.GetRequestsByPage(query, page, pageSize);

            // Assert
            // Items ordered by date of creation in DESCENDING order
            Assert.AreEqual(2, requests.Count());
            Assert.AreSame(secondRequest, requests.ElementAt(0));
            Assert.AreSame(firstRequest, requests.ElementAt(1));
        }

        [Test]
        [TestCase("A")]
        [TestCase("a")]
        public void ReturnCorrectRequests_WhenThereIsAQueryPresentAndAMatchInRequestLastName(string query)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();
            var firstRequest = new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "xxx" }, FirstName = "xxx", LastName = "a", CreatedOn = DateTime.Now.AddSeconds(1) };
            var secondRequest = new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "xxx" }, FirstName = "xxx", LastName = "Asj", CreatedOn = DateTime.Now.AddSeconds(2) };
            var thirdRequest = new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "xcc" }, FirstName = "xxx", LastName = "xxx", CreatedOn = DateTime.Now.AddSeconds(3) };
            var fourthRequest = new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "ss" }, FirstName = "xxx", LastName = "xxx", CreatedOn = DateTime.Now.AddSeconds(4) };

            var requestsMock = new InMemoryDbSet<Request>(true) { secondRequest, thirdRequest, firstRequest, fourthRequest };

            contextMock.Setup(x => x.Requests).Returns(requestsMock);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            var page = 1;
            var pageSize = 3;

            // Act
            var requests = service.GetRequestsByPage(query, page, pageSize);

            // Assert
            // Items ordered by date of creation in DESCENDING order
            Assert.AreEqual(2, requests.Count());
            Assert.AreSame(secondRequest, requests.ElementAt(0));
            Assert.AreSame(firstRequest, requests.ElementAt(1));
        }

        [Test]
        [TestCase("A")]
        [TestCase("a")]
        public void ReturnAnEmptyList_WhenThereAreNoRequestMatchingTheQuery(string query)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();
            var firstRequest = new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "xxx" }, FirstName = "xxx", LastName = "svv", CreatedOn = DateTime.Now.AddSeconds(1) };
            var secondRequest = new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "xxx" }, FirstName = "xxx", LastName = "eee", CreatedOn = DateTime.Now.AddSeconds(2) };
            var thirdRequest = new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "xcc" }, FirstName = "xxx", LastName = "xxx", CreatedOn = DateTime.Now.AddSeconds(3) };
            var fourthRequest = new Request() { StoreItem = new StoreItem() { ItemName = "sss" }, User = new User() { UserName = "ss" }, FirstName = "xxx", LastName = "xxx", CreatedOn = DateTime.Now.AddSeconds(4) };

            var requestsMock = new InMemoryDbSet<Request>(true) { secondRequest, thirdRequest, firstRequest, fourthRequest };

            contextMock.Setup(x => x.Requests).Returns(requestsMock);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            var page = 1;
            var pageSize = 3;

            // Act
            var requests = service.GetRequestsByPage(query, page, pageSize);

            // Assert
            Assert.AreEqual(0, requests.Count());
        }
    }
}
