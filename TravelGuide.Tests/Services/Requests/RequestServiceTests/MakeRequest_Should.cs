using System;
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
    public class MakeRequest_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedItemIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var id = "id";
            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";

            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.MakeRequest(null, id, firstName, lastName, phone, address));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedIdIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var item = new StoreItem();
            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";

            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.MakeRequest(item, null, firstName, lastName, phone, address));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedFirstNameIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var item = new StoreItem();
            var id = "id";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";

            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.MakeRequest(item, id, null, lastName, phone, address));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedLastNameIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var item = new StoreItem();
            var id = "id";
            var firstName = "firstName";
            var phone = "phone";
            var address = "address";

            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.MakeRequest(item, id, firstName, null, phone, address));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedPhoneIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var item = new StoreItem();
            var id = "id";
            var firstName = "firstName";
            var lastName = "lastName";
            var address = "address";

            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.MakeRequest(item, id, firstName, lastName, null, address));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedAddressIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var item = new StoreItem();
            var id = "id";
            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";

            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.MakeRequest(item, id, firstName, lastName, phone, null));
        }

        [Test]
        public void ThrowInvalidOperationException_WhenNoSuchUserIsFound()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var item = new StoreItem();
            var id = "id";
            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";

            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns((User)null);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.MakeRequest(item, id, firstName, lastName, phone, address));
        }

        [Test]
        public void CallFactoryMethod_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var item = new StoreItem();
            var id = "id";
            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";

            var user = new User();

            factoryMock.Setup(x => x.CreateRequest(It.IsAny<Guid>(), It.IsAny<StoreItem>(), It.IsAny<string>(), It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            contextMock.Setup(x => x.Requests.Add(It.IsAny<Request>()));
            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns(user);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Act
            service.MakeRequest(item, id, firstName, lastName, phone, address);

            // Assert
            factoryMock.Verify(x => x.CreateRequest(It.IsAny<Guid>(), It.IsAny<StoreItem>(), It.IsAny<string>(), It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CallAddMethod_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var item = new StoreItem();
            var id = "id";
            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";

            var user = new User();

            factoryMock.Setup(x => x.CreateRequest(It.IsAny<Guid>(), It.IsAny<StoreItem>(), It.IsAny<string>(), It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            contextMock.Setup(x => x.Requests.Add(It.IsAny<Request>()));
            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns(user);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Act
            service.MakeRequest(item, id, firstName, lastName, phone, address);

            // Assert
            contextMock.Verify(x => x.Requests.Add(It.IsAny<Request>()), Times.Once);
        }

        [Test]
        public void CallSaveChanges_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var item = new StoreItem();
            var id = "id";
            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";

            var user = new User();

            factoryMock.Setup(x => x.CreateRequest(It.IsAny<Guid>(), It.IsAny<StoreItem>(), It.IsAny<string>(), It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            contextMock.Setup(x => x.Requests.Add(It.IsAny<Request>()));
            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns(user);
            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Act
            service.MakeRequest(item, id, firstName, lastName, phone, address);

            // Assert
            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
