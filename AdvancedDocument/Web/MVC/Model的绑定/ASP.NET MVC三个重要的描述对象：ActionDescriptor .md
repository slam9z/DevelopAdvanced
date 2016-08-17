[ASP.NET MVC三个重要的描述对象：ActionDescriptor ](http://www.cnblogs.com/artech/archive/2012/05/10/action-descriptor.html)

##一、ActionDescriptor

用于描述定义在Controller类中的Action方法的ActionDescriptor定义如下。属性ActionName和ControllerDescriptor
表示Action的名称和描述所在Controller的ControllerDescriptor对象。表示唯一标识的UniqueId属性由自身类型、Controller的类型与Action名称三者派生。

``` C#
public abstract class ActionDescriptor : ICustomAttributeProvider
{
    public virtual object[] GetCustomAttributes(bool inherit);
    public virtual object[] GetCustomAttributes(Type attributeType,  bool inherit);
    public virtual bool IsDefined(Type attributeType, bool inherit);
    public virtual IEnumerable<FilterAttribute> GetFilterAttributes( bool useCache);
    
    public abstract ParameterDescriptor[] GetParameters();
    public abstract object Execute(ControllerContext controllerContext,  IDictionary<string, object> parameters);
    public virtual ICollection<ActionSelector> GetSelectors();
    public virtual FilterInfo GetFilters();    
  
    public abstract string ActionName { get; }
    public abstract ControllerDescriptor ControllerDescriptor { get; }
    public virtual string UniqueId { get; }
}
```

与ControllerDescriptor一样，ActionDescriptor同样实现了定义在ICustomAttributeProvider接口中的方法，我们可以通过相应的方法得到应用在Action方法上的相关特性，
或者判断某个指定的特性是否应用在对应的Action方法上。GetFilterAttributes方法用于返回应用在Action方法上的所有筛选器特性。
用于描述Action方法中所有参数的ParameterDescriptor数组通过方法GetParameters返回。Action方法的执行可以直接通过调用方法Execute来完成，
该方法的两个参数controllerContext和parameters分别代表Action方法执行所在的Controller上下文和传入的参数。
