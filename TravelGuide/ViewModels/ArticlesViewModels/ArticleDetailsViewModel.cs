using System;
using System.Collections.Generic;
using AutoMapper;
using TravelGuide.Common.Contracts;
using TravelGuide.Models;
using TravelGuide.Models.Articles;
using TravelGuide.Models.Store;
using TravelGuide.Shared;

namespace TravelGuide.ViewModels.ArticlesViewModels
{
    public class ArticleDetailsViewModel : IMapFrom<Article>, IMapFrom<IEnumerable<StoreItem>>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public string Title { get; set; }

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

        public ICollection<ArticleLike> Likes { get; set; }

        public ICollection<ArticleComment> Comments { get; set; }

        public IEnumerable<StoreItem> StoreItems { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<IEnumerable<StoreItem>, ArticleDetailsViewModel>()
                .ForMember(dest => dest.StoreItems, s => s.MapFrom(src => src));
        }
    }
}