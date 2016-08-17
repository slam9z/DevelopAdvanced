[ASP.NET MVC以ModelValidator为核心的Model验证体系: ModelValidatorProvider ](http://www.cnblogs.com/artech/archive/2012/06/02/model-validator-02.html)

##一、ModelValidatorProvider

我们通过注册ModelValidatorProvider来创建相应的ModelValidator，所有的ModelValidatorProvider直接或者间接地继承类型ModelValidatorProvider。
如下面的代码片断所示，ModelValidator的提供实现在抽象方法GetValidators种，返回的是一个ModelValidator集合。

``` C# 
public abstract class ModelValidatorProvider
{  
    public abstract IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ModelBindingExecutionContext context);
}
```

由于ValueProvider提供的数据值仅限于简单类型，所以针对复杂类型的Model绑定采用一个递归的过程对作为Model对象的所有属性进行绑定。
Model验证可以看成是Model绑定的后续环节，它对绑定的数据实施验证，所以Model验证也是一个递归的过程，它采用基于属性的验证规则对绑定的属性值实施验证。
GetValidators方法具有两个参数，类型ModelMetadata的metadata参数用于或者相应的验证规则，而参数context则是表示当前Model绑定上下文的ModelBindingExecutionContext对象。
