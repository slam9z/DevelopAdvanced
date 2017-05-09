[SqlException: 列名 'UserId' 无效]()

> EF非常让人烦的地方，如果Entity与Table不一致就会报错。

```
SqlException: 列名 'UserId' 无效。

    System.Data.SqlClient.SqlConnection.OnError(SqlException exception, bool breakConnection, Action<Action> wrapCloseInAction)
    System.Data.SqlClient.TdsParser.ThrowExceptionAndWarning(TdsParserStateObject stateObj, bool callerHasConnectionLock, bool asyncClose)
    System.Data.SqlClient.TdsParser.TryRun(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj, out bool dataReady)
    System.Data.SqlClient.SqlDataReader.TryConsumeMetaData()
    System.Data.SqlClient.SqlDataReader.get_MetaData()
    System.Data.SqlClient.SqlCommand.FinishExecuteReader(SqlDataReader ds, RunBehavior runBehavior, string resetOptionsString)
    System.Data.SqlClient.SqlCommand.RunExecuteReaderTds(CommandBehavior cmdBehavior, RunBehavior runBehavior, bool returnStream, bool async, int timeout, out Task task, bool asyncWrite, SqlDataReader ds)
    System.Data.SqlClient.SqlCommand.ExecuteReader(CommandBehavior behavior)
    Microsoft.EntityFrameworkCore.Storage.Internal.RelationalCommand.Execute(IRelationalConnection connection, string executeMethod, IReadOnlyDictionary<string, object> parameterValues, bool closeConnection)
    Microsoft.EntityFrameworkCore.Storage.Internal.RelationalCommand.ExecuteReader(IRelationalConnection connection, IReadOnlyDictionary<string, object> parameterValues)
    Microsoft.EntityFrameworkCore.Query.Internal.QueryingEnumerable+Enumerator.BufferlessMoveNext(bool buffer)
    Microsoft.EntityFrameworkCore.Storage.Internal.SqlServerExecutionStrategy.Execute<TState, TResult>(Func<TState, TResult> operation, Func<TState, ExecutionResult<TResult>> verifySucceeded, TState state)
    Microsoft.EntityFrameworkCore.Query.QueryMethodProvider+<_ShapedQuery>d__3.MoveNext()
    Microsoft.EntityFrameworkCore.Query.Internal.LinqOperatorProvider+ExceptionInterceptor+EnumeratorExceptionInterceptor.MoveNext()
    System.Collections.Generic.List..ctor(IEnumerable<T> collection)
    System.Linq.Enumerable.ToList<TSource>(IEnumerable<TSource> source)
    Rwby.Global.Service.UserRepository.GetUsers() in UserRepository.cs

                var users = UserContext.Users

Rwby.Global.Service.UserService.GetUsers() in UserService.cs

                return _userRepository.GetUsers();

Rwby.Global.Api.UserController.GetUsers() in UserController.cs

                    return _userService.GetUsers();
    lambda_method(Closure , object , Object[] )
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
    Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware+<Invoke>d__7.MoveNext()

```

> 千万不要有相同的两个映射。

```
System.InvalidOperationException occurred
  HResult=0x80131509
  Message='UserEntity.Id' and 'UserEntity.UserId' are both mapped to column 'Id' in 'AspNetUsers' but are configured to use different data types ('nvarchar(450)' and 'nvarchar(max)').
  Source=<Cannot evaluate the exception source>
  StackTrace:
   at Microsoft.EntityFrameworkCore.Internal.ModelValidator.ShowError(String message)
   at Microsoft.EntityFrameworkCore.Internal.RelationalModelValidator.EnsureSharedColumnsCompatibility(IModel model)
   at Microsoft.EntityFrameworkCore.Internal.RelationalModelValidator.Validate(IModel model)
   at Microsoft.EntityFrameworkCore.Infrastructure.ModelSource.CreateModel(DbContext context, IConventionSetBuilder conventionSetBuilder, IModelValidator validator)
   at System.Collections.Concurrent.ConcurrentDictionary`2.GetOrAdd(TKey key, Func`2 valueFactory)
   at Microsoft.EntityFrameworkCore.Internal.DbContextServices.CreateModel()
   at Microsoft.EntityFrameworkCore.Internal.LazyRef`1.get_Value()
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitScoped(ScopedCallSite scopedCallSite, ServiceProvider provider)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitConstructor(ConstructorCallSite constructorCallSite, ServiceProvider provider)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitScoped(ScopedCallSite scopedCallSite, ServiceProvider provider)
   at Microsoft.Extensions.DependencyInjection.ServiceProvider.<>c__DisplayClass16_0.<RealizeService>b__0(ServiceProvider provider)
   at Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService(IServiceProvider provider, Type serviceType)
   at Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService[T](IServiceProvider provider)
   at Microsoft.EntityFrameworkCore.Infrastructure.EntityFrameworkServiceCollectionExtensions.<>c.<AddQuery>b__1_3(IServiceProvider p)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitScoped(ScopedCallSite scopedCallSite, ServiceProvider provider)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitConstructor(ConstructorCallSite constructorCallSite, ServiceProvider provider)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitScoped(ScopedCallSite scopedCallSite, ServiceProvider provider)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitConstructor(ConstructorCallSite constructorCallSite, ServiceProvider provider)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitScoped(ScopedCallSite scopedCallSite, ServiceProvider provider)
   at Microsoft.Extensions.DependencyInjection.ServiceProvider.<>c__DisplayClass16_0.<RealizeService>b__0(ServiceProvider provider)
   at Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetService[T](IServiceProvider provider)
   at Microsoft.EntityFrameworkCore.Infrastructure.AccessorExtensions.GetService[TService](IInfrastructure`1 accessor)
   at Microsoft.EntityFrameworkCore.DbContext.get_QueryProvider()
   at Microsoft.EntityFrameworkCore.Internal.InternalDbSet`1.<.ctor>b__3_0()
   at Microsoft.EntityFrameworkCore.Internal.LazyRef`1.get_Value()
   at Microsoft.EntityFrameworkCore.Internal.InternalDbSet`1.System.Linq.IQueryable.get_Provider()
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.AsNoTracking[TEntity](IQueryable`1 source)
   at Rwby.Global.Service.UserRepository.GetUsers() in D:\Source\MyGithub\Rwby\src\Rwby.Global\Rwby.Global.Service\Repository\UserRepository.cs:line 40
   at Rwby.Global.Service.UserService.GetUsers() in D:\Source\MyGithub\Rwby\src\Rwby.Global\Rwby.Global.Service\Service\UserService.cs:line 25
   at Rwby.Global.Api.UserController.GetUsers() in D:\Source\MyGithub\Rwby\src\Rwby.Global\Rwby.Global.Api\Controllers\UserController.cs:line 32
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionMethodAsync>d__27.MoveNext()


```