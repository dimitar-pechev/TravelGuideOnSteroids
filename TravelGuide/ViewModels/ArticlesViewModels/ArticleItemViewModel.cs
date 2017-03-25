using System;
using TravelGuide.Common.Contracts;
using TravelGuide.Models.Articles;

namespace TravelGuide.ViewModels.ArticlesViewModels
{
    public class ArticleItemViewModel : IMapFrom<Article>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ContentMain { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string PrimaryImageUrl { get; set; }
    }
}