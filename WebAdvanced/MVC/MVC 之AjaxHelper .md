[MVC 之AjaxHelper ](http://www.cnblogs.com/jyan/archive/2012/07/23/2604958.html)

##AjaxHelper

[AjaxHelper Class](https://msdn.microsoft.com/en-us/library/system.web.mvc.ajaxhelper(v=vs.118).aspx)

|Helper method | Description |
|-----|------|  
|Ajax.ActionLink |Creates a hyperlink to a controller action that fires an Ajax request when clicked |
|Ajax.RouteLink |Similar to Ajax.ActionLink, but generates a link to a particular route instead of a named controller action |
|Ajax.BeginForm | Creates a form element that submits its data to a particular controller action using Ajax |
|Ajax.BeginRouteForm  |Similar to Ajax.BeginForm, but creates a form that sub- mits its data to a particular route instead of a named control- ler action |
|Ajax.GlobalizationScript | Creates an HTML script element that references a script that contains culture information |
|Ajax.JavaScriptStringEncode | Encodes a string to make sure that it can safely be used inside JavaScript |



##jQuery和Microsoft Ajax



使用AjaxHelper可以很方便的实现Ajax请求，Aps.net MVC提供了jQuery和Microsoft Ajax类库两种方式来实现，使用何种方式取决于我们Web.config配置：


``` XML
<add key="UnobtrusiveJavaScriptEnabled" value="true" />
```

当设置为true时，将使用jQuery方式实现请求，生成的链接如下：


``` XML
 <a data-ajax="true" data-ajax-method="GET" data-ajax-mode="replace" data-ajax-update="#test" href="http://www.cnblogs.com/">测试</a>
```

反之则使用Microsoft Ajax类库实现
 
``` Html
<a href="http://www.cnblogs.com/" onclick="Sys.Mvc.AsyncHyperlink.handleClick(this, new Sys.UI.DomEvent(event), { insertionMode: Sys.Mvc.InsertionMode.replace, httpMethod: 'GET', updateTargetId: 'test' });">测试</a>
```

![](http://pic002.cnblogs.com/images/2012/193556/2012072314571568.png)


在我们创建项目时，该值默认为true。这种情况下吗，我们要在页面中引入相应的js类库：


``` Html
@section scripts{
    <script type="text/javascript" src=" @Url.Content("~/Scripts/jquery.unobtrusive-ajax.js")"></script>
}
```


##Ajax.ActionLink()：

向客户端输入一个链接地址，当单击这个链接时可以异步调用Controller中的方法，Ajax.ActionLink()方法有许多重载，我们这里举例说明其中一个比较常用的重载：

``` C#
public static string ActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, object routeValues, AjaxOptions ajaxOptions);
```

ajaxOptions：配置Ajax的一些选项，看完下面的例子我们再详细讲解这个配置选项。


|参数|说明|
|---|--|
|Confirm |获取或设置提交请求之前，显示在确认窗口中的消息。 |
|HttpMethod |获取或设置 HTTP 请求方法（“Get”或“Post”）。 |
|InsertionMode |获取或设置指定如何将响应插入目标 DOM 元素的模式。 |
|LoadingElementId |获取或设置加载 Ajax 函数时要显示的 HTML 元素的 id 特性。 |
|OnBegin | 获取或设置更新页面之前，恰好调用的 JavaScript 函数的名称。 |
|OnComplete | 获取或设置实例化响应数据之后但更新页面之前，要调用的 JavaScript 函数。 |
|OnFailure | 获取或设置页面更新失败时，要调用的 JavaScript 函数。 |
|OnSuccess |获取或设置成功更新页面之后，要调用的 JavaScript 函数。 |
|UpdateTargetId |获取或设置要使用服务器响应来更新的 DOM 元素的 ID。 |
|Url |获取或设置要向其发送请求的 URL。 |


我们将使用ActionLink分别异步请求ContentController，Json格式的Controller和PartialView格式的Controller来显示详细信息：

1. Ajax异步请求ContentController

2. 使用Json格式返回

3. 使用PartialView来返回数据

##Ajax.BeginForm



