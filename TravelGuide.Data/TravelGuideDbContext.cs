using Microsoft.AspNet.Identity.EntityFramework;
using TravelGuide.Models;

namespace TravelGuide.Data
{
    public class TravelGuideDbContext : IdentityDbContext<User>
    {
        public TravelGuideDbContext()
            : base("TravelGuideMvc")
        {
        }

        public static TravelGuideDbContext Create()
        {
            return new TravelGuideDbContext();
        }
    }
}
