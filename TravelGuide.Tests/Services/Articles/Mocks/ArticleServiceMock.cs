using TravelGuide.Data.Contracts;
using TravelGuide.Services.Articles;
using TravelGuide.Services.Factories;

namespace TravelGuide.Tests.Services.Articles.Mocks
{
    public class ArticleServiceMock : ArticleService
    {
        public ArticleServiceMock(ITravelGuideContext context, IArticleFactory articleFactory, IArticleCommentFactory commentFactory)
            : base(context, articleFactory, commentFactory)
        {
        }

        public ITravelGuideContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IArticleFactory ArticleFactory
        {
            get
            {
                return this.articleFactory;
            }
        }

        public IArticleCommentFactory CommentFactory
        {
            get
            {
                return this.commentFactory;
            }
        }
    }
}
