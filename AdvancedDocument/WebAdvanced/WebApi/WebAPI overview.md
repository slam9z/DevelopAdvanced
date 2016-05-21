##项目结构

###Startup

1. Owin Startup Attirbute

    ``` C#
    [assembly: OwinStartup(startupType)]

    OwinStartupAttribute(Type startupType)
    ```
    ``` XMl
    <appSettings>  
      <add key="owin:appStartup" value="StartupDemo.ProductionStartup" />
    </appSettings>
    ```

2. Configuration Method

    ``` C#
    public void Configuration(IAppBuilder app)
    ```

###App_Start

App_Start只是一个常见的目录结构，没有特定的组织。常见的配置：

* BundleConfig  
    配置StyleBundle与ScriptBundle

* FilterConfig  
    配置Filter
        
* RountConfig  
    配置默认路由

常在Global.asax.cs 文件中被调用
``` C#
   public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
```
###favicon.ico

###Controllers  

WebAPIController

###Content

css文件

###Scripts

js文件

###Views

cshtml文件

###Web.config

configuration  
    configSections  
    connectionStrings  
    appSettings  
    system.web  
    system.webServer  
    runtime
    entityFramework  
        defaultConnectionFactory  
        providers  

``` xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <!-- For more information on Entity Framework configuration, visit http://go.microsoft.com/fwlink/?LinkID=237468 -->
    <section name="entityFramework" type="System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" requirePermission="false" />
  </configSections>
  <connectionStrings>
    <add name="DefaultConnection" connectionString="Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-WebApplication2015-20150328061715.mdf;Initial Catalog=aspnet-WebApplication2015-20150328061715;Integrated Security=True"
      providerName="System.Data.SqlClient" />
  </connectionStrings>
  <appSettings>
    <add key="webpages:Version" value="3.0.0.0" />
    <add key="webpages:Enabled" value="false" />
    <add key="ClientValidationEnabled" value="true" />
    <add key="UnobtrusiveJavaScriptEnabled" value="true" />
  </appSettings>
  <system.web>
    <authentication mode="None" />
    <compilation debug="true" targetFramework="4.5" />
    <httpRuntime targetFramework="4.5" />
  </system.web>
  <system.webServer>
    <modules>
      <remove name="FormsAuthentication" />
    </modules>
  </system.webServer>
  <runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
      <dependentAssembly>
        <assemblyIdentity name="Microsoft.Owin" publicKeyToken="31bf3856ad364e35" />
        <bindingRedirect oldVersion="1.0.0.0-3.0.0.0" newVersion="3.0.0.0" />
      </dependentAssembly>
    </assemblyBinding>
  </runtime>
  <entityFramework>
    <defaultConnectionFactory type="System.Data.Entity.Infrastructure.LocalDbConnectionFactory, EntityFramework">
      <parameters>
        <parameter value="mssqllocaldb" />
      </parameters>
    </defaultConnectionFactory>
    <providers>
      <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
    </providers>
  </entityFramework>
</configuration>
```

###packages.config

``` xml
<?xml version="1.0" encoding="utf-8"?>
<packages>
  <package id="Antlr" version="3.4.1.9004" targetFramework="net45" />
</packages>
```

##IAppBuilder

[AppBuilder Class](https://msdn.microsoft.com/en-us/library/microsoft.owin.builder.appbuilder%28v=vs.113%29.aspx?f=255&MSPPError=-2147217396)

1. IAppBuilder接口

    ``` C#

    namespace Owin
    {
	    public interface IAppBuilder
	    {
		    IDictionary<string, object> Properties { get; }

		    object Build(Type returnType);
		    IAppBuilder New();
		    IAppBuilder Use(object middleware, params object[] args);
	    }
    }
    ```

    主要使用IAppBuilder的Use方法配置中间件

2. IAppBuilder扩展
    一般使用扩展方法进行扩展，例如

    ```
    namespace Owin
    {
        public static class AppBuilderExtensions
        {
		    public static IAppBuilder CreatePerOwinContext<T>(this IAppBuilder app, Func<T> createCallback) where T : class, IDisposable;
		    public static IAppBuilder CreatePerOwinContext<T>(this IAppBuilder app, Func<IdentityFactoryOptions<T>, IOwinContext, T> createCallback) where T : class, IDisposable;
		    public static void UseExternalSignInCookie(this IAppBuilder app);
		    public static void UseExternalSignInCookie(this IAppBuilder app, string externalAuthenticationType);
		    public static void UseOAuthBearerTokens(this IAppBuilder app, OAuthAuthorizationServerOptions options);
		    public static void UseTwoFactorRememberBrowserCookie(this IAppBuilder app, string authenticationType);
		    public static void UseTwoFactorSignInCookie(this IAppBuilder app, string authenticationType, TimeSpan expires);
        }
    }
    ```
    ``` C#
    namespace Owin
    {
	    public static class CookieAuthenticationExtensions
	    {
		    public static IAppBuilder UseCookieAuthentication(this IAppBuilder app, CookieAuthenticationOptions options);
		    public static IAppBuilder UseCookieAuthentication(this IAppBuilder app, CookieAuthenticationOptions options, PipelineStage stage);
	    }
    }
    ```

    实例
    ``` C#
    namespace WebApplication2015
    {
        public partial class Startup
        {
            // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
            public void ConfigureAuth(IAppBuilder app)
            {
            }
        }
    }
    ```

3. 职责链？

    

4. 原理

    Parameters
    middleware
    >Type: System.Object
    The middleware parameter determines which behavior is being chained into the pipeline. If the middleware given to Use is a Delegate, 
    then it will be invoked with the "next app" in the chain as the first parameter. If the delegate takes more than the single argument, 
    then the additional values must be provided to Use in the args array. If the middleware given 
    to Use is a Type, then the public constructor will be invoked with the "next app" in the chain 
    as the first parameter. The resulting object must have a public Invoke method. If the object has 
    constructors which take more than the single "next app" argument, then additional values may be 
    provided in the args array.

[AppBuilder（一）Use汇总 ](http://www.cnblogs.com/hmxb/p/5299216.html)