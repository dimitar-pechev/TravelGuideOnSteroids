using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TravelGuide.Common.Contracts;
using TravelGuide.Models;

namespace TravelGuide.Areas.Store.ViewModels
{
    public class CartViewModel : IMapFrom<User>
    {
        public IEnumerable<StoreItemCartViewModel> StoreItems { get; set; }

        public Guid Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(30, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(15, MinimumLength = 7)]
        public string PhoneNumber { get; set; }

        public string Total
        {
            get
            {
                if (this.StoreItems != null)
                {
                    return $"{this.StoreItems.Sum(x => x.Price)} BGN";
                }
                else
                {
                    return "0 BGN";
                }
            }
        }
    }
}