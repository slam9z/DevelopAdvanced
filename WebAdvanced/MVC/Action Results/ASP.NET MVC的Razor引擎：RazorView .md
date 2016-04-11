##一、BuildManagerCompiledView

``` C#
public abstract class BuildManagerCompiledView : IView
{   
    internal IViewPageActivator ViewPageActivator;
  
    protected BuildManagerCompiledView(ControllerContext controllerContext, string viewPath);
    protected BuildManagerCompiledView(ControllerContext controllerContext, string viewPath, IViewPageActivator viewPageActivator);
    internal BuildManagerCompiledView(ControllerContext controllerContext, string viewPath, IViewPageActivator viewPageActivator, IDependencyResolver dependencyResolver);
   
    public void Render(ViewContext viewContext, TextWriter writer);
    protected abstract void RenderView(ViewContext viewContext, TextWriter writer, object instance);
   
    internal IBuildManager     BuildManager { get; set; }
    public string              ViewPath { get; protected set; }
}
```

通过《View编译原理》的介绍我们知道采用Razor引擎的View文件（.cshtml或者.vbhtml）最终都会编译成一个WebViewPage类型，
所以通过RazorView/WebFormView体现的View的呈现机制最终体现在对WebViewPage对象的激活。我们可以利用BuildManager根据View文件的虚拟路径得到编译后的类型。从名称也可以看出来，BuildManagerCompiledView内部就是利用了BuildManager根据指定的View文件虚拟路径完成对WebViewPage对象激活。

BuildManagerCompiledView的属性ViewPath表示的就是View文件的虚拟路径，该属性在构造函数中被初始化。BuildManagerCompiledView具有三个构造函数
，对象本身的构造逻辑体现在内部构造函数上。如上面的代码片断所示，除了将当前ControllerContext和View文件虚拟路径作为构造函数的参数之外，
该构造函数还具有额外两个参数，其类型分别是IViewPageActivator和IDependencyResolver。

``` C#
public interface IViewPageActivator
{    
    object Create(ControllerContext controllerContext, Type type);
}
```


##二、RazorView

RazorView通过实现RenderView方法最终完成了对View的呈现。方法传入参数instance是通过BuildManagerCompiledView激活的View对象，
通过上面的介绍我们知道这是一个空的WebViewPage<TModel>对象（默认情况下是通过默认构造函数创建的）。
RazorView在RenderView方法中对其进行初始后调用ExecutePageHierarchy方法将整个页面内容呈现出来。
RazorView实现RenderView方法的逻辑基本上可以通过如下的代码片断来表示。



