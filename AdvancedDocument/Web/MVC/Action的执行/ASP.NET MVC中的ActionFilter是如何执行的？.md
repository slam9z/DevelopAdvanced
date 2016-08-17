[ASP.NET MVC中的ActionFilter是如何执行的？ ](http://www.cnblogs.com/artech/archive/2012/08/06/action-filter.html)

在ASP.NET MVC中的四大筛选器（Filter），ActionFilter直接应用在某个Action方法上，它在目标Action方法执行前后对调用进行拦截以执行一些额外的操作。
这是一种典型的AOP式的设计，如果我们需要在执行某个Action方法的前后执行一些操作，可以通过定义ActionFilter来实现。
本篇文章主要讲述多一个应用到相同Action方法上的ActionFilter的执行机制

##一、ActionFilter

ActionFilter允许我们在目标Action方法执行前后对调用进行拦截以执行一些额外的操作，所有的ActionFilter实现了具有如下定义的接口IActionFilter。

``` C#
public interface IActionFilter
{    
    void OnActionExecuting(ActionExecutingContext filterContext);
    void OnActionExecuted(ActionExecutedContext filterContext);
}
 
public class ActionExecutingContext : ControllerContext
{    
    public ActionExecutingContext();
    public ActionExecutingContext(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary<string, object> actionParameters);
     
    public virtual ActionDescriptor            ActionDescriptor { get; set; }
    public virtual IDictionary<string, object> ActionParameters { get; set; }
    public ActionResult                        Result { get; set; }
}
  
public class ActionExecutedContext : ControllerContext
{    
    public ActionExecutedContext();   
    public ActionExecutedContext(ControllerContext controllerContext, ActionDescriptor actionDescriptor, bool canceled, Exception exception);
     
    public virtual ActionDescriptor     ActionDescriptor { get; set; }
    public virtual bool                 Canceled { get; set; }
    public virtual Exception            Exception { get; set; }
    public bool                         ExceptionHandled { get; set; }
    public ActionResult                 Result { get; set; }
}
```


##二、ActionFilter的执行机制


##三、ActionFilter对ActionResult的设置


##四、ActionFilter中的异常处理