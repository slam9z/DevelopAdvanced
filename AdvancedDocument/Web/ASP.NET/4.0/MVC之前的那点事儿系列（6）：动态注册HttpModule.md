[MVC之前的那点事儿系列（6）：动态注册HttpModule](http://www.cnblogs.com/TomXu/p/3756846.html)

通过前面的章节，我们知道HttpApplication在初始化的时候会初始化所有配置文件里注册的HttpModules，
那么有一个疑问，能否初始化之前动态加载HttpModule，而不是只从Web.config里读取？

答案是肯定的， ASP.NET MVC3发布的时候提供了一个Microsoft.Web.Infrastructure.dll文件，这个文件就是
提供了动态注册HttpModule的功能，那么它是如何以注册的呢？我们先去MVC3的源码看看该DLL的源代码。

注：该DLL位置在C:\Program Files\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\下 
我们发现了一个静态类DynamicModuleUtility，里面有个RegisterModule方法引起了我的注意：

```cs
// Call from PreAppStart to dynamically register an IHttpModule, just as if you had added it to the
// <modules> section in Web.config. 
[SecuritySafeCritical] 
public static void RegisterModule(Type moduleType) {
    if (DynamicModuleReflectionUtil.Fx45RegisterModuleDelegate != null) { 
        // The Fx45 helper exists, so just call it directly.
        DynamicModuleReflectionUtil.Fx45RegisterModuleDelegate(moduleType);
    }
    else { 
        // Use private reflection to perform the hookup.
        LegacyModuleRegistrar.RegisterModule(moduleType); 
    } 
}
```