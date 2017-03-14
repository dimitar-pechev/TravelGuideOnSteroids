using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Data.Contracts;
using TravelGuide.Models;
using TravelGuide.Models.Articles;
using TravelGuide.Services.Articles;
using TravelGuide.Services.Factories;

namespace TravelGuide.Tests.Services.Articles.ArticleServiceTests
{
    [TestFixture]
    public class CreateArticle_Should
    {
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ThrowArgumentNullException_WhenPassedUsernameIsNull(string id)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();
            var title = "some name";
            var city = "some name";
            var country = "some name";
            var contentMain = "some content";
            var contnentBudget = "some content";
            var contentAccomodation = "some content";
            var contentMustSee = "some content";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => service.CreateArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ThrowArgumentNullException_WhenPassedTtileIsNull(string title)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();
            var id = "some name";
            var city = "some name";
            var country = "some name";
            var contentMain = "some content";
            var contnentBudget = "some content";
            var contentAccomodation = "some content";
            var contentMustSee = "some content";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => service.CreateArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ThrowArgumentNullException_WhenPassedCityIsNull(string city)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();
            var id = "some name";
            var title = "some name";
            var country = "some name";
            var contentMain = "some content";
            var contnentBudget = "some content";
            var contentAccomodation = "some content";
            var contentMustSee = "some content";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => service.CreateArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ThrowArgumentNullException_WhenPassedCountryIsNull(string country)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();
            var id = "some name";
            var title = "some name";
            var city = "some name";
            var contentMain = "some content";
            var contnentBudget = "some content";
            var contentAccomodation = "some content";
            var contentMustSee = "some content";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => service.CreateArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ThrowArgumentNullException_WhenPassedContentMainIsNull(string contentMain)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();
            var id = "some name";
            var title = "some name";
            var city = "some name";
            var country = "some content";
            var contnentBudget = "some content";
            var contentAccomodation = "some content";
            var contentMustSee = "some content";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => service.CreateArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ThrowArgumentNullException_WhenPassedContentBudgetIsNull(string contnentBudget)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();
            var id = "some name";
            var title = "some name";
            var city = "some name";
            var country = "some content";
            var contentMain = "some content";
            var contentAccomodation = "some content";
            var contentMustSee = "some content";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => service.CreateArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ThrowArgumentNullException_WhenPassedContentAccomodationIsNull(string contentAccomodation)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();
            var id = "some name";
            var title = "some name";
            var city = "some name";
            var country = "some content";
            var contentMain = "some content";
            var contnentBudget = "some content";
            var contentMustSee = "some content";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => service.CreateArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ThrowArgumentNullException_WhenPassedContentMustSeeIsNull(string contentMustSee)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();
            var id = "some name";
            var title = "some name";
            var city = "some name";
            var country = "some content";
            var contentMain = "some content";
            var contnentBudget = "some content";
            var contentAccomodation = "some content";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => service.CreateArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ThrowArgumentNullException_WhenPassedPrimaryImgIsNull(string imageUrl)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();
            var id = "some name";
            var title = "some name";
            var city = "some name";
            var country = "some content";
            var contentMain = "some content";
            var contnentBudget = "some content";
            var contentAccomodation = "some content";
            var contentMustSee = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => service.CreateArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ThrowArgumentNullException_WhenPassedSecondImgIsNull(string secondImage)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();
            var id = "some name";
            var title = "some name";
            var city = "some name";
            var country = "some content";
            var contentMain = "some content";
            var contnentBudget = "some content";
            var contentAccomodation = "some content";
            var contentMustSee = "some name";
            var imageUrl = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => service.CreateArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ThrowArgumentNullException_WhenPassedThirdImgIsNull(string thirdImage)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();
            var id = "some name";
            var title = "some name";
            var city = "some name";
            var country = "some content";
            var contentMain = "some content";
            var contnentBudget = "some content";
            var contentAccomodation = "some content";
            var contentMustSee = "some name";
            var imageUrl = "some name";
            var secondImage = "some name";
            var coverImage = "some name";

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => service.CreateArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ThrowArgumentNullException_WhenPassedCoverImgIsNull(string coverImage)
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();
            var id = "some name";
            var title = "some name";
            var city = "some name";
            var country = "some content";
            var contentMain = "some content";
            var contnentBudget = "some content";
            var contentAccomodation = "some content";
            var contentMustSee = "some name";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => service.CreateArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage));
        }

        [Test]
        public void ThrowInvalidOperationException_WhenNoSuchUserIsFound()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();
            var id = "some name";
            var title = "some name";
            var city = "some name";
            var country = "some content";
            var contentMain = "some content";
            var contnentBudget = "some content";
            var contentAccomodation = "some content";
            var contentMustSee = "some name";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            string coverImage = "some name";

            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns((User)null);
            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => service.CreateArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage));
        }

        [Test]
        public void CallSaveChanges_WhenInputDataIsValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();
            var id = "some name";
            var title = "some name";
            var city = "some name";
            var country = "some content";
            var contentMain = "some content";
            var contnentBudget = "some content";
            var contentAccomodation = "some content";
            var contentMustSee = "some name";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            string coverImage = "some name";

            contextMock.Setup(x => x.Articles.Add(It.IsAny<Article>()));
            contextMock.Setup(x => x.Users.Find(It.IsAny<string>())).Returns(new User());
            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act
            service.CreateArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage);

            // Assert
            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
