using Microsoft.Owin;
using Owin;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebShopCommon.Dal;

[assembly: OwinStartupAttribute(typeof(WebShop.Startup))]
namespace WebShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            DbConfig.ConnectionString = ConfigurationManager.ConnectionStrings["MvcConnection"].ConnectionString;

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
    }
}
