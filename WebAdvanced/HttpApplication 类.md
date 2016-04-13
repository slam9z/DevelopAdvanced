[HttpApplication 类](https://msdn.microsoft.com/zh-cn/library/system.web.httpapplication(v=vs.110).aspx)


HttpApplication 类的实例是在 ASP.NET 基础结构中创建的，而不是由用户直接创建的。使用 HttpApplication 类的一个实例来处理其生存期中收到的众多请求。
但是，它每次只能处理一个请求。这样，成员变量才可用于存储针对每个请求的数据。

应用程序引发的事件可以由实现 IHttpModule 接口的自定义模块处理，也可以由 Global.asax 文件中定义的事件处理程序代码处理。
可以将实现 IHttpModule 接口的自定义模块放在 App_Code 文件夹中，也可以放在 Bin 文件夹下的某个 DLL 中。

HttpApplication 是在 .NET Framework 3.5 版中引入的。有关详细信息，请参阅Wersje i zależności programu .NET Framework。


如果 IIS 7.0 在集成模式下运行，App_Code 文件夹或 Bin 文件夹中的自定义模块将应用于请求管线中的所有请求。
Global.asax 文件中的事件处理程序代码则只应用于映射到某个 ASP.NET 处理程序的请求。
 

按照以下顺序引发应用程序事件：

1. BeginRequest


2. AuthenticateRequest


3. PostAuthenticateRequest


4. AuthorizeRequest


5. PostAuthorizeRequest


6. ResolveRequestCache


7. PostResolveRequestCache

    在 PostResolveRequestCache 事件之后和 PostMapRequestHandler 事件之前，会创建一个事件处理程序（一个对应于请求 URL 的页）。
    如果服务器在集成模式下运行 IIS 7.0 并且 .NET Framework 至少为 3.0 版本，则会引发 MapRequestHandler 事件。
    如果服务器在经典模式下运行 IIS 7.0 或者运行的是较早版本的 IIS，则无法处理此事件。


8. PostMapRequestHandler


9. AcquireRequestState


10. PostAcquireRequestState


11. PreRequestHandlerExecute

    执行事件处理程序。一般的框架处理都在这里


12. PostRequestHandlerExecute


13. ReleaseRequestState


14. PostReleaseRequestState

    在引发 PostReleaseRequestState 事件之后，现有的所有响应筛选器都将对输出进行筛选。


15. UpdateRequestCache


16. PostUpdateRequestCache


17. LogRequest.

    仅在 IIS 7.0 处于集成模式并且 .NET Framework 至少为 3.0 版本的情况下才支持此事件。


18. PostLogRequest

    仅在 IIS 7.0 处于集成模式并且 .NET Framework 至少为 3.0 版本的情况下才支持此事件。


19. EndRequest

