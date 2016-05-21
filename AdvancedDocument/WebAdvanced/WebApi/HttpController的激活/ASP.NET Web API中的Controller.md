[ASP.NET Web API中的Controller ](http://www.cnblogs.com/artech/p/httpcontroller.html)

``` C#
public interface IHttpController
{
    Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken);
}
````

##一、HttpControllerContext

``` C#
public class HttpControllerContext
{
    public HttpControllerContext();
    public HttpControllerContext(HttpConfiguration configuration, IHttpRouteData routeData, HttpRequestMessage request);   
    public HttpConfiguration         Configuration { get; set; }
    public IHttpRouteData            RouteData { get; set; }
    public HttpRequestMessage        Request { get; set; }
    public IHttpController              Controller { get; set; }
    public HttpControllerDescriptor     ControllerDescriptor { get; set; }
}
```

##二、HttpControllerDescriptor

``` C#
 public class HttpControllerDescriptor
 {   
     public HttpControllerDescriptor();
     public HttpControllerDescriptor(HttpConfiguration configuration, string controllerName, Type controllerType);
  
     public virtual IHttpController CreateController(HttpRequestMessage request);
  
     public virtual Collection<T> GetCustomAttributes<T>() where T: class;
     public virtual Collection<T> GetCustomAttributes<T>(bool inherit) where T: class;
    public virtual Collection<IFilter> GetFilters();
   
    public HttpConfiguration     Configuration { get; set; }
    public string                ControllerName { get; set; }
    public Type                  ControllerType { get; set; }
 
    public virtual ConcurrentDictionary<object, object> Properties { get; }
}
```


##三、ApiController

ApiController是不能“重用”的，用于处理每一个请求的ApiController都应该是“全新”的。