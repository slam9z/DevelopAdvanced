[async 与 await 在 Web 下的应用 ](http://www.cnblogs.com/therock/articles/2382534.html)

>`IO-Bound Operation`就是Web异步的好处，主要是提升系统整理的性能。


##在 WebForm 和 MVC 中使用 async 和 await

在 .net 4.5 中，最新的 WebForm 和 MVC 都已经支持这两个关键字了。

###在 asp.net WebForm 中：

1. 首先新建一个页面
1. 打开 aspx 文件，然后再顶部的属性中加入：Async="true"
1. 接下来在任何一个事件中，加入这两个关键字即可
1. 另外在 Web.Config 中有两个奇怪的配置，有可能会导致出错，去掉有正常，这两个配置具体有什么用，我已经在 StackOverFlow 上问别人了


```cs
protected async void Page_Load(object sender, EventArgs e) 
{ 
    WebClient client = new WebClient(); 
    var result1 = await client.DownloadStringTaskAsync("http://www.website.com"); 
    WebClient client2 = new WebClient(); 
    var result2 = await client.DownloadStringTaskAsync(result1); 
    //do more 
}
```

###在 asp.net MVC 中：

把原来继承于 Controller 改成继承于 AsyncController
在方法前加上 async，并把返回类型改成 Task<T>


```cs
public class HomeController : AsyncController 
{ 
    public async Task<ActionResult> Test() 
    { 
        var result = await Task.Run(() => 
        { 
            Thread.Sleep(5000); 
            return "hello"; 
        }); 
        return Content(result); 
    } 
}
```

### 在 IHttpHandlder 中：

微软官方的 .net 4.5 releace note 中已经提到了：

```cs
public class MyAsyncHandler : HttpTaskAsyncHandler 
{ 
    // ... 
  
    // ASP.NET automatically takes care of integrating the Task based override 
    // with the ASP.NET pipeline. 
    public override async Task ProcessRequestAsync(HttpContext context) 
    { 
        WebClient wc = new WebClient(); 
        var result = await 
            wc.DownloadStringTaskAsync("http://www.microsoft.com"); 
        // Do something with the result 
    } 
}
```
###在 IHttpModule 中：

同样是微软官方的 .net 4.5 [releace note](https://www.asp.net/whitepapers/whats-new-in-aspnet-45-and-visual-studio-2012#_Toc318097377) 中，实现起来有点复杂，大家可以自己去看看。
 

##在 Web 应用程序中使用 async 和 await 的注意事项

其实不仅仅是使用这两个关键字的注意事项，而是在 Web 中只要用到了异步页，就要注意一下问题！

###Web 本来就是多线程的，为什么还要用异步编程？

多线程只是实现异步的一种手段，的确，Web 本来就是多线程的，所以在很多时候不用异步也没什么问题。一般也不会有问题，只是有更好的方案。

大家看完[《正确使用异步操作》](http://blog.zhaojie.me/2008/02/use-async-operation-properly.html)后就会知道，异步有多种实现方式，但是它们底层只有两种类型，一种是：`“Compute-Bound Operation”`，另一种是`“IO-Bound Operation”`。（具体的可以到文中查看）

在 Web 中，使用异步去处理“Compute-Bound Operation”是没有意义的，因为 Web 本来就是多线程的，这样做没有任何效率上的提升。（除非你在处理这个异步的时候，不需要等待这个异步执行结束就可以返回页面内容）

所以，在 Web 中，只有当你需要面对“IO-Bound Operation”的时候，去用异步页才是真的有用的。因为它是在等待磁盘或者网络响应，并不占据资源，甚至不占据工作线程。

如何区分呢？那篇文章中已经写了，另外，大部分和磁盘&网络打交道的异步操作都是“IO-Bound Operation”的。

但是，如果你真的想要提升效率，还需要你亲自去测试一下，因为要实现“IO-Bound Operation”有一定的条件。

###WebClient、WebService 和 WCF 支持吗？

经过测试，上面这三种 Web 应用程序中使用最多的，是支持“IO-Bound Operation”的。其中，在 .net 4.5 中，WebClient 和 WCF 可以直接支持 async 和 await 关键字。（因为它们有相关的方法可以返回 Task 对象）

而 WebService（微软不建议使用，但实际上还在被大量的应用），却不支持，但是可以通过写一些代码后让它支持。

###数据库操作支持吗？

经过一定的配置后，它是可以支持的，但是具体的还需要进行大量的测试，毕竟不是调用几个方法那么简单。
 
###如何把传统的异步模式转换成 TPL 模式，以实现 async 和 await

上面提到了 WebService 并没有实现 TPL 模式，在 .net 4.5 中引用 WebService 后实现的是基于事件的异步。
（.net 2.0 以上程序在引用 WebService 的时候，需要点“添加服务引用”——“高级”——“添加Web引用”，如果直接在服务引用中添加，会出现一定的问题。并且，就算你添加了，它也没有帮你实现基于 TPL 的异步。）
如何把 APM 模式转换成 TPL 模式？

其实微软在这篇文章中已经写过如何把传统的异步模式转换成 TPL 模式了：TPL 和传统 .NET 异步编程
其中 APM 转 TPL 比较简单，我就不多介绍了。