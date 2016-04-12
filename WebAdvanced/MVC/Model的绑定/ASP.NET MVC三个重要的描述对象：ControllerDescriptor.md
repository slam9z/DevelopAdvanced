[ASP.NET MVC三个重要的描述对象：ControllerDescriptor ](http://www.cnblogs.com/artech/archive/2012/05/10/controller-descriptor.html)

ASP.NET MVC应用的请求都是针对某个Controller的某个Action方法，所以对请求的处理最终体现在对目标Action方法的执行。而Action方法具有相应的参数，
所以在方法执行之前必须根据相应的规则从请求中提取相应的数据并将其转换为Action方法参数列表，我们将这个过程称为Model绑定。在ASP.NET MVC应用编程接口中，
Action方法某个参数的元数据通过ParameterDescriptor表示，而两个相关的类型ControllerDescriptor和ActionDescriptor则用于描述Controller和Action方法。

##一、ControllerDescriptor

ControllerDescriptor包含了用于描述某个Controller的元数据信息。如下面的代码片断所示，ControllerDescriptor具有三个属性，其中ControllerName和ControllerType分别表示Controller的名称和类型，前者来源于路由信息；字符串类型的UniqueId表示ControllerDescriptor的唯一标识，该标识由自身的类型、Controller的类型以及Controller的名称三者派生。

``` C#
public abstract class ControllerDescriptor : ICustomAttributeProvider
{   
     public virtual object[] GetCustomAttributes(bool inherit);
     public virtual object[] GetCustomAttributes(Type attributeType, bool inherit);
     public virtual bool IsDefined(Type attributeType, bool inherit);
     public virtual IEnumerable<FilterAttribute> GetFilterAttributes(bool useCache);
 
     public abstract ActionDescriptor FindAction(ControllerContext controllerContext, string actionName);
     public abstract ActionDescriptor[] GetCanonicalActions();
   
    public virtual string ControllerName { get; }
    public abstract Type ControllerType { get; }
    public virtual string UniqueId { get; }
}
  
public interface ICustomAttributeProvider
{
    object[] GetCustomAttributes(bool inherit);
    object[] GetCustomAttributes(Type attributeType, bool inherit);
    bool IsDefined(Type attributeType, bool inherit);
}
```


##二、ReflectedControllerDescriptor

