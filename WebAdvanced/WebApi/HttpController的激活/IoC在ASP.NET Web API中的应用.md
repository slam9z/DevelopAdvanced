##一、 基于IoC的HttpControllerActivator

将IoC应用于HttpController激活系统的目的在于让一个预定义的IoC容器来提供最终的HttpController对象。通过《ASP.NET Web API的Controller是如何被创建的？》的介绍我们知道HttpController的激活最终由HttpControllerActivator对象来完成，所以将IoC与ASP.NET Web API的HttpController激活系统进行集成最为直接的方式莫过于自定义一个HttpControllerActivator。

我们通过一个简单实例来演示如何通过自定义HttpControllerActivator的方式实现与IoC的集成，我们采用的IoC框架是Unity。我们在一个ASP.NET Web API应用中定义了这个UnityHttpControllerActivator类型。UnityHttpControllerActivator具有一个表示Unity容器的属性UnityContainer，该属性在构造函数中被初始化。在用于创建的HttpController的Create方法中，我们调用此UnityContainer对象的Resolve方法创建目标HttpController对象。


``` C#
public class UnityHttpControllerActivator : IHttpControllerActivator
{
    public IUnityContainer UnityContainer { get; private set; }
 
    public UnityHttpControllerActivator(IUnityContainer unityContainer)
    {        
        this.UnityContainer = unityContainer;
    }
 
   public  IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
   {
       return (IHttpController)this.UnityContainer.Resolve(controllerType);
   }
}
```

##二、基于IoC的DependencyResolver

由于默认的DefaultHttpControllerActivator会先利用当前注册的DependencyResolver对象去激活目标HttpController，
所以除了利用自定义的HttpControllerActivator将IoC引入HttpController激活系统之外，另一个有效的方案就是注册自定义的DependencyResolver。
