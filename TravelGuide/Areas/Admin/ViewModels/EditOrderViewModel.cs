using System;
using System.ComponentModel.DataAnnotations;
using TravelGuide.Common.Contracts;
using TravelGuide.Models.Requests;

namespace TravelGuide.Areas.Admin.ViewModels
{
    public class EditOrderViewModel : IMapFrom<Request>
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public bool BoolStatus { get; set; }
    }
}