using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SportLife.Website.Startup))]
namespace SportLife.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
