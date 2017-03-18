using System.ComponentModel.DataAnnotations;

namespace TravelGuide.Areas.Blog.ViewModels
{
    public class CreateImageViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }
    }
}