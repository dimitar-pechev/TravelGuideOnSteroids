using System.ComponentModel.DataAnnotations;

namespace TravelGuide.ViewModels.ArticlesViewModels
{
    public class CreateArticleViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 20)]
        [Display(Name = "Article Main Content")]
        public string ContentMain { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 20)]
        [Display(Name = "Worth-Seeing Places")]
        public string ContentMustSee { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 20)]
        [Display(Name = "Budget Tips")]
        public string ContentBudgetTips { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 20)]
        [Display(Name = "Accomodation Tips")]
        public string ContentAccomodation { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Country { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string City { get; set; }

        [Required]
        [Url]
        [Display(Name = "Primary Image Url")]
        public string PrimaryImageUrl { get; set; }

        [Required]
        [Url]
        [Display(Name = "Second Image Url")]
        public string SecondImageUrl { get; set; }

        [Required]
        [Url]
        [Display(Name = "Third Image Url")]
        public string ThirdImageUrl { get; set; }

        [Required]
        [Url]
        [Display(Name = "Cover Image Url")]
        public string CoverImageUrl { get; set; }
    }
}