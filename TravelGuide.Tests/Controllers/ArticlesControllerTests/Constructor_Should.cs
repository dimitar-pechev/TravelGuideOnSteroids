using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Common.Contracts;
using TravelGuide.Controllers;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Articles.Contracts;
using TravelGuide.Services.Store.Contracts;
using TravelGuide.Tests.Controllers.ArticlesControllerTests.Mocks;

namespace TravelGuide.Tests.Controllers.ArticlesControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgument_WhenArticleServiceIsNull()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ArticlesController(null, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object));
        }

        [Test]
        public void ThrowArgument_WhenMappingServiceIsNull()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ArticlesController(articleServiceMock.Object, null, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object));
        }

        [Test]
        public void ThrowArgument_WhenStoreServiceIsNull()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, null, userServiceMock.Object, utilsMock.Object));
        }

        [Test]
        public void ThrowArgument_WhenUserServiceIsNull()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, null, utilsMock.Object));
        }

        [Test]
        public void ThrowArgument_WhenUtilitiesServiceIsNull()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, null));
        }

        [Test]
        public void ReturnCorrectInstance_WhenParamsAreValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act
            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.IsInstanceOf<ArticlesController>(controller);
        }

        [Test]
        public void CorrectlyAssignArticleService_WhenParamsAreValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act
            var controller = new ArticleControllerMock(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.AreSame(articleServiceMock.Object, controller.ArticleService);
        }

        [Test]
        public void CorrectlyAssignMappingService_WhenParamsAreValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act
            var controller = new ArticleControllerMock(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.AreSame(mappingServiceMock.Object, controller.MappingService);
        }

        [Test]
        public void CorrectlyAssignStoreService_WhenParamsAreValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act
            var controller = new ArticleControllerMock(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.AreSame(storeServiceMock.Object, controller.StoreService);
        }

        [Test]
        public void CorrectlyAssignUserService_WhenParamsAreValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act
            var controller = new ArticleControllerMock(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.AreSame(userServiceMock.Object, controller.UserService);
        }

        [Test]
        public void CorrectlyAssignUtilitiesService_WhenParamsAreValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            // Act
            var controller = new ArticleControllerMock(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.AreSame(utilsMock.Object, controller.Utils);
        }
    }
}
