[深入探讨ASP.NET MVC的筛选器 ](http://www.cnblogs.com/artech/archive/2012/07/02/filter.html)

##一、Filter

虽然ASP.NET MVC提供的四种类型的筛选器具有各自实现的接口，但是对于筛选器的提供体系来说所有的筛选器都通过具有如下定义的Filter类型表示。
Filter的核心是Instance属性，因为它代表真正实施筛选功能的对象，该对象实现了一个或者多个基于上述四种筛选器类型的接口。

``` C#
public class Filter
{    
    public const int DefaultOrder = -1;   
    public Filter(object instance, FilterScope scope, int? order);
    
    public object Instance { get; protected set; }
    public int Order { get; protected set; }
    public FilterScope Scope { get; protected set; }
}
  
public enum FilterScope
{
    Action        = 30,
    Controller    = 20,
    First         = 0,
    Global        = 10,
    Last          = 100
}
```

*注：由于System.Web.Mvc.Filter和实现了IAuthorizationFilter、IActionFilter、IResultFilter和IExceptionFilter的类型均可以被称为“筛选器”，
为了不至于造成混淆，在没有做明确说明的情况下，我们使用英文“Filter”和中文“筛选器”分别来表示它们。*



##二、FilterProvider


##三、FilterAttribute与FilterAttributeFilterProvider

##四、Controller与ControllerInstanceFilterProvider

##五、GlobalFilterCollection

