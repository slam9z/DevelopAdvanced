[总体介绍ASP.NET Web API下Controller的激活与释放流程 ](http://www.cnblogs.com/artech/p/asp-net-web-api-active-release-controller.html)

##一、HttpController激活流程

 对于组成ASP.NET Web API核心框架的消息处理管道来说，处于末端的HttpMessageHandler是一个HttpRoutingDispatcher对象。
当它完成路由解析工作之后（HttpRoutingDispatcher的路由解析只发生在Self Host寄宿模式下，对于Web Host寄宿模式来说，
路由解析工作是由ASP.NET路由系统来完成的），在默认情况下它会将请求传递给一个HttpControllerDispatcher对象。 

##二、HttpController的释放

``` C#
public static class HttpRequestMessageExtensions
{
    //其他成员
    public static IEnumerable<IDisposable> GetResourcesForDisposal(this HttpRequestMessage request);
    
    public static void RegisterForDispose(this HttpRequestMessage request, IEnumerable<IDisposable> resources);
    public static void RegisterForDispose(this HttpRequestMessage request,IDisposable resource);
    
    public static void DisposeRequestResources(this HttpRequestMessage request);
}
```

ASP.NET Web API还为释放这些附加到HttpRequestMessage上的对象定义了如上一个扩展方法DisposeRequestResources，那么这个方法究竟是在什么时候被调用的呢？

释放这些资源的时机取决于采用的寄宿模式。对于Web Host来说，ASP.NET Web API用于“处理请求、回复响应”的HttpMessageHandler管道是由HttpControllerHandler创建的，
后者根据当前HTTP上下文创建一个表示当前请求的HttpRequestMessage对象并传入这个管道进行处理。
在整个管道完成对请求的处理并最终对请求予以响应之后，HttpControllerHandler会负责完成如下三项与资源释放有关的工作。

* 调用HttpRequestMessage对象的扩展方法DisposeRequestResources释放附加在自身属性字典中的对象。 
* 调用HttpRequestMessage对象的Dispose方法对请求消息本身作相应的释放工作。 
* 调用返回的HttpResponseMessage对象对响应消息作相应的释放工作。
