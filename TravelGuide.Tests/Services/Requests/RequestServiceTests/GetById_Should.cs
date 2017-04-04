using System;
using FakeDbSet;
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
    public class GetById_Should
    {
        [Test]
        public void ReturnCorrectRequest_WhenParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IRequestFactory>();

            var id = Guid.NewGuid();
            var targetRequest = new Request() { Id = id };

            contextMock.Setup(x => x.Requests.Find(id)).Returns(targetRequest);

            var service = new RequestService(contextMock.Object, userServiceMock.Object, factoryMock.Object);

            // Act
            var request = service.GetById(id);

            // Assert
            Assert.AreSame(targetRequest, request);
        }
    }
}
