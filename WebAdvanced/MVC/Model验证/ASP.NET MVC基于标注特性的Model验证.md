[ASP.NET MVC基于标注特性的Model验证：ValidationAttribute ](http://www.cnblogs.com/artech/archive/2012/06/06/data-annotations-model-validation-01.html)

##一、ValidationAttribute特性

与通过数据标注特性定义Model元数据类似，我们可以在作为Model的数据类型及其属性上应用相应的标注特性来定义Model验证规则。
所有的验证特性都直接或者间接继承自抽象类型System.ComponentModel.DataAnnotations.ValidationAttribute。
如下面的代码片断所示，ValidationAttribute具有一个字符串类型的ErrorMessage属性用于指定验证错误消息。
出于对本地化或者对错误消息单独维护的需要，我们可以采用资源文件的方式来保存错误消息，
在这种情况下我们只需要通过ErrorMessageResourceName和ErrorMessageResourceType这两个属性指定错误消息所在资源项的名称和类型即可。

``` C#
public abstract class ValidationAttribute : Attribute
{     
    public string ErrorMessage { get; set; }
    public string ErrorMessageResourceName { get; set; }
    public Type ErrorMessageResourceType { get; set; }
    protected string ErrorMessageString {get;}  
 
    public virtual string FormatErrorMessage(string name);
 
    public virtual bool IsValid(object value); 
    protected virtual ValidationResult IsValid(object value, ValidationContext validationContext)
  
    public void Validate(object value, string name);
    public ValidationResult GetValidationResult(object value, ValidationContext validationContext);
}
```


##二、验证消息的定义

如果我们通过ErrorMessage属性指定一个字符串作为验证错误消息，又通过ErrorMessageResourceName/ErrorMessageResourceType属性指定了错误消息资源项对应的名称和类型，后者具有更高的优先级。
ValidationAttribute具有一个受保护的只读属性ErrorMessageString用于返回最终的错误消息文本。

对于错误消息的定义，我们可以定义一个完整的消息，比如“年龄必需在18至25之间”。但是对于像资源文件这种对错误消息进行独立维护的情况，
为了让定义的资源文本能够最大限度地被重用，我们倾向于定义一个包含占位符的文本模板，比如“{DisplayName}必需在{LowerBound}和{UpperBound}之间”，
这样消息适用于所有基于数值范围的验证。对于后者，模板中的占位符可以在虚方法FormatErrorMessage中进行替换。该方法中的参数name实际上代表的是对应的显示名称，即对应ModelMetadata的DisplayName属性。

FormatErrorMessage方法在ValidationAttribute中的默认实现仅仅是简单地调用String的静态方法Format将参数name作为替换占位符的参数，
具体的定义如下。所以在默认的情况下，我们在定义错误消息模板的时候，只允许包含唯一一个针对显示名称的占位符“{0}”。
如果具有额外的占位符，或者不需要采用基于序号（“{0}”）的定义方法（比如采用类似于“{DisplayName}”这种基于文字的占位符更具可读性），只需要重写FormatErrorMessage方法即可。

``` C#
public abstract class ValidationAttribute : Attribute
{
    //其他成员
    public virtual string FormatErrorMessage(string name)
    {
        return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, new object[] { name });
    }
}
```

##四、预定义ValidationAttribute

* RequiredAttribute：用于验证必需数据字段。 
* RangeAttribute：用于验证数值字段的值是否在指定的范围之内。 
* StringLengthAttribute：用于验证目标字段的字符串长度是否在指定的范围之内。 
* MaxLengthAttribute/MinLengthAttribute：用于验证字符/数组字典的长度是否小于/大于指定的上/下限。 
* RegularExpressionAttribute：用于验证字符串字段的格式是否与指定的正则表达式相匹配。 
* CompareAttribute：用于验证目标字段的值是否与另一个字段值一致，在用户注册场景中可以用于确认两次输入密码的一致性。 
* CustomValidationAttribute：指定一个用于验证目标成员的验证类型和验证方法。 


[ASP.NET MVC基于标注特性的Model验证：将ValidationAttribute应用到参数上 ](http://www.cnblogs.com/artech/archive/2012/06/11/data-annotations-model-validation-04.html)


[ASP.NET MVC基于标注特性的Model验证：一个Model，多种验证规则 ](http://www.cnblogs.com/artech/archive/2012/06/12/data-annotations-model-validation-05.html)

