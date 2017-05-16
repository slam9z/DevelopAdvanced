[ASP.NET路由系统实现原理：HttpHandler的动态映射 ](http://www.cnblogs.com/artech/archive/2012/03/28/http-handler-mapping.html)

ASP.NET路由系统通过一个注册到当前应用的自定义HttpModule对所有的请求进行拦截，并通过对请求的分析为之动态匹配一个用于处理它的HttpHandler。
HttpHandler对请求进行处理后将相应的结果写入HTTP回复以实现对请求的相应。

![](http://images.cnblogs.com/cnblogs_com/artech/201203/201203280814472093.jpg)

##UrlRoutingModule 

``` C#
public class UrlRoutingModule : IHttpModule
{
    //其他成员
    public RouteCollection RouteCollection { get; set; }
    public void Init(HttpApplication context)
    {
        context.PostResolveRequestCache += new EventHandler(this.OnApplicationPostResolveRequestCache);
  
    }

//其他成员    
    private void OnApplicationPostResolveRequestCache(object sender, EventArgs e)
    { 
        HttpContext context = ((HttpApplication)sender).Context;
        HttpContextBase contextWrapper = new HttpContextWrapper(context);
        RouteData routeData = this.RouteCollection.GetRouteData(contextWrapper);
        RequestContext requestContext = new RequestContext(contextWrapper, routeData);
        IHttpHandler handler = routeData.RouteHandler.GetHttpHandler(requestContext);
        context.RemapHandler(handler);
    }

}
```


##二、 PageRouteHandler V.S. MvcRouteHandler


##三、 ASP.NET路由系统扩展

##实例演示：通过自定义Route对ASP.NET路由系统进行扩展


