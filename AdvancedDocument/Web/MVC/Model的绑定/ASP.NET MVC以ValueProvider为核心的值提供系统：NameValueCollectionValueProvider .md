[ASP.NET MVC以ValueProvider为核心的值提供系统: NameValueCollectionValueProvider ](http://www.cnblogs.com/artech/archive/2012/05/17/value-provider-01.html)

在进行Model绑定过程中，需要根据基于Action方法参数的绑定上下文从请求数据中提取相应的数据以提供相应的数据。
具体来说，Model绑定的数据具有多个来源，可能来源于Post的表单或者JSON字符串，或者来源于当前的路由数据，也可能来源于请求地址的插叙字符串。
ASP.NET MVC将这种基于不同数据来源的数据获取/提供机制实现在一个叫做ValueProvider的组件中。

##一、IValueProvider与ValueProviderResult

一般来讲，一个ValueProvider采用的数据源是一个字典类型的数据结构，我们通过它从这个字典中获取一个Key与当前绑定上下文匹配的值。
ValueProvider实现了具有如下定义的接口IValueProvider，GetValue方法根据指定的Key从数据源中获取对应的值对象，这个Key是基于当前绑定上下文的。
这个Key和存在于数据源中对应数据条目的Key可能并非完全一致，后者可能在前者基础上添加相应的前缀，而ContainsPrefix方法用于判断数据源字典的Key是否具有指定的前缀。

``` c#
public interface IValueProvider
{
    bool ContainsPrefix(string prefix);
    ValueProviderResult GetValue(string key);
}
```

IValueProvider的GetValue返回的是一个ValueProviderResult对象，我们可以将ValueProviderResult看成是对ValueProvider提供对象的封装。
如下面的代码片断所示，ValueProviderResult具有三个只读属性，其中RawValue表示原始的值对象。而AttemptedValue表示以值对象的字符串表示，该属性主要用于显示。

``` C#
[Serializable]
public class ValueProviderResult
{    
    public ValueProviderResult(object rawValue, string attemptedValue, CultureInfo culture);    
    public object ConvertTo(Type type);
    public virtual object ConvertTo(Type type, CultureInfo culture);
 
    public string AttemptedValue { get; }
    public CultureInfo Culture { get; }
    public object RawValue { get; }
}
```

ValueProviderResult提供了两个ConvertTo方法重载以实现向指定目标类型的转换。某些类型的格式化行为依赖于相应的语言文化（比如时间、日期和货币等），
而这个辅助格式湖的语言文化信息通过Culture属性表示。其中第一个ValueProviderResult方法重载通过属性Culture表示的语言文化进行类型转化。


##二、NameValueCollectionValueProvider

##三、两种前缀形式


##五、FormValueProvider与QueryStringValueProvider

