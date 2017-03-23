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
        [TestCase("")]
        [TestCase(null)]
        public void ThrowArgumentNullException_WhenUserIdIsNull(string id)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var service = new UserService(contextMock.Object);

            var username = "username";
            var email = "email";
            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";
            var isDeleted = true;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.UpdateUserInfo(id, username, email, firstName, lastName, phone, address, isDeleted));
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

            var username = "username";
            var email = "email";
            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";
            var isDeleted = true;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.UpdateUserInfo(id, username, email, firstName, lastName, phone, address, isDeleted));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ThrowArgumentNullException_WhenUsernameIsNull(string username)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var user = new User();
            var id = user.Id;

            contextMock.Setup(x => x.Users.Find(user.Id)).Returns((User)user);
            var service = new UserService(contextMock.Object);

            var email = "email";
            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";
            var isDeleted = true;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.UpdateUserInfo(id, username, email, firstName, lastName, phone, address, isDeleted));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ThrowArgumentNullException_WhenEmailIsNull(string email)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var user = new User();
            var id = user.Id;

            contextMock.Setup(x => x.Users.Find(user.Id)).Returns((User)user);
            var service = new UserService(contextMock.Object);

            var username = "username";
            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";
            var isDeleted = true;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.UpdateUserInfo(id, username, email, firstName, lastName, phone, address, isDeleted));
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

            var username = "username";
            var email = "email";
            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";
            var isDeleted = true;

            // Act
            service.UpdateUserInfo(id, username, email, firstName, lastName, phone, address, isDeleted);

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

            var username = "username";
            var email = "email";
            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";
            var isDeleted = true;

            // Act
            service.UpdateUserInfo(id, username, email, firstName, lastName, phone, address, isDeleted);

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

            var username = "username";
            var email = "email";
            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";
            var isDeleted = true;

            // Act
            service.UpdateUserInfo(id, username, email, firstName, lastName, phone, address, isDeleted);

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

            var username = "username";
            var email = "email";
            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";
            var isDeleted = true;

            // Act
            service.UpdateUserInfo(id, username, email, firstName, lastName, phone, address, isDeleted);

            // Assert
            Assert.AreEqual(address, user.Address);
        }

        [Test]
        public void CorrectlyAssignUsernameValue_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var user = new User();
            var id = user.Id;

            contextMock.Setup(x => x.Users.Find(user.Id)).Returns(user);
            var service = new UserService(contextMock.Object);

            var username = "username";
            var email = "email";
            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";
            var isDeleted = true;

            // Act
            service.UpdateUserInfo(id, username, email, firstName, lastName, phone, address, isDeleted);

            // Assert
            Assert.AreEqual(username, user.UserName);
        }

        [Test]
        public void CorrectlyAssignEmailValue_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var user = new User();
            var id = user.Id;

            contextMock.Setup(x => x.Users.Find(user.Id)).Returns(user);
            var service = new UserService(contextMock.Object);

            var username = "username";
            var email = "email";
            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";
            var isDeleted = true;

            // Act
            service.UpdateUserInfo(id, username, email, firstName, lastName, phone, address, isDeleted);

            // Assert
            Assert.AreEqual(email, user.Email);
        }

        [Test]
        public void CorrectlyAssignIsDeletedValue_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var user = new User();
            var id = user.Id;

            contextMock.Setup(x => x.Users.Find(user.Id)).Returns(user);
            var service = new UserService(contextMock.Object);

            var username = "username";
            var email = "email";
            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";
            var isDeleted = true;

            // Act
            service.UpdateUserInfo(id, username, email, firstName, lastName, phone, address, isDeleted);

            // Assert
            Assert.AreEqual(isDeleted, user.IsDeleted);
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

            var username = "username";
            var email = "email";
            var firstName = "firstName";
            var lastName = "lastName";
            var phone = "phone";
            var address = "address";
            var isDeleted = true;

            // Act
            service.UpdateUserInfo(id, username, email, firstName, lastName, phone, address, isDeleted);

            // Assert
            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
