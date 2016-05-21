[ASP.NET MVC的Model元数据与Model模板：预定义模板 ](http://www.cnblogs.com/artech/archive/2012/05/02/model-metadata-and-template-01.html)

```C#
HtmlHelper<TModel>.EditorForModel
```

##一、 实例演示：通过模板将布尔值显示为RadioButton


##二、预定义模板

上面我们介绍如何通过View的方式创建模板进而控制某种数据类型或者某个目标元素最终在UI界面上的HTML呈现方式，
实际上在ASP.NET MVC的内部还定义了一系列的预定义模板。当我们调用HtmlHelper/HtmlHelper<TModel>的模板方法对Model或者
Model的某个成员进行呈现的时候，系统会根据当前的呈现模式（显示模式和编辑模式）和Model元数据获取一个具体的模板（自定义模版或者预定义模版）。
由于Model具有显示和编辑两种呈现模式，所以定义在ASP.NET MVC内部的默认模版分为这两种基本的类型。
接下来我们就逐个介绍这些预定义模版以及最终的HTML呈现方式。


###EmailAddress

``` C#
public class Model
{
    [UIHint("EmailAddress")]
    public string Foo { get; set; }
}
```

然后在一个基于Model类型的强类型View中，我们通过调用HtmlHelper<TModel>的DisplayFor方法将一个具体的Model对象的Foo属性以显示模式呈现出来。

``` XML
@model Model
@Html.DisplayFor(m=>m.Foo)
```

如下的代码片断表示Model的Foo属性对应的HTML，我们可以看到它就是一个针对Email地址的连接。
当我们点击该链接的时候，相应的Email编辑软件（比如Outlook）会被开启用于针对目标Email地址的邮件编辑。

``` XMl
<a href="mailto:foo@gmail.com">foo@gmail.com</a>
```


###HiddenInput


###Html

###Text与String


###Url

与EmailAddress和Html一样，模板Url也仅限于显示模式。

###MultilineText

###Password

###Decimal

如果采用Decimal模板，代表目标元素的数字不论其小数位数是多少，都会最终被格式化为两位小数。


###Boolean

通过本章最开始的实例演示我们知道一个布尔类型的对象在编辑模式下会以一个类型为“checkbox”的<input>元素的形式呈现，
实际上在显示模式下它依然对应着这么一个元素，只是其disabled属性会被设置为True使之处于只读状态


###Collection

顾名思义，Collection模板用于集合类型的目标元素的显示与编辑。对应采用该模板的类型为集合（实现了IEnumerable接口）的目标元素，
在调用HtmlHelper或者HtmlHelper<TModel>以显示或者编辑模式对其进行呈现的时候，会遍历其中的每个元素，
并根据基于集合元素的Model元数据决定对其的呈现方法。

###Object

我们说过，ASP.NET 内部采用基于类型的模板匹配策略，如果通过ModelMetadata对象表示的Model元数据不能找到一个具体的模板，
最终都会落到Object模板上










