using System;
using System.Linq;
using System.Web;
using Moq;
using NUnit.Framework;
using TravelGuide.Services.Store;
using TravelGuide.Services.Store.Contracts;

namespace TravelGuide.Tests.Services.Store.CartServiceTests
{
    [TestFixture]
    public class WriteCookie_Should
    {
        private const string CookieName = "store-items";

        [Test]
        public void ThrowArgumentException_IfTargetIterationsNumberIsNotParsable()
        {
            // Arrange
            var serviceMock = new Mock<IStoreService>();
            var cookie = new HttpCookie(CookieName);
            var username = "username";
            var itemId = Guid.NewGuid().ToString();
            var quantity = "asdasds";

            var service = new CartService(serviceMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.WriteCookie(cookie, username, itemId, quantity));
        }

        [Test]
        public void ReturnNewCookie_IfCookieIsNull()
        {
            // Arrange
            var serviceMock = new Mock<IStoreService>();
            var cookie = new HttpCookie(CookieName);
            var username = "username";
            var itemId = Guid.NewGuid().ToString();
            var quantity = "10";

            var service = new CartService(serviceMock.Object);

            // Act
            var newCookie = service.WriteCookie(null, username, itemId, quantity);

            // Assert
            Assert.IsInstanceOf<HttpCookie>(newCookie);
        }

        [Test]
        public void ReturnNewCookieWithTenItems_IfCookieIsNullAndHasTenQueuedItems()
        {
            // Arrange
            var serviceMock = new Mock<IStoreService>();
            var cookie = new HttpCookie(CookieName);
            var username = "username";
            var itemId = Guid.NewGuid().ToString();
            var quantity = "10";

            var service = new CartService(serviceMock.Object);

            // Act
            var newCookie = service.WriteCookie(null, username, itemId, quantity);

            // Assert
            Assert.AreEqual(10, newCookie.Value.Split(',').ToList().Count);
        }
    }
}
