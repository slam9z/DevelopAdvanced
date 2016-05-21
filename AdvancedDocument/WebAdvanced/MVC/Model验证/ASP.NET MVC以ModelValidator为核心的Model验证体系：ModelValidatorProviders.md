[ASP.NET MVC以ModelValidator为核心的Model验证体系: ModelValidatorProviders ](http://www.cnblogs.com/artech/archive/2012/06/03/model-validator-03.html)

##一、ModelValidatorProviders

我们通过静态类型ModelValidatorProviders对ModelValidatorProvider进行注册。
如下面的代码片断所示，ModelValidatorProviders具有一个静态只读属性Providers，其类型为ModelValidatorProviderCollection，
表示注册的基于整个Web应用范围的ModelValidatorProvider列表。

``` C#
public static class ModelValidatorProviders
{   
    public static ModelValidatorProviderCollection Providers { get; }
}
 
public class ModelValidatorProviderCollection : Collection<ModelValidatorProvider>
{   
    public ModelValidatorProviderCollection();
    public ModelValidatorProviderCollection(IList<ModelValidatorProvider> list);
     public IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context);   
}
```

值得一提的是，ModelValidatorProviderCollection定义了一个GetValidators方法用于返回一个通过集合中每个ModelValidatorProvider创建的ModelValidator集合。
在这个方法中，指定的Model元数据和Controller上下文会被传入每个ModelValidatorProvider对象的GetValidators方法，得到的每个ModelValidator对象将会作为最终返回的ModelValidator集合的元素。

在默认的情况下，通过ModelValidatorProviders的Providers表示注册的ModelValidatorProvider列表会包含三个对象，
对应着我们前面介绍的三种ModelValidatorProvider类型，即DataAnnotationsModelValidatorProvider、ClientDataTypeModelValidatorProvider和DataErrorInfoPropertyModelValidator。

如果我们需要添加一个自定义ModelValidatorProvider，可以直接将相应的对象添加到ModelValidatorProviders的Providers列表中。
如果需要采用自定义ModelValidatorProvider来替换掉现有的ModelValidatorProvider，比如我们创建了一个扩展的DataAnnotationsModelValidatorProvider，
还需要将现有的ModelValidatorProvider从该列表中移除。

##二、ModelValidator、ModelValidatorProvider和ModelValidatorProviders

![](http://images.cnblogs.com/cnblogs_com/artech/201206/201206030918436467.png)