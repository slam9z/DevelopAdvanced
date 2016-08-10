[ASP.NET Core 十种方式扩展你的 Views](http://mp.weixin.qq.com/s?__biz=MzAwNTMxMzg1MA==&mid=2654067764&idx=2&sn=5fdfe60c4fac4f9702bca722098d809c&scene=4#wechat_redirect)


#7: 标签助手（TagHelper）

这是 ASP.NET Core 非常好的一个新特性。

可以像Html一样写Razor代码

这是 ASP.NET Core 非常好的一个新特性。
一个扩展你视图的小助手，它看起来像一个原生的HTML标签一样。 在ASP.NET Core MVC中你应该使用 TagHelpers 来替换 HtmlHelpers，因为它们更加的简洁和容易使用。另一个巨大的好处就是依赖注入，在HtmlHelpers中是使用不了的，因为HtmlHelpers 扩展的都是静态内容。 但TagHelpers是一个公共类，我们可以很容易的在它的构造函数中注入服务。
下面是一个很简单的小示例，来展示怎么样定义一个TagHelper：

```C#
[TargetElement("hi")] 
public class HelloTagHelper : TagHelper { 
    public override void Process(TagHelperContext context, TagHelperOutput output) 
    { 
        output.TagName = "p"; 
        output.Attributes.Add("id", context.UniqueId); 

        output.PreContent.SetContent("Hello "); 
        output.PostContent.SetContent(string.Format(", time is now: {0}",  
                DateTime.Now.ToString("HH:mm"))); 
    } 
}
```

这里定义了一个叫做hi的标签，它以HTML标记来呈现，p标签的内容是当前时间。
使用:

```html
<hi>John Smith</hi>
```

结果:

```html
<p>Hello John Smith, time is now: 18:55</p>
```

ASP.NET Core MVC 已经默认提供了很多TagHelpers来替换以前的HtmlHelpers。例如ActionLink已经被新的TagHelper所替换

```html
@Html.ActionLink(“About me”, “About”, “Home”)
```

新的TagHelper像这样来创建一个link：

```html
<a asp-controller=”Home” asp-action=”About”>About me</a>
```

以上两种方式来创建一个a标签的结果：

```html
<a href=”/Home/About”>About me</a>
```

可以看到，TagHelpers看起来更像原生的HTML，他们在视图中更加的直观，更高的可读性，并且更容易使用。
