using System;
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
    public class UpdateRequestInfo_Should
    {
        [Test]
        public void ThrowInvalidOperationException_WhenNoSuchUserIsFound()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";
            var orderStatus = true;

            contextMock.Setup(x => x.Requests.Find(It.IsAny<Guid>())).Returns((Request)null);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.UpdateRequestInfo(Guid.NewGuid(), firstName, lastName, phone, address, orderStatus));
        }

        [Test]
        public void CorrectlySetNewFirstName_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";
            var orderStatus = true;

            var request = new Request();
            var id = request.Id;
            contextMock.Setup(x => x.Requests.Find(id)).Returns(request);

            // Act
            service.UpdateRequestInfo(id, firstName, lastName, phone, address, orderStatus);

            // Assert
            Assert.AreEqual(firstName, request.FirstName);
        }

        [Test]
        public void CorrectlySetNewLastName_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";
            var orderStatus = true;

            var request = new Request();
            var id = request.Id;
            contextMock.Setup(x => x.Requests.Find(id)).Returns(request);

            // Act
            service.UpdateRequestInfo(id, firstName, lastName, phone, address, orderStatus);

            // Assert
            Assert.AreEqual(lastName, request.LastName);
        }

        [Test]
        public void CorrectlySetNewPhone_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";
            var orderStatus = true;

            var request = new Request();
            var id = request.Id;
            contextMock.Setup(x => x.Requests.Find(id)).Returns(request);

            // Act
            service.UpdateRequestInfo(id, firstName, lastName, phone, address, orderStatus);

            // Assert
            Assert.AreEqual(phone, request.Phone);
        }

        [Test]
        public void CorrectlySetNewAddress_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";
            var orderStatus = true;

            var request = new Request();
            var id = request.Id;
            contextMock.Setup(x => x.Requests.Find(id)).Returns(request);

            // Act
            service.UpdateRequestInfo(id, firstName, lastName, phone, address, orderStatus);

            // Assert
            Assert.AreEqual(address, request.Address);
        }

        [Test]
        public void CorrectlySetNewOrderStatus_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";
            var orderStatus = true;

            var request = new Request();
            var prevStatus = request.Status;
            var id = request.Id;
            contextMock.Setup(x => x.Requests.Find(id)).Returns(request);

            // Act
            service.UpdateRequestInfo(id, firstName, lastName, phone, address, orderStatus);

            // Assert
            Assert.AreEqual("Awaiting confirmation", prevStatus);
            Assert.AreEqual("Confirmed!", request.Status);
        }
    }
}