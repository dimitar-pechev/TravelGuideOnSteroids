using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Data.Contracts;
using TravelGuide.Models;
using TravelGuide.Services;

namespace TravelGuide.Tests.Services.Account.UserServiceTests
{
    [TestFixture]
    public class UpdateUserInfo_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenUserIdIsNull()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var service = new UserService(contextMock.Object);

            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.UpdateUserInfo(null, firstName, lastName, phone, address));
        }

        [Test]
        public void ThrowInvalidOperationException_NoUserIsFound()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var user = new User();
            var id = user.Id;

            contextMock.Setup(x => x.Users.Find(user.Id)).Returns((User)null);
            var service = new UserService(contextMock.Object);

            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.UpdateUserInfo(id, firstName, lastName, phone, address));
        }

        [Test]
        public void CorrectlyAssignFirstNameValue_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var user = new User();
            var id = user.Id;

            contextMock.Setup(x => x.Users.Find(user.Id)).Returns(user);
            var service = new UserService(contextMock.Object);

            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";

            // Act
            service.UpdateUserInfo(id, firstName, lastName, phone, address);

            // Assert
            Assert.AreEqual(firstName, user.FirstName);
        }

        [Test]
        public void CorrectlyAssignLastNameValue_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var user = new User();
            var id = user.Id;

            contextMock.Setup(x => x.Users.Find(user.Id)).Returns(user);
            var service = new UserService(contextMock.Object);

            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";

            // Act
            service.UpdateUserInfo(id, firstName, lastName, phone, address);

            // Assert
            Assert.AreEqual(lastName, user.LastName);
        }

        [Test]
        public void CorrectlyAssignPhoneValue_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var user = new User();
            var id = user.Id;

            contextMock.Setup(x => x.Users.Find(user.Id)).Returns(user);
            var service = new UserService(contextMock.Object);

            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";

            // Act
            service.UpdateUserInfo(id, firstName, lastName, phone, address);

            // Assert
            Assert.AreEqual(phone, user.PhoneNumber);
        }

        [Test]
        public void CorrectlyAssignAddressValue_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var user = new User();
            var id = user.Id;

            contextMock.Setup(x => x.Users.Find(user.Id)).Returns(user);
            var service = new UserService(contextMock.Object);

            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";

            // Act
            service.UpdateUserInfo(id, firstName, lastName, phone, address);

            // Assert
            Assert.AreEqual(address, user.Address);
        }

        [Test]
        public void CallSaveChanges_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var user = new User();
            var id = user.Id;

            contextMock.Setup(x => x.Users.Find(user.Id)).Returns(user);
            var service = new UserService(contextMock.Object);

            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";

            // Act
            service.UpdateUserInfo(id, firstName, lastName, phone, address);

            // Assert
            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
