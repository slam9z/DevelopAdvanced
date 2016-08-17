[ASP.NET MVC Model元数据及其定制: Model元数据的定制 ](http://www.cnblogs.com/artech/archive/2012/04/12/model-metadata-02.html)

Model元数据的定制是通过在作为Model的数据类型极其属性成员上应用相应的特性来实现，
这些用于声明式元数据定义的特性大都定义在System.ComponentModel.DataAnnotations.dll程序集中，
程序集的名称同时也是对应的命名空间名称，所以我们可以它们为数据注解特性（Data Annotation Attribute），
接下来我们来介绍一些常用的数据注解特性，以及它们对于元数据具有怎样的影响


##一、UIHintAttribute


##二、HiddenInputAttribute与ScaffoldColumnAttribute

HtmlHelper<TModel>的扩展方法EditForModel方法将一个具体的Model对象（new Model { Foo = "foo", Bar = "bar", Baz = "baz" }）
显示在某个基于Model类型的强类型View中。


##三、DataTypeAttribute与DisplayFormatAttribute

##四、EditableAttribute与ReadOnlyAttribute


##五、DisplayAttribute与DisplayNameAttribute


##六、RequiredAttribute

