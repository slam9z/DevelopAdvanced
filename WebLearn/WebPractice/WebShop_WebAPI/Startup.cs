using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using WebShop.App_Start;
using System.Web.Mvc;
using WebShopCommon.Dal;
using System.Configuration;

[assembly: OwinStartup(typeof(WebShop.Startup))]

namespace WebShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();

            // Configure web api
            //var config = new HttpConfiguration();
            //WebApiConfig.Register(config);

            //config.EnsureInitialized();


            DbConfig.ConnectionString=ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            //FilterConfig.RegisterGlobalFilters(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configuration.EnsureInitialized();

        }
    }
}
