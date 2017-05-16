[ASP.NET Web API标准的“管道式”设计 ](http://www.cnblogs.com/artech/p/asp-net-web-api-pipeline.html)

ASP.NET Web API的核心框架是一个消息处理管道，这个管道是一组HttpMessageHandler的有序组合。
这是一个双工管道，请求消息从一端流入并依次经过所有HttpMessageHandler的处理。
在另一端，目标HttpController被激活，Action方法被执行，响应消息随之被生成。响应消息逆向流入此管道，同样会经过逐个HttpMessageHandler的处理。
这是一个独立于寄宿环境的抽象管道，如何实现对请求的监听与接收，以及将接收的请求传入消息处理管道进行处理并将管道生成的响应通过网络回传给客户端，
这就是Web API寄宿需要解决的问题。

##一、HttpMessageHandler

这里的“消息处理”具有两个层面的含义，既包括针对请求消息的处理，还包括针对响应消息的处理。HttpMessageHandler直接或者间接继承自具有如下定义的抽象类型HttpMessageHandler，该类型定义在命名空间“System.Net.Http”下。ASP.NET Web API通过类型HttpRequestMessage和HttpResponseMessage来表示管道处理的请求消息和响应消息，所以对HttpMessageHandler的定义就很好理解了。

``` C#
public abstract class HttpMessageHandler : IDisposable
{
    public void Dispose();
    protected virtual void Dispose(bool disposing);
    protected abstract Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
}
```


##二、DelegatingHandler


··· C#
public abstract class DelegatingHandler : HttpMessageHandler
{  
    protected internal override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
    public HttpMessageHandler InnerHandler get;  set; }
}
```

InnerHandler是下一个handler

##三、HttpServer

作为这个HttpMessageHandler链“龙头”的是一个HttpServer对象，该类型定义在命名空间“System.Web.Http”下。

如下面的代码片断所示，HttpServer直接继承自DelegatingHandler。它具有两个重要的只读属性（Configuration和Dispatcher），
我们可以通过前者得到用于配置整个消息处理管道的HttpConfiguration对象，另外一个属性Dispatcher返回的是处于整个消息处理管道“尾端”的HttpMessageHandler。

``` C#
public class HttpServer : DelegatingHandler
{
    public HttpConfiguration     Configuration { get; }
    public HttpMessageHandler    Dispatcher { get; }
}
```

##四、HttpRoutingDispatcher

在默认情况下，作为消息处理管道“龙头”的HttpServer的Dispatcher属性返回一个HttpRoutingDispatcher对象，
它可以视为这个消息处理管道的最后一个HttpMessageHandler


##五、HttpControllerDispatcher

我们从类型命名可以看出HttpRoutingDispatcher具有两个基本的职能，即“路由（Routing）”和“消息分发（Dispatching）”。对于前者，它会调用当前路由表对请求消息实施路由解析进而生成用于封装路由数据的HttpRouteData（如果这样的HttpRouteData不存在于当前请求的属性字典中）。对于后者，它会将请求直接分发给在创建时指定的HttpMessageHandler来完成进一步处理。
