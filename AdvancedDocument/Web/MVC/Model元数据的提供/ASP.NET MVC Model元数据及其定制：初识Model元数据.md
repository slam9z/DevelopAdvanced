[ASP.NET MVC Model元数据及其定制: 初识Model元数据 ](http://www.cnblogs.com/artech/archive/2012/04/11/model-metadata-01.html)

ASP.NET MVC中的Model实际上View Model，表示最终绑定到View上的数据，而Model元数据描述了Model的数据结构，以及Model的每个数据成员的一些特性。
正是有了Model元数据的存在，才使模板化HTML的呈现机制成为可能。此外，Model元数据支撑了ASP.NET MVC的Model验证体系，
因为针对Model的验证规则正是定义在Model元数据中。ASP.NET MVC的Model元数据通过类型ModelMetadata表示。


##一、Model元数据层次化结构

![](http://images.cnblogs.com/cnblogs_com/artech/201204/201204110732032956.png)

``` C#
public class ModelMetadata
{
    //其他成员
    public virtual IEnumerable<ModelMetadata> Properties { get; }
}
```


##二、基本Model元数据信息

基于作为Model类型创建的元数据主要是为View实现模板化HTML呈现和数据验证服务的，我们可以通过在类型和数据成员上应用相应的特性控制Model在View中的呈现方式或者定义相应的验证规则。
在介绍声明式Model元数据编程方式之前，我们先来介绍表示Model元数据的ModelMetadata类型中与UI呈现和数据验证无关的基本属性。

``` C#
public class ModelMetadata
{
   //其他成员
    public Type ModelType { get; }
    public virtual bool IsComplexType { get; }
    public bool IsNullableValueType { get; }
    public Type ContainerType { get; }
 
    public object Model { get; set; }
    public string PropertyName { get; }
  
    public virtual Dictionary<string, object> AdditionalValues { get; }
    protected ModelMetadataProvider Provider { get; set; }
}
```


我们可以通过TypeConverterAttribute特性标注一个支持从字符串类型转换的TypeConverter使之转变成非复杂类型。

