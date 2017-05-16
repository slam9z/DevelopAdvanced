[how to handle “OPTIONS Method” in ASP.NET MVC](http://stackoverflow.com/questions/7001846/how-to-handle-options-method-in-asp-net-mvc)

##answer

Turns out I had to create an ActionFilterAttribute

```cs
namespace WebService.Attributes
{
    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();

            filterContext.RequestContext.HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");

            string rqstMethod = HttpContext.Current.Request.Headers["Access-Control-Request-Method"];
            if (rqstMethod == "OPTIONS" || rqstMethod == "POST")
            {
                filterContext.RequestContext.HttpContext.Response.AppendHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
                filterContext.RequestContext.HttpContext.Response.AppendHeader("Access-Control-Allow-Headers", "X-Requested-With, Accept, Access-Control-Allow-Origin, Content-Type");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
```


##answer

```xml
<httpProtocol>
  <customHeaders>
    <add name="Access-Control-Allow-Headers" value="Content-Type, Accept, X-Requested-With"/>
    <add name="Access-Control-Allow-Methods" value="GET, POST, OPTIONS"/>
    <add name="Access-Control-Allow-Origin" value="*"/>
  </customHeaders>
</httpProtocol>
```

good approach, except that it's global. The accepted answer can be applied on a "per controller"
basis. – Chase Florell Jan 22 '13 at 22:27 