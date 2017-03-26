using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TravelGuide.Common.Contracts;
using TravelGuide.Controllers;
using TravelGuide.Models.Articles;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Articles.Contracts;
using TravelGuide.Services.Store.Contracts;
using TravelGuide.ViewModels.ArticlesViewModels;

namespace TravelGuide.Tests.Controllers.ArticlesControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnDefaultViewWithCorrectModel_WhenParamsAreValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var articleMock = new Article();
            var mappedArticleMock = new ArticleItemViewModel();
            var modelMock = new ArticlesListViewModel();

            var articlesMock = new List<Article>() { articleMock };
            var mappedArticlesMock = new List<ArticleItemViewModel>() { mappedArticleMock };

            articleServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);
            articleServiceMock.Setup(x => x.GetFilteredArticlesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(articlesMock);
            mappingServiceMock.Setup(x => x.Map<IEnumerable<ArticleItemViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(mappedArticlesMock);
            mappingServiceMock.Setup(x => x.Map<ArticlesListViewModel>(It.IsAny<IEnumerable<ArticleItemViewModel>>())).Returns(modelMock);
            utilsMock.Setup(x => x.AssignViewParams<ArticlesListViewModel>(It.IsAny<ArticlesListViewModel>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(modelMock);

            // Act & Assert
            controller.WithCallTo(x => x.Index(null, null))
                .ShouldRenderDefaultView()
                .WithModel<ArticlesListViewModel>();
        }

        [Test]
        public void CallGetPagesCountMethod_WhenParamsAreValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var articleMock = new Article();
            var mappedArticleMock = new ArticleItemViewModel();
            var modelMock = new ArticlesListViewModel();

            var articlesMock = new List<Article>() { articleMock };
            var mappedArticlesMock = new List<ArticleItemViewModel>() { mappedArticleMock };

            articleServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);
            articleServiceMock.Setup(x => x.GetFilteredArticlesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(articlesMock);
            mappingServiceMock.Setup(x => x.Map<IEnumerable<ArticleItemViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(mappedArticlesMock);
            mappingServiceMock.Setup(x => x.Map<ArticlesListViewModel>(It.IsAny<IEnumerable<ArticleItemViewModel>>())).Returns(modelMock);
            utilsMock.Setup(x => x.AssignViewParams<ArticlesListViewModel>(It.IsAny<ArticlesListViewModel>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(modelMock);

            // Act
            controller.Index(null, null);

            // Assert
            articleServiceMock.Verify(x => x.GetPagesCount(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CallGetPageMethod_WhenParamsAreValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var articleMock = new Article();
            var mappedArticleMock = new ArticleItemViewModel();
            var modelMock = new ArticlesListViewModel();

            var articlesMock = new List<Article>() { articleMock };
            var mappedArticlesMock = new List<ArticleItemViewModel>() { mappedArticleMock };

            articleServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);
            articleServiceMock.Setup(x => x.GetFilteredArticlesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(articlesMock);
            mappingServiceMock.Setup(x => x.Map<IEnumerable<ArticleItemViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(mappedArticlesMock);
            mappingServiceMock.Setup(x => x.Map<ArticlesListViewModel>(It.IsAny<IEnumerable<ArticleItemViewModel>>())).Returns(modelMock);
            utilsMock.Setup(x => x.AssignViewParams<ArticlesListViewModel>(It.IsAny<ArticlesListViewModel>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(modelMock);

            // Act
            controller.Index(null, null);

            // Assert
            utilsMock.Verify(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void CallGetFilteredArticlesByPageMethod_WhenParamsAreValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var articleMock = new Article();
            var mappedArticleMock = new ArticleItemViewModel();
            var modelMock = new ArticlesListViewModel();

            var articlesMock = new List<Article>() { articleMock };
            var mappedArticlesMock = new List<ArticleItemViewModel>() { mappedArticleMock };

            articleServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);
            articleServiceMock.Setup(x => x.GetFilteredArticlesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(articlesMock);
            mappingServiceMock.Setup(x => x.Map<IEnumerable<ArticleItemViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(mappedArticlesMock);
            mappingServiceMock.Setup(x => x.Map<ArticlesListViewModel>(It.IsAny<IEnumerable<ArticleItemViewModel>>())).Returns(modelMock);
            utilsMock.Setup(x => x.AssignViewParams<ArticlesListViewModel>(It.IsAny<ArticlesListViewModel>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(modelMock);

            // Act
            controller.Index(null, null);

            // Assert
            articleServiceMock.Verify(x => x.GetFilteredArticlesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void CallMappingToArticleItemMethod_WhenParamsAreValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var articleMock = new Article();
            var mappedArticleMock = new ArticleItemViewModel();
            var modelMock = new ArticlesListViewModel();

            var articlesMock = new List<Article>() { articleMock };
            var mappedArticlesMock = new List<ArticleItemViewModel>() { mappedArticleMock };

            articleServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);
            articleServiceMock.Setup(x => x.GetFilteredArticlesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(articlesMock);
            mappingServiceMock.Setup(x => x.Map<IEnumerable<ArticleItemViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(mappedArticlesMock);
            mappingServiceMock.Setup(x => x.Map<ArticlesListViewModel>(It.IsAny<IEnumerable<ArticleItemViewModel>>())).Returns(modelMock);
            utilsMock.Setup(x => x.AssignViewParams<ArticlesListViewModel>(It.IsAny<ArticlesListViewModel>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(modelMock);

            // Act
            controller.Index(null, null);

            // Assert
            mappingServiceMock.Verify(x => x.Map<IEnumerable<ArticleItemViewModel>>(It.IsAny<IEnumerable<Article>>()), Times.Once);
        }

        [Test]
        public void CallMAppingToArticlesListMethod_WhenParamsAreValid()
        {
            // Arrange
            var articleServiceMock = new Mock<IArticleService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var storeServiceMock = new Mock<IStoreService>();
            var userServiceMock = new Mock<IUserService>();
            var utilsMock = new Mock<IUtilitiesService>();

            var controller = new ArticlesController(articleServiceMock.Object, mappingServiceMock.Object, storeServiceMock.Object, userServiceMock.Object, utilsMock.Object);

            var articleMock = new Article();
            var mappedArticleMock = new ArticleItemViewModel();
            var modelMock = new ArticlesListViewModel();

            var articlesMock = new List<Article>() { articleMock };
            var mappedArticlesMock = new List<ArticleItemViewModel>() { mappedArticleMock };

            articleServiceMock.Setup(x => x.GetPagesCount(It.IsAny<string>())).Returns(1);
            utilsMock.Setup(x => x.GetPage(It.IsAny<int?>(), It.IsAny<int>())).Returns(1);
            articleServiceMock.Setup(x => x.GetFilteredArticlesByPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(articlesMock);
            mappingServiceMock.Setup(x => x.Map<IEnumerable<ArticleItemViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(mappedArticlesMock);
            mappingServiceMock.Setup(x => x.Map<ArticlesListViewModel>(It.IsAny<IEnumerable<ArticleItemViewModel>>())).Returns(modelMock);
            utilsMock.Setup(x => x.AssignViewParams<ArticlesListViewModel>(It.IsAny<ArticlesListViewModel>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(modelMock);

            // Act
            controller.Index(null, null);

            // Assert
            mappingServiceMock.Verify(x => x.Map<ArticlesListViewModel>(It.IsAny<IEnumerable<ArticleItemViewModel>>()), Times.Once);
        }
    }
}