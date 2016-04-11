[ASP.NET MVC的Model元数据与Model模板：模板的获取与执行策略 ](http://www.cnblogs.com/artech/archive/2012/05/03/model-metadata-and-template-02.html)

##一、 DataTypeAttribute和模板有何关系？

实际上在模板匹配的过程中会将ModelMetadata的DataTypeName属性当作模板名称来看待，所以下面两种形式的Model类型定义可以看成是等效的。
通过UIHintAttribute特性设置的模板名称和通过DataTypeAttribute特性设置的数据类型的唯一不同之处在于*前者具有更高的优先级*。



##二、模板的获取与执行

当我们调用HtmlHelper或者HtmlHelper<TModel>的模板方法对整个Model或者Model的某个数据成员以某种模式（显示模式或者编辑模式）进行呈现的时候，
通过预先创建的代表Model元数据的ModelMetadata对象都可以找到相应的模板。如果模板对应着某个自定义的分部View，
那么只需要执行该View即可；对于默认模板，则直接可以得到相应的HTML。

根据Model元数据对目标模板的解析是整个模板方法执行流程中最核心的部分，也是本篇讨论的重点。
我们以针对HtmlHelper<TModel>的扩展方法DisplayFor为例，看看针对通过表达式expression获取的Model对象是如何以显示模式呈现出来的。

``` C#
public static class DisplayExtensions
{
    public static MvcHtmlString DisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName);
}
```

在DisplayFor被调用的时候，如果通过参数expression表示的Model获取表达式是针对某个属性的，那么属性名会被获取出来。
然后执行表达式得到一个作为Model的对象，该对象连同属性名（如果有）一起被用于表示Model元数据的Metadatadata对象。
接下来会根据该Metadatadata对象得到一系列表示分部模板View名称的列表，这些View名称按照优先级排列如下：

* 作为参数templateName传入的模板名称（如果不为空）。 
* Metadatadata的TemplateHint属性值（如果不为空）。 
* Metadatadata的DataTypeName属性值（如果不为空）。 
* 如果Model对象的真实类型为非空值类型，该类型名作为模板View名；否则底层（Underlying）类型名作为模板View名（比如说，对于int?类型则将Int32作为模板View名）。 
* 如果Model对象的真实类型为非复杂类型，则使用String模板（由于非复杂类型能够实现与String类型之间的转换，所以可以转换成String进行呈现）。 
* 在Model的声明类型为接口情况下，如果该接口继承自IEnuerable则采用Collection模板。 
* 在Model的声明类型为接口情况下，使用Object模板。 
* 如果Model声明类型不是接口类型，按照其类型继承关系向上追溯知道Object类型，逐个将类型名称作为模板View名称。如果声明类型实现了IEnuerable接口，则将最后的Object替换成Collection。 


