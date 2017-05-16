[ASP.NET MVC路由扩展：链接和URL的生成 ](http://www.cnblogs.com/artech/archive/2012/03/27/mvc-routing-02.html)

##一、UrlHelper V.S. HtmlHelper

``` C#
public class UrlHelper
{
    //其他成员
    public UrlHelper(RequestContext requestContext);
    public UrlHelper(RequestContext requestContext, RouteCollection routeCollection);
 
    public RequestContext RequestContext { get; }
    public RouteCollection RouteCollection { get;}
}
```

再来看看如下所示的HtmlHelper的定义，它同样具有一个表示路由对象集合的RouteCollection属性。
和UrlHelper一样，如果在构造函数没有显示指定，那么RouteTable的静态属性Routes表示的RouteCollection对象将会用于初始化该属性。

``` C#
public class HtmlHelper
{
    //其他成员
    public HtmlHelper(ViewContext viewContext, IViewDataContainer viewDataContainer);
    public HtmlHelper(ViewContext viewContext, IViewDataContainer viewDataContainer, RouteCollection routeCollection);
 
    public RouteCollection RouteCollection { get; }
    public ViewContext ViewContext { get; }
}
public class ViewContext : ControllerContext
{
    //省略成员
}
public class ControllerContext
{
    //其他成员   
    public RequestContext RequestContext { get; set; }
    public virtual RouteData RouteData { get; set; }
}
```


##二、UrlHelper.Action V.S. HtmlHelper.ActionLink

UrlHelper和HtmlHelper分别通过Action和ActionLink方法用于生成一个针对某个Controller的某个Action的URL和链接。

在System.Web.Mvc.Html.LinkExtensions中，我们为HtmlHelper定义了如下所示的一系列ActionLink方法重载。
顾名思义，ActionLink不再仅仅返回一个URL，而是生成一个链接（<a>...</a>），但是其中作为目标URL的生成逻辑和UriHelper是完全一致的。


##三、实例演示：创建一个RouteHelper模拟UrlHelper的URL生成逻辑



