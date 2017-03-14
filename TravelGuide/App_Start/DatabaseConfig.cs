using System.Data.Entity;
using TravelGuide.Data;
using TravelGuide.Data.Migrations;

namespace TravelGuide.App_Start
{
    public class DatabaseConfig
    {
        public static void InitializeDatabase()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TravelGuideContext, Configuration>());
        }
    }
}