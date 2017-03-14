using System.Data.Entity;
using TravelGuide.Models;

namespace TravelGuide.Data.Contracts
{
    public interface ITravelGuideContext
    {
        IDbSet<User> Users { get; set; }

        int SaveChanges();
    }
}
