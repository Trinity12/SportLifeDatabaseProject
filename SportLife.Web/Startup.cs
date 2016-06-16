using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SportLife.Web.Startup))]
namespace SportLife.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
