using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Services.Store.Contracts;
using TravelGuide.Tests.Services.Store.Mocks;

namespace TravelGuide.Tests.Services.Store.CartServiceTests
{
    [TestFixture]
    public class GetClearedCookie_Should
    {
        private const string CookieName = "store-items";

        [Test]
        public void ThrowArgumentNullException_WhenInputParamIsNull()
        {
            // Arrange
            var serviceMock = new Mock<IStoreService>();

            var service = new CartServiceMock(serviceMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.GetClearedCookie(null));
        }

        [Test]
        public void ReturnCookieWithCorrectNameAndNoValue_WhenUsernameIsValid()
        {
            // Arrange
            var serviceMock = new Mock<IStoreService>();
            var username = "username";
            var service = new CartServiceMock(serviceMock.Object);

            // Act
            var cookie = service.GetClearedCookie(username);

            // Assert
            Assert.AreEqual(CookieName + username, cookie.Name);
            Assert.AreEqual(string.Empty, cookie.Value);
        }
    }
}
