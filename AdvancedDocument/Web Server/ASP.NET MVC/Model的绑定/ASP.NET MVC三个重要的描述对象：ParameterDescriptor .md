[ASP.NET MVC三个重要的描述对象：ParameterDescriptor ](http://www.cnblogs.com/artech/archive/2012/05/11/parameter-descriptor.html)

Model绑定是为作为目标Action的方法准备参数列表的过程，所以针对参数的描述才是Model绑定的核心。
在ASP.NET MVC应用编程接口中，服务于Model绑定的参数元数据通过ParameterDescriptor类型来表示，而ActionDescriptor的GetParameters方法返回的就是一个ParameterDescriptor数组。

如下面的代码片断所示，ParameterDescriptor同样实现了ICustomAttributeProvider接口提供应用在相应参数上的特性。
ParameterDescriptor的只读属性ActionDescriptor表示描述所在Action方法的ActionDescriptor对象。属性ParameterName、ParameterType和DefaultValue分别表示参数的名称、类型和默认值。

``` C#
public abstract class ParameterDescriptor : ICustomAttributeProvider
{   
    public virtual object[] GetCustomAttributes(bool inherit);
    public virtual object[] GetCustomAttributes(Type attributeType, bool inherit);
    public virtual bool IsDefined(Type attributeType, bool inherit);
    
    public abstract ActionDescriptor ActionDescriptor { get; }
    public abstract string ParameterName { get; }
    public abstract Type ParameterType { get; }
    public virtual object DefaultValue { get; }
  
    public virtual ParameterBindingInfo BindingInfo { get; }
}
```

        
ParameterDescriptor的只读属性BindingInfo表示的System.Web.Mvc.ParameterBindingInfo对象封装一些信息用于控制请求数据与参数的绑定行为。
如下面的代码片断所示，抽象类ParameterBindingInfo具有四个属性，其中类型为IModelBinder的Binder属性返回的ModelBinder对象是整个Model绑定的核心，我们将在本章后续部分进行单独介绍。

``` C#
public abstract class ParameterBindingInfo
{
    public virtual IModelBinder Binder { get; }
    
    public virtual ICollection<string> Include { get; }
    public virtual ICollection<string> Exclude { get; }
    public virtual string Prefix { get; }
}
```

如果参数类型是一个复杂类型，默认情况下会绑定其所有公共可读写属性，而两个ICollection<string>类型的属性Include和Exclude表示显示设置的参与/不参与绑定的属性名称列表。
在默认情况下，请求数据与参数之间严格按照名称进行绑定，但是有时候请求数据名称具有相应的前缀，这个前缀体现在ParameterBindingInfo的Prefix属性上。
