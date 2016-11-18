using ContosoUniversity.DAL;
using Microsoft.Owin;
using Owin;
using System.Data.Entity.Infrastructure.Interception;

[assembly: OwinStartupAttribute(typeof(ContosoUniversity.Startup))]
namespace ContosoUniversity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);


            DbInterception.Add(new SchoolInterceptorTransientErrors());
            DbInterception.Add(new SchoolInterceptorLogging());
        }
    }
}
