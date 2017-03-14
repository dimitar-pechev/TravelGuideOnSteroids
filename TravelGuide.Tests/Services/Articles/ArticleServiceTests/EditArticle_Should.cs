using System;
using Moq;
using NUnit.Framework;
using TravelGuide.Data.Contracts;
using TravelGuide.Models.Articles;
using TravelGuide.Services.Articles;
using TravelGuide.Services.Factories;

namespace TravelGuide.Tests.Services.Articles.ArticleServiceTests
{
    [TestFixture]
    public class EditArticle_Should
    {
        [Test]
        public void ThrowInvalidOperationException_WhenNoSuchArticleIsFound()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            var article = new Article();
            article.Title = "name";
            var initialValue = article.Title;

            var id = Guid.NewGuid();
            var title = "some name";
            var city = "some name";
            var country = "some name";
            var contentMain = "some name";
            var contnentBudget = "some name";
            var contentAccomodation = "some name";
            var contentMustSee = "some name";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            contextMock.Setup(x => x.Articles.Find(It.IsAny<Guid>())).Returns((Article)null);

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.EditArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage));
        }

        [Test]
        public void CorrectlyAssignNewTitleValue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            var article = new Article();
            article.Title = "name";
            var initialValue = article.Title;

            var id = Guid.NewGuid();
            var title = "some name";
            var city = "some name";
            var country = "some name";
            var contentMain = "some name";
            var contnentBudget = "some name";
            var contentAccomodation = "some name";
            var contentMustSee = "some name";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            contextMock.Setup(x => x.Articles.Find(It.IsAny<Guid>())).Returns(article);

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act
            service.EditArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage);

            // Assert
            Assert.AreEqual(initialValue, "name");
            Assert.AreEqual(article.Title, "some name");
        }

        [Test]
        public void CorrectlyAssignNewCityValue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            var article = new Article();
            article.City = "name";
            var initialValue = article.City;

            var id = Guid.NewGuid();
            var title = "some name";
            var city = "some name";
            var country = "some name";
            var contentMain = "some name";
            var contnentBudget = "some name";
            var contentAccomodation = "some name";
            var contentMustSee = "some name";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            contextMock.Setup(x => x.Articles.Find(It.IsAny<Guid>())).Returns(article);

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act
            service.EditArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage);

            // Assert
            Assert.AreEqual(initialValue, "name");
            Assert.AreEqual(article.City, "some name");
        }

        [Test]
        public void CorrectlyAssignNewCountryValue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            var article = new Article();
            article.Country = "name";
            var initialValue = article.Country;

            var id = Guid.NewGuid();
            var title = "some name";
            var city = "some name";
            var country = "some name";
            var contentMain = "some name";
            var contnentBudget = "some name";
            var contentAccomodation = "some name";
            var contentMustSee = "some name";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            contextMock.Setup(x => x.Articles.Find(It.IsAny<Guid>())).Returns(article);

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act
            service.EditArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage);

            // Assert
            Assert.AreEqual(initialValue, "name");
            Assert.AreEqual(article.Country, "some name");
        }

        [Test]
        public void CorrectlyAssignNewContentMainValue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            var article = new Article();
            article.ContentMain = "name";
            var initialValue = article.ContentMain;

            var id = Guid.NewGuid();
            var title = "some name";
            var city = "some name";
            var country = "some name";
            var contentMain = "some name";
            var contnentBudget = "some name";
            var contentAccomodation = "some name";
            var contentMustSee = "some name";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            contextMock.Setup(x => x.Articles.Find(It.IsAny<Guid>())).Returns(article);

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act
            service.EditArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage);

            // Assert
            Assert.AreEqual(initialValue, "name");
            Assert.AreEqual(article.ContentMain, "some name");
        }

        [Test]
        public void CorrectlyAssignNewContentBudgetValue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            var article = new Article();
            article.ContentBudgetTips = "name";
            var initialValue = article.ContentBudgetTips;

            var id = Guid.NewGuid();
            var title = "some name";
            var city = "some name";
            var country = "some name";
            var contentMain = "some name";
            var contnentBudget = "some name";
            var contentAccomodation = "some name";
            var contentMustSee = "some name";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            contextMock.Setup(x => x.Articles.Find(It.IsAny<Guid>())).Returns(article);

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act
            service.EditArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage);

            // Assert
            Assert.AreEqual(initialValue, "name");
            Assert.AreEqual(article.ContentBudgetTips, "some name");
        }

        [Test]
        public void CorrectlyAssignNewContentAccomodationValue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            var article = new Article();
            article.ContentAccomodation = "name";
            var initialValue = article.ContentAccomodation;

            var id = Guid.NewGuid();
            var title = "some name";
            var city = "some name";
            var country = "some name";
            var contentMain = "some name";
            var contnentBudget = "some name";
            var contentAccomodation = "some name";
            var contentMustSee = "some name";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            contextMock.Setup(x => x.Articles.Find(It.IsAny<Guid>())).Returns(article);

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act
            service.EditArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage);

            // Assert
            Assert.AreEqual(initialValue, "name");
            Assert.AreEqual(article.ContentAccomodation, "some name");
        }

        [Test]
        public void CorrectlyAssignNewContentMustSeeValue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            var article = new Article();
            article.ContentMustSee = "name";
            var initialValue = article.ContentMustSee;

            var id = Guid.NewGuid();
            var title = "some name";
            var city = "some name";
            var country = "some name";
            var contentMain = "some name";
            var contnentBudget = "some name";
            var contentAccomodation = "some name";
            var contentMustSee = "some name";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            contextMock.Setup(x => x.Articles.Find(It.IsAny<Guid>())).Returns(article);

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act
            service.EditArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage);

            // Assert
            Assert.AreEqual(initialValue, "name");
            Assert.AreEqual(article.ContentMustSee, "some name");
        }

        [Test]
        public void CorrectlyAssignNewPrimaryImgValue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            var article = new Article();
            article.PrimaryImageUrl = "name";
            var initialValue = article.PrimaryImageUrl;

            var id = Guid.NewGuid();
            var title = "some name";
            var city = "some name";
            var country = "some name";
            var contentMain = "some name";
            var contnentBudget = "some name";
            var contentAccomodation = "some name";
            var contentMustSee = "some name";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            contextMock.Setup(x => x.Articles.Find(It.IsAny<Guid>())).Returns(article);

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act
            service.EditArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage);

            // Assert
            Assert.AreEqual(initialValue, "name");
            Assert.AreEqual(article.PrimaryImageUrl, "some name");
        }

        [Test]
        public void CorrectlyAssignNewSecondImgValue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            var article = new Article();
            article.SecondImageUrl = "name";
            var initialValue = article.SecondImageUrl;

            var id = Guid.NewGuid();
            var title = "some name";
            var city = "some name";
            var country = "some name";
            var contentMain = "some name";
            var contnentBudget = "some name";
            var contentAccomodation = "some name";
            var contentMustSee = "some name";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            contextMock.Setup(x => x.Articles.Find(It.IsAny<Guid>())).Returns(article);

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act
            service.EditArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage);

            // Assert
            Assert.AreEqual(initialValue, "name");
            Assert.AreEqual(article.SecondImageUrl, "some name");
        }

        [Test]
        public void CorrectlyAssignNewThirdImgValue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            var article = new Article();
            article.ThirdImageUrl = "name";
            var initialValue = article.ThirdImageUrl;

            var id = Guid.NewGuid();
            var title = "some name";
            var city = "some name";
            var country = "some name";
            var contentMain = "some name";
            var contnentBudget = "some name";
            var contentAccomodation = "some name";
            var contentMustSee = "some name";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            contextMock.Setup(x => x.Articles.Find(It.IsAny<Guid>())).Returns(article);

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act
            service.EditArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage);

            // Assert
            Assert.AreEqual(initialValue, "name");
            Assert.AreEqual(article.ThirdImageUrl, "some name");
        }

        [Test]
        public void CorrectlyAssignNewCoverImgValue_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            var article = new Article();
            article.CoverImageUrl = "name";
            var initialValue = article.CoverImageUrl;

            var id = Guid.NewGuid();
            var title = "some name";
            var city = "some name";
            var country = "some name";
            var contentMain = "some name";
            var contnentBudget = "some name";
            var contentAccomodation = "some name";
            var contentMustSee = "some name";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            contextMock.Setup(x => x.Articles.Find(It.IsAny<Guid>())).Returns(article);

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act
            service.EditArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage);

            // Assert
            Assert.AreEqual(initialValue, "name");
            Assert.AreEqual(article.CoverImageUrl, "some name");
        }

        [Test]
        public void CallSaveChanges_WhenInputParamsAreValid()
        {
            // Arrange
            var contextMock = new Mock<ITravelGuideContext>();
            var factoryMock = new Mock<IArticleFactory>();
            var commentFactoryMock = new Mock<IArticleCommentFactory>();

            var article = new Article();

            var id = Guid.NewGuid();
            var title = "some name";
            var city = "some name";
            var country = "some name";
            var contentMain = "some name";
            var contnentBudget = "some name";
            var contentAccomodation = "some name";
            var contentMustSee = "some name";
            var imageUrl = "some name";
            var secondImage = "some name";
            var thirdImage = "some name";
            var coverImage = "some name";

            contextMock.Setup(x => x.Articles.Find(It.IsAny<Guid>())).Returns(article);

            var service = new ArticleService(contextMock.Object, factoryMock.Object, commentFactoryMock.Object);

            // Act
            service.EditArticle(id, title, city, country, contentMain, contentMustSee, contnentBudget, contentAccomodation, imageUrl, secondImage, thirdImage, coverImage);

            // Assert
            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
