[An item with the same key has already been added. Key: idsrv4:tokenvalidation:token ](https://github.com/IdentityServer/IdentityServer4.AccessTokenValidation/issues/32)


> `UseIdentityServerAuthentication` 使用了2次

```
System.ArgumentException: An item with the same key has already been added. Key: idsrv4:tokenvalidation:token
   at System.ThrowHelper.ThrowAddingDuplicateWithKeyArgumentException(Object key)
   at System.Collections.Generic.Dictionary`2.Insert(TKey key, TValue value, Boolean add)
   at IdentityServer4.AccessTokenValidation.IdentityServerAuthenticationMiddleware.<Invoke>d__7.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware`1.<Invoke>d__18.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware`1.<Invoke>d__18.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at IdentityServer4.AccessTokenValidation.IdentityServerAuthenticationMiddleware.<Invoke>d__7.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Server.IISIntegration.IISMiddleware.<Invoke>d__8.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Hosting.Internal.RequestServicesContainerMiddleware.<Invoke>d__3.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Server.Kestrel.Internal.Http.Frame`1.<RequestProcessingAsync>d__2.MoveNext()


```