[ASP.NET MVC以ValueProvider为核心的值提供系统: ValueProviderFactory ](http://www.cnblogs.com/artech/archive/2012/05/19/value-provider-03.html)

在ASP.NET Model绑定系统中，用于提供数据值的ValueProvider对象通过ValueProviderFactory来创建。
在ASP.NET MVC应用编程接口中，ValueProviderFactory继承自ValueProviderFactory类。
本篇文章只要介绍基于ValueProviderFactory的ValueProvider的提供机制，以及如何通过自定义ValueProviderFactory实现我们需要的数据值的绑定方式。


##一、ValueProviderFactory

如下面的代码片断所示，ValueProviderFactory是一个抽象类，唯一的抽象方法GetValueProvider用于实现基于指定Controller上下文的ValueProvider创建。

``` C#
public abstract class ValueProviderFactory
{
    public abstract IValueProvider GetValueProvider(ControllerContext controllerContext);
}
```

下面的列表列出了定义在Model绑定系统中的6个原生的ValueProviderFactory：

* ChildActionValueProviderFactory：根据给定的Controller上下文创建一个ChildActionValueProvider对象。 
* FormValueProviderFactory：根据给定的Controller上下文创建一个FormValueProvider对象。 
* JsonValueProviderFactory：将以JSON形式表示的请求数据转换成一个Dictionary<string, object>对象，并最终创建一个DictionaryValueProvider<object>对象。 
* RouteDataValueProviderFactory：根据给定的Controller上下文创建一个RouteDataValueProvider对象。 
* QueryStringValueProviderFactory：根据给定的Controller上下文创建一个QueryStringValueProvider对象。 
* HttpFileCollectionValueProviderFactory：根据给定的Controller上下文创建一个HttpFileCollectionValueProvider对象。 

##二、ValueProviderFactory的注册

ValueProviderFactory在ASP.NET MVC应用中的注册通过静态类型ValueProviderFactories实现。
如下面的代码片断所示，ValueProviderFactories具有一个静态只读属性Factories返回一个表示ValueProviderFactory集合的ValueProviderFactoryCollection类型。


``` C#
public static class ValueProviderFactories
{
    public static ValueProviderFactoryCollection Factories { get; }
}
  
public class ValueProviderFactoryCollection : Collection<ValueProviderFactory>
{ 
    public ValueProviderFactoryCollection();
    public ValueProviderFactoryCollection(IList<ValueProviderFactory> list);    
    public IValueProvider GetValueProvider(ControllerContext controllerContext);
}
```

ValueProviderFactoryCollection的GetValueProvider方法返回的是一个ValueProviderCollection对象，集合中的每个ValueProvider通过对应的ValueProviderFactory来创建
。ValueProviderFactory在ValueProviderFactoryCollection集合中的先后次序决定了创建的ValueProvider在ValueProviderCollection中的次序，而次序决定了使用优先级。

在默认的情况下ValueProviderFactories的Factories属性表示的ValueProviderFactoryCollection包含了上面我们介绍的6种ValueProviderFactory，
次序（优先级）为：ChildActionValueProviderFactory、FormValueProviderFactory、JsonValueProviderFactory、RouteDataValueProviderFactory、QueryStringValueProviderFactory和。
如果具有相同的名称的请求书去同时存在于请求表单和查询字符串中，前者会被选用。

以ValueProvider为核心的值提供系统中涉及到了三类组件/类型，即用于具体实现数据值提供的ValueProvider，ValueProvider通过ValueProviderFactotry，
而ValueProviderFactotry通过ValueProviderFactotries进行注册。图5-4所示的UML体现了三者之间的关系。

![](http://images.cnblogs.com/cnblogs_com/artech/201205/201205190719469390.png)

##三、实例演示：创建一个自定义ValueProviderFactory
