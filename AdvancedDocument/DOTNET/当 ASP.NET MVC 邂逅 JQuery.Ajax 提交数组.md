[当 ASP.NET MVC 邂逅 JQuery.Ajax 提交数组](http://www.cnblogs.com/coolite/archive/2012/12/24/JQModelBinder.html)

使用Ajax上传数组会自动加上[]。

```js

var data = {};
data.ProjectPath = ["system", "module"];
$.ajax({
    data: data,
});

```

```cs
request.ProjectPath = Request.QueryString.GetValues("ProjectPath[]");

```