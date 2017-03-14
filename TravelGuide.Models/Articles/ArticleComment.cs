using System;
using TravelGuide.Models.Abstractions;

namespace TravelGuide.Models.Articles
{
    public class ArticleComment : Comment
    {
        public ArticleComment()
            : base()
        {
        }

        public ArticleComment(string userId, User user, string content, Guid articleId)
            : base()
        {
            this.User = user;
            this.UserId = userId;
            this.Content = content;
            this.ArticleId = articleId;
        }

        public Guid ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
