using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Data.Contracts;
using TravelGuide.Models;
using TravelGuide.Services;

namespace TravelGuide.Tests.Services.Account.UserServiceTests
{
    [TestFixture]
    public class ActivateUser_Should
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ThrowArgumentNullException_WhenPassedIdIsNull(string id)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();

            var service = new UserService(contextMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.ActivateAccount(id));
        }

        [Test]
        public void ThrowInvalidOperationException_WhenNoUserIsFound()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var id = "id";
            var user = new User();

            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns((User)null);
            var service = new UserService(contextMock.Object);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.ActivateAccount(id));
        }

        [Test]
        public void AssignCorrectValue_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var id = "id";
            var user = new User();
            user.IsDeleted = true;
            var initialValie = user.IsDeleted;

            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns(user);
            var service = new UserService(contextMock.Object);

            // Act
            service.ActivateAccount(id);

            // Assert
            Assert.IsTrue(initialValie);
            Assert.IsFalse(user.IsDeleted);
        }

        [Test]
        public void CallSaveChanges_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var id = "id";
            var user = new User();
            user.IsDeleted = false;
            var initialValie = user.IsDeleted;

            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns(user);
            var service = new UserService(contextMock.Object);

            // Act
            service.ActivateAccount(id);

            // Assert
            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
