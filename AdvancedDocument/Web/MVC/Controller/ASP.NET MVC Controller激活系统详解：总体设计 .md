##一、Controller

```C#
public interface IController
{
    void Execute(RequestContext requestContext);
}
```

定义在IController接口中的Execute是以同步的方式执行的。为了支持以异步方式对请求的处理，IController接口的异步版本System.Web.Mvc.IAsyncController被定义出来。如下面的代码片断所示，实现了IAsyncController接口的异步Controller的执行通过BeginExecute/EndExecute方法组合来完成。

``` C#
public interface IAsyncController : IController
{
    IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state);
    void EndExecute(IAsyncResult asyncResult);
}
```

抽象类ControllerBase实现了IController接口，它具有如下几个重要的属性。TemplateData、ViewBag和ViewData用于存储从Controller向View传递的数据或者变量。
其中TemplateData和ViewData具有基于字典的数据结构，Key和Value分别表示变量的名称和值，所不同的前者用于存储基于当前HTTP上下文的变量（在完成当前请求后，存储的数据会被回收）。
ViewBag和ViewData具有相同的作用，甚至对应着相同的数据存储，它们之间的不同之处在于前者是一个动态对象，我们可以为其指定任意属性。

```  C#
public abstract class ControllerBase : IController
{   
    //其他成员
    public ControllerContext ControllerContext { get; set; }
    public TempDataDictionary TempData { get; set; }
    public object ViewBag { [return: Dynamic] get; }
    public ViewDataDictionary ViewData { get; set; }
}
```



##二、 ControllerFactory

``` C#
public interface IControllerFactory
{
    IController CreateController(RequestContext requestContext, string controllerName);
    SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName);
    void ReleaseController(IController controller);
}
public enum SessionStateBehavior
{
    Default,
     Required,
     ReadOnly,
     Disabled
}

```


##三、ControllerBuilder

``` C#
public class ControllerBuilder
{
    public IControllerFactory GetControllerFactory();
    public void SetControllerFactory(Type controllerFactoryType);
    public void SetControllerFactory(IControllerFactory controllerFactory);  
     
    public HashSet<string> DefaultNamespaces { get; }
    public static ControllerBuilder Current { get; }
}
```


###实例演示：如何提升命名空间的优先级

