
[An unhandled exception occurred while processing the request.
Exception: External authentication error
IdentityServer4.Quickstart.UI.AccountController+<ExternalLoginCallback>d__8.MoveNext() in AccountController.cs, line 182]

> 使用`Quickstart.UI` OpenID Connect 登录之后

>  需要设置`SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme`

```cs
IdentityServer4.Quickstart.UI.AccountController+<ExternalLoginCallback>d__8.MoveNext() in AccountController.cs
+
            var claims = tempUser.Claims.ToList();
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker+<InvokeActionMethodAsync>d__27.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker+<InvokeNextActionFilterAsync>d__25.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ActionExecutedContext context)
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
IdentityServer4.Hosting.IdentityServerMiddleware+<Invoke>d__4.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
IdentityServer4.Hosting.FederatedSignOutMiddleware+<Invoke>d__6.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
IdentityServer4.Hosting.AuthenticationMiddleware+<Invoke>d__2.MoveNext()
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
Microsoft.AspNetCore.Cors.Infrastructure.CorsMiddleware+<Invoke>d__7.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
IdentityServer4.Hosting.BaseUrlMiddleware+<Invoke>d__2.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
Microsoft.VisualStudio.Web.BrowserLink.BrowserLinkMiddleware+<ExecuteWithFilter>d__7.MoveNext()
System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware+<Invoke>d__7.MoveNext()
```