[MVC和Web API 过滤器Filter](http://www.cnblogs.com/gcczhongduan/p/4375675.html)

ASP.NET MVC 支持下面类型的操作筛选器：

* 授权筛选器。这些筛选器用于实现IAuthorizationFilter和做出关于是否运行操作方法（如运行身份验证或验证请求的属性）的安全决策。AuthorizeAttribute类和RequireHttpsAttribute类是授权筛选器的演示样例。授权筛选器在不论什么其它筛选器之前运行。

* 操作筛选器。这些筛选器用于实现IActionFilter以及包装操作方法运行。IActionFilter接口声明两个方法：OnActionExecuting和OnActionExecuted。OnActionExecuting在操作方法之前运行。OnActionExecuted在操作方法之后运行，能够运行其它处理，如向操作方法提供额外数据、检查返回值或取消运行操作方法。

* 结果筛选器。这些筛选器用于实现IResultFilter以及包装ActionResult对象的运行。IResultFilter声明两个方法：OnResultExecuting和OnResultExecuted。OnResultExecuting在运行ActionResult对象之前运行。OnResultExecuted在结果之后运行，能够对结果运行其它处理，如改动HTTP响应。OutputCacheAttribute类是结果筛选器的一个演示样例。

* 异常筛选器。这些筛选器用于实现IExceptionFilter，并在ASP.NET MVC管道运行期间引发了未处理的异常时运行。异常筛选器可用于运行诸如日志记录或显示错误页之类的任务。HandleErrorAttribute类是异常筛选器的一个演示样例。

不同类型的筛选器

ASP.NET MVC 框架支持四种不同类型的筛选器：

1. 授权筛选器 — — 实现的IAuthorizationFilter属性。
2. 操作筛选器 — — 实现的IActionFilter属性。
3. 结果筛选器 — — 实现的IResultFilter属性。
4. 异常筛选器 — — 实现的IExceptionFilter属性。

 

特别注意：

MVC中过滤器是system.web.Mvc.dll实现

实现全局过滤器：App_Start目录下的FilterConfig.cs文件里

``` C#
public static void RegisterGlobalFilters(GlobalFilterCollection filters)

{

       //new Test_Mvc.Mvc_Filter()过滤器类的实例对象

      filters.Add(new Test_Mvc.Mvc_Filter());//--注冊全局过滤器

}
```

 

Web Api 中过滤器 system.web.http.dll中的system.web.http.Filters实现，加入控制器时一定要注意：一定要选择空的API控制器。假设选择空的MVC控制器那么过滤器对该控制器失效。

实现全局过滤器：Global.asax文件的：

``` C#
protected void Application_Start()

{

    //new Test_Http.Http_Filter()过滤器类的实例对象

    GlobalConfiguration.Configuration.Filters.Add(newTest_Http.Http_Filter());//--注冊全局过滤器

}
```