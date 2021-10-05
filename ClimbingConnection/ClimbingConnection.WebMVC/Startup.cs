using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClimbingConnection.WebMVC.Startup))]
namespace ClimbingConnection.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
