using System;
using System.Collections.Generic;

namespace TravelGuide.Models.Articles
{
    public class Article
    {
        public Article()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.Now;
            this.Comments = new HashSet<ArticleComment>();
            this.Likes = new HashSet<ArticleLike>();
            this.IsDeleted = false;
        }

        public Article(User user, string userId, string title, string city, string country, string contentMain, string contentMustSee, string contentBudgetTips, string contentAccomodation, string primaryImageUrl, string secondImageUrl, string thirdImageUrl, string coverImageUrl)
            : this()
        {
            this.User = user;
            this.UserId = userId;
            this.Title = title;
            this.City = city;
            this.Country = country;
            this.ContentMain = contentMain;
            this.ContentMustSee = contentMustSee;
            this.ContentBudgetTips = contentBudgetTips;
            this.ContentAccomodation = contentAccomodation;
            this.PrimaryImageUrl = primaryImageUrl;
            this.SecondImageUrl = secondImageUrl;
            this.ThirdImageUrl = thirdImageUrl;
            this.CoverImageUrl = coverImageUrl;
        }

        public Guid Id { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ContentMain { get; set; }

        public string ContentMustSee { get; set; }

        public string ContentBudgetTips { get; set; }

        public string ContentAccomodation { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string PrimaryImageUrl { get; set; }

        public string SecondImageUrl { get; set; }

        public string ThirdImageUrl { get; set; }

        public string CoverImageUrl { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<ArticleLike> Likes { get; set; }

        public virtual ICollection<ArticleComment> Comments { get; set; }
    }
}
