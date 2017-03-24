using System;
using System.ComponentModel.DataAnnotations;
using TravelGuide.Common.Contracts;
using TravelGuide.Models.Stories;

namespace TravelGuide.Areas.Blog.ViewModels
{
    public class CreateEditStoryViewModel : IMapFrom<Story>
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 100)]
        public string Content { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Related Destination")]
        public string RelatedDestination { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }
    }
}