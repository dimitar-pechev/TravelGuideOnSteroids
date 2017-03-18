using TravelGuide.Data.Contracts;
using TravelGuide.Services.Factories;
using TravelGuide.Services.Gallery;

namespace TravelGuide.Tests.Services.Gallery.Mocks
{
    public class GalleryImageServiceMock : GalleryImageService
    {
        public GalleryImageServiceMock(ITravelGuideContext context, IGalleryImageFactory imageFactory, IGalleryLikeFactory likeFactory, IGalleryCommentFactory commentFactory)
            : base(context, imageFactory, likeFactory, commentFactory)
        {
        }

        public ITravelGuideContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IGalleryImageFactory ImageFactory
        {
            get
            {
                return this.imageFactory;
            }
        }

        public IGalleryLikeFactory LikeFactory
        {
            get
            {
                return this.likeFactory;
            }
        }

        public IGalleryCommentFactory CommentFactory
        {
            get
            {
                return this.commentFactory;
            }
        }
    }
}
