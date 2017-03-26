using TravelGuide.Common.Contracts;
using TravelGuide.Controllers;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Articles.Contracts;
using TravelGuide.Services.Store.Contracts;

namespace TravelGuide.Tests.Controllers.ArticlesControllerTests.Mocks
{
    public class ArticleControllerMock : ArticlesController
    {
        public ArticleControllerMock(IArticleService articleService, IMappingService mappingService, IStoreService storeService, IUserService userService, IUtilitiesService utils)
            : base(articleService, mappingService, storeService, userService, utils)
        {
        }

        public IArticleService ArticleService
        {
            get
            {
                return this.articleService;
            }
        }

        public IMappingService MappingService
        {
            get
            {
                return this.mappingService;
            }
        }

        public IStoreService StoreService
        {
            get
            {
                return this.storeService;
            }
        }

        public IUtilitiesService Utils
        {
            get
            {
                return this.utils;
            }
        }

        public IUserService UserService
        {
            get
            {
                return this.userService;
            }
        }
    }
}
