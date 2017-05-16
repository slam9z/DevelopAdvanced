[ASP.NET MVC以ModelValidator为核心的Model验证体系: ModelValidator ](http://www.cnblogs.com/artech/archive/2012/06/01/model-validator-01.html)

ASP.NET MVC的整个Model验证系统以组件ModelValidator为核心，或者说Model对象的验证最终通过某个ModelValidator对象来完成，
所以我们有必要先来认识一下ModelValidator以及背后的提供机制。


##一、ModelValidator

在ASP.NET MVC应用编程接口中，所有的ModelValidator都直接或者间接地继承自抽象类型ModelValidator。
如下面的代码片断所示，ModelValidator具有一个布尔类型的只读属性IsRequired，表示该ModelValidator是否是对目标数据进行必要性的验证，默认返回False。
GetClientValidationRules返回一个元素类型为ModelClientValidationRule的集合。ModelClientValidationRule是对客户端验证规则的封装，
我们会在进行客户端验证时对其进行详细介绍。

``` C#
public abstract class ModelValidator
{
    //其他成员    
    public virtual IEnumerable<ModelClientValidationRule> GetClientValidationRules();
    public abstract IEnumerable<ModelValidationResult> Validate(object container);
    
    public virtual bool IsRequired { get; }
}
```

真正对目标数据实施验证是通过调用Validate方法来完成的，而该方法的输入参数container表示的正式被验证的对象。
该Validate返回一个表示验证结果的元素类型为ModelValidationResult的集合，该类型的定义如下所示。

``` C#
public class ModelValidationResult
{  
    public ModelValidationResult();
  
    public string MemberName { get; set; }
    public string Message { get; set; }
}
```

ModelValidationResult具有两个字符串类型的属性MemberName和Message，前者代表被验证数据成员的名称，后者表示错误消息。

##二、DataAnnotationsModelValidator


##三、ClientModelValidator



##四、DataErrorInfoModelValidator

在System.ComponentModel命名空间下定义了一个名为IDataErrorInfo的接口，该接口提供了一种标准的错误信息定制方式。如下面的代码片断所示，IDataErrorInfo具有两个成员，只读属性Error用于获取基于自身的错误消息，而只读索引用于返回指定数据成员的错误消息。

``` C#
public interface IDataErrorInfo
{
    string Error { get; }
    string this[string columnName] { get; }
}
```

##五、ValidatableObjectAdapter


