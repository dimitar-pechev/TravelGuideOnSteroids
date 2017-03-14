using System;

namespace TravelGuide.Models.Abstractions
{
    public abstract class Like
    {
        public Like()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string UserId { get; set; }
    }
}
