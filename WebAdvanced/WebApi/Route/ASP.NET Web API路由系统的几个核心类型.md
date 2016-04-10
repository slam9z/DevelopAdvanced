[ASP.NET Web API路由系统的几个核心类型 ](http://www.cnblogs.com/artech/p/asp-net-web-api-routing-01.html)

##一、HttpRequestMessage与HttpResponseMessage


##二、HttpRouteData

当我们调用某个Route的GetRouteData的时候，如果指定的HTTP上下文具有一个与自身URL模板相匹配，同时满足定义的所有约束条件的情况下会返回一个RouteData对象。
ASP.NET的路由系统通过RouteData对象来封装解析出来的路由数据，其核心自然是通过Values和DataTokens属性封装的路由变量。

``` C#
public interface IHttpRouteData
{
    IHttpRoute                      Route { get; }
    IDictionary<string, object>     Values { get; }
}
```

在ASP.NET Web API路由系统中唯一实现了IHttpRouteData接口的公有类型为HttpRouteData

##三、HttpVirtualPathData


``` C#
public interface IHttpVirtualPathData
{
    IHttpRoute     Route { get; }
    string         VirtualPath { get; set; }
}

```


##四、HttpRouteConstraint


一个Route能够与HTTP请求相匹配，必须同时满足两个条件：其一，请求的URL必须与Route自身的URL的模式相匹配；其二，当前请求必须通过定义在当前Route上的所有约束。ASP.NET Web API路由系统通过HttpRouteContraint表示路由约束，具体类型实现了具有如下定义的接口IHttpRouteConstraint。

``` C#
public interface IHttpRouteConstraint
{
    bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection);
}
```


##五、HttpRoute

``` C#
public interface IHttpRoute
{
string                          RouteTemplate { get; }
IDictionary<string, object>     Constraints { get; }
IDictionary<string, object>     Defaults { get; }
IDictionary<string, object>     DataTokens { get; }
HttpMessageHandler              Handler { get; }
 
IHttpRouteData GetRouteData(string virtualPathRoot, HttpRequestMessage request);
    IHttpVirtualPathData GetVirtualPath(HttpRequestMessage request, IDictionary<string, object> values);
}

```

##六、HttpRouteCollection

故名思义HttpRouteCollection就是一个元素类型为IHttpRoute的集合，如下面的代码片断所示，
它实现了接口ICollection<IHttpRoute>。ASP.NET Web API路由系统中的路由表实际上就是一个HttpRouteCollection对象。HttpRouteCollection


*它先判断虚拟根路径是否已经被添加到表示请求的HttpRequestMessage的属性字典（Properties属性）中，对应的Key为“MS_VirtualPathRoot”，如果这样的属性存在并且是一个字符串,
那么这将直接被用作调用HttpRoute的GetRouteData方法的参数。否则直接使用通过属性VirtualPathRoot表示的默认根路径。*



##七、注册路由映射

    
``` C#
public class HttpConfiguration : IDisposable
{
    //其他成员
    public HttpRouteCollection                      Routes { get; }
    public string                                   VirtualPathRoot { get; }
    public ConcurrentDictionary<object, object>     Properties { get; }
}
```


##八、缺省路由变量

我们在进行路由注册的时候可以为某个路由变量设置一个默认值，这个默认值可以是一个具体的变量值，
也可以是通过RouteParameter具有如下定义的静态只读字段Optional返回的一个RouteParameter对象，
我们具有这种默认值的路由变量成为缺省路由变量。

``` C#
public sealed class RouteParameter
{
    public static readonly RouteParameter Optional;
}
```
