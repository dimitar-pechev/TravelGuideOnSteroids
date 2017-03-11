using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TravelGuide.Startup))]
namespace TravelGuide
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
