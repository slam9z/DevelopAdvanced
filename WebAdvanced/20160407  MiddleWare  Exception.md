20160407  MiddleWare  Exception

``` C# 
public class InvokeMiddleWare
{
	public void Invoke()
	{
		Console.Write("InvokeMiddleWare");
	}
}
``` C# 

System.NotSupportedException was unhandled by user code
  HResult=-2146233067
  Message=The type 'WebShop.Startup+InvokeMiddleWare' does not match any known middleware pattern.
  Source=Microsoft.Owin
  StackTrace:
       at Microsoft.Owin.Builder.AppBuilder.ToMiddlewareFactory(Object middlewareObject, Object[] args)
       at Microsoft.Owin.Builder.AppBuilder.Use(Object middleware, Object[] args)
       at WebShop.Startup.Configuration(IAppBuilder app) in D:\VsFile\Source\WebPractice\WebPractice\WebShop_WebAPI\Startup.cs:line 43
  InnerException: 

``` C#  
public void Invoke(object middleWare)
{
	Console.Write("InvokeMiddleWare 2");
}
```
  
  System.ArgumentException was unhandled by user code
  HResult=-2147024809
  Message=The type 'System.Void' may not be used as a type argument.
  Source=mscorlib
  StackTrace:
       at System.RuntimeType.ThrowIfTypeNeverValidGenericArgument(RuntimeType type)
       at System.RuntimeType.SanityCheckGenericArguments(RuntimeType[] genericArguments, RuntimeType[] genericParamters)
       at System.RuntimeType.MakeGenericType(Type[] instantiation)
       at System.Linq.Expressions.Compiler.DelegateHelpers.GetFuncType(Type[] types)
       at System.Linq.Expressions.Expression.GetFuncType(Type[] typeArgs)
       at Microsoft.Owin.Builder.AppBuilder.ToGeneratorMiddlewareFactory(Object middlewareObject, Object[] args)
       at Microsoft.Owin.Builder.AppBuilder.ToMiddlewareFactory(Object middlewareObject, Object[] args)
       at Microsoft.Owin.Builder.AppBuilder.Use(Object middleware, Object[] args)
       at WebShop.Startup.Configuration(IAppBuilder app) in D:\VsFile\Source\WebPractice\WebPractice\WebShop_WebAPI\Startup.cs:line 43
  InnerException: 

  
``` C# 
public class TypeMiddleWare
{
	public TypeMiddleWare()
	{
		Console.Write("TypeMiddleWare");
	}
}
```

  System.MissingMethodException was unhandled by user code
  HResult=-2146233069
  Message=The class 'WebShop.Startup+TypeMiddleWare' does not have a constructor taking 1 arguments.
  Source=Microsoft.Owin
  StackTrace:
       at Microsoft.Owin.Builder.AppBuilder.ToConstructorMiddlewareFactory(Object middlewareObject, Object[] args, Delegate& middlewareDelegate)
       at Microsoft.Owin.Builder.AppBuilder.ToMiddlewareFactory(Object middlewareObject, Object[] args)
       at Microsoft.Owin.Builder.AppBuilder.Use(Object middleware, Object[] args)
       at WebShop.Startup.Configuration(IAppBuilder app) in D:\VsFile\Source\WebPractice\WebPractice\WebShop_WebAPI\Startup.cs:line 46
  InnerException: 

