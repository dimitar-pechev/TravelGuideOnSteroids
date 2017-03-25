using System;

namespace TravelGuide.Models.Abstractions.Contracts
{
    public interface IComment
    {
        Guid Id { get; set; }

        DateTime CreatedOn { get; set; }

        User User { get; set; }

        string UserId { get; set; }

        string Content { get; set; }
    }
}
