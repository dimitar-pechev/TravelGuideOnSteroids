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
    public class DeleteItemFromCookie_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedCookieIsNull()
        {
            // Arrange
            var storeService = new Mock<IStoreService>();
            var service = new CartService(storeService.Object);
            var itemId = "adasdasd";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.DeleteItemFromCookie(null, itemId));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedCookieNameIsInvalid()
        {
            // Arrange
            var storeService = new Mock<IStoreService>();
            var service = new CartService(storeService.Object);
            var itemId = "adasdasd";
            var cookie = new HttpCookie("asdasdad");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.DeleteItemFromCookie(cookie, itemId));
        }

        [Test]
        public void RemoveIdEntry_WhenPassedCookieIsValid()
        {
            // Arrange
            var storeService = new Mock<IStoreService>();
            var service = new CartService(storeService.Object);
            var itemId = "adasdasd";
            var list = new List<string>() { "dsa;dsa", "aksdjask", "adasdasd" };
            var cookie = new HttpCookie("store-items", string.Join(",", list));

            // Act
            var newCookie = service.DeleteItemFromCookie(cookie, itemId);
            var extractedList = newCookie.Value.Split(',').ToList();

            // Assert
            Assert.AreEqual(2, extractedList.Count);
            Assert.IsTrue(!extractedList.Contains(itemId));
        }
    }
}
