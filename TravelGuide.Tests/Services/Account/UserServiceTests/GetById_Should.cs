using Moq;
using NUnit.Framework;
using TravelGuide.Data.Contracts;
using TravelGuide.Models;
using TravelGuide.Services;

namespace TravelGuide.Tests.Services.Account.UserServiceTests
{
    [TestFixture]
    public class GetById_Should
    {
        [Test]
        public void ReturnCorrectInstance_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var user = new User();
            var id = user.Id;

            contextMock.Setup(x => x.Users.Find(user.Id)).Returns(user);
            var service = new UserService(contextMock.Object);

            // Act
            var dbUser = service.GetById(id);

            // Assert
            Assert.AreSame(user, dbUser);
        }
    }
}
