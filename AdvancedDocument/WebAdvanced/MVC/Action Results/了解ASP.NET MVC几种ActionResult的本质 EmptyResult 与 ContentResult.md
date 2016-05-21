##一、ActionResult对请求的响应

``` C#

public abstract class ActionResult
{    
    //其他成员
    public abstract void ExecuteResult(ControllerContext context);
}

```

##二、EmptyResult

上面我们谈到Action方法返回的ActionResult对象被ActionInvoker调用以实现对当前请求的响应，其实这种说法不够准确。
不论Action方法是否具有返回值，也不论它的返回值是什么类型，ActionInvoker最终都会创建相应的ActionResult对象。
如果Action方法返回类型为void，或者返回值为Null，最终生成的就是一个EmptyResult对象。


##三、ContentResult

ContentResult使ASP.NET MVC按照我们指定的内容对请求予以响应。如下面的代码片断所示，我们可以利用ContentResult的Content
属性以字符串的形式指定响应的内容，另外两个属性ContentEncoding和ContentType则用于指定字符编码方式和媒体类型（MIME类型）。


*那么如果返回值不是一个ActionResult对象，ActionInvoker最终会创建怎样一个ActionResult对象呢？*

实际上对于一个非Null的返回值，ActionInvoker采用这样的方式来创建相应的ActionResult：如果返回对象是一个ActionResult，
直接返回该对象，否则将对象转换成字符串并以此创建一个ContentResult对象。ControllerActionInvoker根据Action方法的返回指生成相应
ActionResult的逻辑体现在如下一个受保护的虚方法CreateActionResult中，最后一个参数（actionReturnValue）表示Action方法的返回值。
而另一个受保护的InvokeActionMethod负责执行Action方法并返回响应的ActionResult对象，该方法在执行Action方法得到返回值后通过调用
CreateActionResult方法返回相应的ActionResult对象。

``` C#
public class ControllerActionInvoker : IActionInvoker
{
    //其他成员
    protected virtual ActionResult InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters);
    protected virtual ActionResult CreateActionResult(ControllerContext controllerContext, ActionDescriptor actionDescriptor, object actionReturnValue);
}
```

##四、实例演示：执行返回类型为非ActionResult的Action方法得到的ActionResult对象