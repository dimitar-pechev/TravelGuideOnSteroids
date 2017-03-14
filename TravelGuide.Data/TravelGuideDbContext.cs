using Microsoft.AspNet.Identity.EntityFramework;
using TravelGuide.Data.Contracts;
using TravelGuide.Models;

namespace TravelGuide.Data
{
    public class TravelGuideContext : IdentityDbContext<User>, ITravelGuideContext
    {
        public TravelGuideContext()
            : base("TravelGuideMvc")
        {
        }

        public static TravelGuideContext Create()
        {
            return new TravelGuideContext();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
