[ASP.NET MVC下的异步Action的定义和执行原理 ](http://www.cnblogs.com/artech/archive/2012/06/20/async-action-in-mvc.html)


##一、基于线程池的请求处理

##二、两种异步Action方法的定义

###XxxAsync/XxxCompleted
``` C#
public class HomeController : AsyncController
{
    public void ArticleAsync(string name)
    {
        AsyncManager.OutstandingOperations.Increment();
        Task.Factory.StartNew(() =>
            {
                string path = ControllerContext.HttpContext.Server.MapPath(string.Format(@"\articles\{0}.html", name));
                using (StreamReader reader = new StreamReader(path))
                 {
                     AsyncManager.Parameters["content"] = reader.ReadToEnd();
                 }
                 AsyncManager.OutstandingOperations.Decrement();
             });
     }
     public ActionResult ArticleCompleted(string content)
     {
         return Content(content);
     }
}
```

###Task返回值  

``` c#
public class HomeController : AsyncController
{
    public Task<ActionResult> Article(string name)
    {
        return Task.Factory.StartNew(() =>
            {
                string path = ControllerContext.HttpContext.Server.MapPath(string.Format(@"\articles\{0}.html", name));
                using (StreamReader reader = new StreamReader(path))
                {
                     return reader.ReadToEnd();
                }
            }).ContinueWith<ActionResult>(task =>
                {                    
                    return Content((string)task.Result);
                });
    }
}
```

##三、AsyncManager


##四、Completed方法的执行


##五、异步操作的超时控制
