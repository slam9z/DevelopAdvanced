[InvalidOperationException: No authentication handler is configured to authenticate for the scheme: idsrv]

> 配置好`Quickstart.UI`后运行

> 缺少 `app.UseIdentityServer();`


```cs
Microsoft.AspNetCore.Http.Authentication.Internal.DefaultAuthenticationManager+<GetAuthenticateInfoAsync>d__12.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
Microsoft.AspNetCore.Http.Authentication.AuthenticationManager+<AuthenticateAsync>d__6.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
IdentityServer4.Extensions.HttpContextExtensions+<GetIdentityServerUserAsync>d__7.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
System.Runtime.CompilerServices.TaskAwaiter.GetResult()
AspNetCore._Views_Shared__Layout_cshtml+<ExecuteAsync>d__41.MoveNext() in _Layout.cshtml

            var user = await Context.GetIdentityServerUserAsync();

System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
Microsoft.AspNetCore.Mvc.Razor.RazorView+<RenderPageAsync>d__14.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
Microsoft.AspNetCore.Mvc.Razor.RazorView+<RenderLayoutAsync>d__17.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
Microsoft.AspNetCore.Mvc.Razor.RazorView+<RenderAsync>d__13.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
Microsoft.AspNetCore.Mvc.ViewFeatures.ViewExecutor+<ExecuteAsync>d__18.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
Microsoft.AspNetCore.Mvc.ViewResult+<ExecuteResultAsync>d__26.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker+<InvokeResultAsync>d__30.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker+<InvokeNextResultFilterAsync>d__28.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ResultExecutedContext context)
Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Next(ref State next, ref Scope scope, ref object state, ref bool isCompleted)
Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker+<InvokeNextResourceFilter>d__22.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ResourceExecutedContext context)
Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Next(ref State next, ref Scope scope, ref object state, ref bool isCompleted)
Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker+<InvokeAsync>d__20.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
Microsoft.AspNetCore.Builder.RouterMiddleware+<Invoke>d__4.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
Microsoft.AspNetCore.Authentication.AuthenticationMiddleware+<Invoke>d__18.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
Microsoft.AspNetCore.Authentication.AuthenticationMiddleware+<Invoke>d__18.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
Microsoft.AspNetCore.Authentication.AuthenticationMiddleware+<Invoke>d__18.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
Microsoft.AspNetCore.Authentication.AuthenticationMiddleware+<Invoke>d__18.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
Microsoft.VisualStudio.Web.BrowserLink.BrowserLinkMiddleware+<ExecuteWithFilter>d__7.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware+<Invoke>d__7.MoveNext()
```