using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Moq;
using NUnit.Framework;
using TravelGuide.Services.Store;
using TravelGuide.Services.Store.Contracts;

namespace TravelGuide.Tests.Services.Store.CartServiceTests
{
    [TestFixture]
    public class ExtractItemsFromCookie_Should
    {
        private const string CookieName = "store-items";

        [Test]
        public void ReturnEmptyList_WhenCookieIsNull()
        {
            // Arrange
            var serviceMock = new Mock<IStoreService>();

            var service = new CartService(serviceMock.Object);

            // Act
            var list = service.ExtractItemsFromCookie(null).ToList();

            // Assert
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void ReturnEmptyList_WhenCookieNameIsDifferent()
        {
            // Arrange
            var serviceMock = new Mock<IStoreService>();

            var service = new CartService(serviceMock.Object);
            var cookie = new HttpCookie("other name");

            // Act
            var list = service.ExtractItemsFromCookie(cookie).ToList();

            // Assert
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void ReturnCorrectList_WhenInputIsValid()
        {
            // Arrange
            var serviceMock = new Mock<IStoreService>();

            var service = new CartService(serviceMock.Object);
            var list = new List<string>() { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };
            var cookie = new HttpCookie(CookieName, string.Join(",", list));
            serviceMock.Setup(x => x.GetStoreItemById(It.IsAny<Guid>()));

            // Act
            var extractedList = service.ExtractItemsFromCookie(cookie).ToList();

            // Assert
            serviceMock.Verify(x => x.GetStoreItemById(It.IsAny<Guid>()), Times.Exactly(2));
        }
    }
}
