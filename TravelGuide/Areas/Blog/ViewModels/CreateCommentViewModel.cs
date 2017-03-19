using System.ComponentModel.DataAnnotations;

namespace TravelGuide.Areas.Blog.ViewModels
{
    public class CreateCommentViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Content { get; set; }
    }
}