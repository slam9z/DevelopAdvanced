using System.Web.Http;

namespace WebShop.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.EnableCors();

			//RESTful （英文：Representational State Transfer，简称REST） api  不匹配 action name。

			//config.Routes.MapHttpRoute(
			// name: "DefaultApi",
			// routeTemplate: "api/{controller}/{id}",
			// defaults: new { id = RouteParameter.Optional });

			config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}