[关于HttpContext.Current.Items](http://www.cnblogs.com/Last-Redemption/archive/2011/07/20/2111626.html#undefined)

之前只是在使用Session来进行用户会话时的信息存储，甚至很少留意Session完整的类调用是HttpContext.Current.Sessoin....

好吧，我还是处于只会固定写法的超级小菜....

之前偶然看到bbsmax的代码中使用HttpContext.Current.Items来存储当前用户ID，之前便模仿着写着身份验证，于是乎今天遇到问题，需要彻底了解一下这个HttpContext.Current.Items首先，**HttpContext.Current.Items的用途受到严重限制，它只作用于单独的一个用户请求(HttpContext.Current.Items valid for a single HTTPRequest)**。完成这个请求，服务器信息传回浏览器的时候，这个Item集合将丢失。而Session对象是针对用户的本次会话，也就是作用于多个用户请求，在Session失效后才丢失其中的信息。既然HttpContext.Current.Items的生命周期如此之短，那在什么情况下可以加以利用呢。百度查不到什么的情况下，找到了一篇国外的文章:

`http://abhijitjana.net/2011/01/14/when-we-can-use-httpcontext-current-items-to-stores-data-in-asp-net/`

这里指出，HttpContext.Current.Items 可以在 HttpModule 和 HTTPHandler 之间共享数据时使用，因为每次用户请求都要通过HTTP 运行时管道HttpModule 、HTTPHandler 。当你实现IHttpMoudle的方法来通过HttpMoudle向用户请求传递信息。如下图所示:

你可以用HttpContext.Current.Items 在不同请求页，不同的HttpModule中传输数据，但是一旦请求结束，数据回发，这个集合中的数据将自己丢失。

像今天，在HttpContext.Current.Items 中加入对象，但是Response.Redirect之后便丢失了数据...