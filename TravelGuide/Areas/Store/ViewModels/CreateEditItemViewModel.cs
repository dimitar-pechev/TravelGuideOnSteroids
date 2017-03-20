using System;
using System.ComponentModel.DataAnnotations;
using TravelGuide.Common.Contracts;
using TravelGuide.Models.Store;

namespace TravelGuide.Areas.Store.ViewModels
{
    public class CreateEditItemViewModel : IMapFrom<StoreItem>
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        [StringLength(50, MinimumLength = 3)]
        public string ItemName { get; set; }

        [Required]
        [Display(Name = "Related Destination")]
        [StringLength(50, MinimumLength = 3)]
        public string DestinationFor { get; set; }

        [Required]
        [Display(Name = "Brand")]
        [StringLength(50, MinimumLength = 3)]
        public string Brand { get; set; }

        [Required]
        [Display(Name = "Image Url")]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [Range(0.1, 100000)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 10)]
        public string Description { get; set; }
    }
}