[ASP.NET MVC的View是如何被呈现出来的？[设计篇] ](http://www.cnblogs.com/artech/archive/2012/08/22/view-engine-01.html)

##一、View引擎中的View

ASP.NET MVC为我们提供了两种View引擎，它们针对不同的动态View设计方式。一种是传统的Web Form引擎，由于该引擎下View的设计与我们定义.aspx页面一致，
又称为ASPX引擎。另外一种则是本书默认采用同时也是推荐使用的Razor引擎。在两种View引擎的工作机制之前，有一个必须要知道的问题：
View如何表示？提到View，很多ASP.NET MVC的开发人员可能首先想到的就是定义UI界面的.aspx文件（Web Form引擎）或者.cshtml/.vbhtml文件（Razor引擎）。
其实对于View引擎来说，View是一个实现了IView接口类型的对象。如下面的代码片断所示，IView的定义非常简单，仅仅具有唯一的Render方法根据指定的View上下文
和TextWriter对象实现对View的呈现。

``` C#
public interface IView
{    
    void Render(ViewContext viewContext, TextWriter writer);
}
 
public class ViewContext : ControllerContext
{
    //其他成员
    public virtual bool ClientValidationEnabled { get; set; }
     public virtual bool UnobtrusiveJavaScriptEnabled { get; set; }
  
     public virtual TempDataDictionary TempData { get; set; }    
     [Dynamic]
     public object                     ViewBag { [return: Dynamic] get; }
     public virtual ViewDataDictionary ViewData { get; set; }
     public virtual IView              View { get; set; }
     public virtual TextWriter         Writer { get; set; }
}
  
public abstract class HttpResponseBase
{
    //其他成员
    public virtual TextWriter Output { get; set; }
}
```


##二、ViewEngine

View引擎的核心是一个ViewEngine对象，它实现了IViewEngine接口。如下面的代码片断所示，IViewEngine定义了两个FindView和FindPartialView方法
根据指定的Controller上下文、View名称和布局文件名称去获取对应的View和Partial View，两个方法中具有一个布尔类型的参数useCache表示是否启用缓存。
另一个方法ReleaseView用于释放View对象。

``` C#
public interface IViewEngine
{    
    ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache);
    ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache);
    void ReleaseView(ControllerContext controllerContext, IView view);
}
```

FindView和FindPartialView方法返回的并不是实现了IView接口的类型的对象，而是一个类型为System.Web.Mvc.ViewEngineResult对象。
如下面的代码片断所示，ViewEngineResult的只读属性View和ViewEngine属性表示找到的View对象和表示自身的ViewEngine对象。
在成功获取到对应View的情况下这两个属性会通过构造函数进行初始化。如果没有找到相应的View，则将一个搜寻位置列表传入另一个构造函数
创建一个ViewEngineResult，而只读属性SearchedLocations表示的就是这么一个搜寻位置列表。

``` C#
public class ViewEngineResult
{    
    public ViewEngineResult(IEnumerable<string> searchedLocations);
    public ViewEngineResult(IView view, IViewEngine viewEngine);
    
    public IEnumerable<string> SearchedLocations { get; }
    public IView               View { get; }
    public IViewEngine         ViewEngine { get; }
}
```

在上面实例演示中涉及到了一个重要的静态类型ViewEngines，它通过如下定义的只读属性Engines维护一个全局ViewEngine列表。
从给出的定义可以看出，两个原生的ViewEngine在初始化的时候就被添加到了该列表中，它们的类型就是分别代表Web Form和Razor引擎
的WebFormViewEngine和RazorViewEngine如果我们创建了一个自定义View引擎，相应的ViewEngine也可以通过ViewEngines进行注册。



##三、ViewResult的执行

View引擎对View的获取以及对View的呈现最初是通过ViewResult触发的，那么两者是如何衔接的呢？这是本小节着重讨论的问题，
在这之前我们不妨先来看看ViewResult的定义。如下面的代码片断所示，表示ViewResult的类型ViewResult是抽象类ViewResultBase的子类。

``` C#
public class ViewResult : ViewResultBase
{    
    protected override ViewEngineResult FindView(ControllerContext context);
    public string MasterName { get; set; }
}
 
public abstract class ViewResultBase : ActionResult
{   
    public override void ExecuteResult(ControllerContext context);
     protected abstract ViewEngineResult FindView(ControllerContext context);
   
     public object                     Model { get; }
     public TempDataDictionary         TempData { get; set; }    
     [Dynamic]
     public object                     ViewBag { [return: Dynamic] get; }
     public ViewDataDictionary         ViewData { get; set; }   
     public string                     ViewName { get; set; }
     public ViewEngineCollection       ViewEngineCollection { get; set; }
     public IView                      View { get; set; }
}
```

ViewResultBase的只读属性Model表示作为View的Model对象，三个表示数据状态的属性（ViewData、ViewBag和TempData）来源于Controller的同名属性。
View和ViewName属性则是代表具体的View对象和View的名称。ViewEngineCollection属性值默认来源于ViewEngines的静态属性Engines代表的全局ViewEngine列表。

