using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApplication2015.Startup))]
namespace WebApplication2015
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
