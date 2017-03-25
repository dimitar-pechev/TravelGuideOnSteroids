using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TravelGuide.Models;
using TravelGuide.Models.Abstractions.Contracts;

namespace TravelGuide.Common.Contracts
{
    public interface ICommentable
    {
        Guid Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3)]
        [Display(Name = "Content")]
        string NewCommentContent { get; set; }

        User CurrentUser { get; set; }
       
        IEnumerable<IComment> Comments { get; set; }

        int ProfilePicSize { get; set; }
    }
}
