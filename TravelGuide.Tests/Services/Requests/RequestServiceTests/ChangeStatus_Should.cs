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
    public class ChangeStatus_Should
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ThrowArgumentNullException_WhenPassedIdIsNull(string id)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Act
            Assert.Throws<ArgumentNullException>(() => service.ChangeStatus(id));
        }

        [Test]
        public void ThrowInvalidOperationException_WhenNoSuchRequestIsFound()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();
            var request = new Request();

            var id = Guid.NewGuid().ToString();

            contextMock.Setup(x => x.Requests.Find(It.IsAny<Guid>())).Returns((Request)null);

            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Act
            Assert.Throws<InvalidOperationException>(() => service.ChangeStatus(id));
        }

        [Test]
        public void CorrectlyChangeStatusValue_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();
            var request = new Request();
            request.Status = "Awaiting confirmation";
            var initialStatus = request.Status;

            var id = Guid.NewGuid().ToString();

            contextMock.Setup(x => x.Requests.Find(It.IsAny<Guid>())).Returns(request);

            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Act
            service.ChangeStatus(id);

            // Assert
            Assert.AreEqual(initialStatus, "Awaiting confirmation");
            Assert.AreEqual(request.Status, "Confirmed!");
        }

        [Test]
        public void CallSaveChanges_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();
            var request = new Request();
            request.Status = "Awaiting confirmation";
            var initialStatus = request.Status;

            var id = Guid.NewGuid().ToString();

            contextMock.Setup(x => x.Requests.Find(It.IsAny<Guid>())).Returns(request);

            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Act
            service.ChangeStatus(id);

            // Assert
            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
