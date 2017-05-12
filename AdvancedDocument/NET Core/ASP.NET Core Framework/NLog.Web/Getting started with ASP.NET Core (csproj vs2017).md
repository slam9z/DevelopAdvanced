[Getting started with ASP.NET Core (csproj vs2017)](https://github.com/NLog/NLog.Web/wiki/Getting-started-with-ASP.NET-Core-(csproj---vs2017))

How to use for ASP.NET Core with csproj (VS2017 only)

### 0. Create a new ASP.NET Core project

In Visual Studio 2017.

### 1. Add dependency in csproj manually or using NuGet


Install the latest:

- [NLog.Web.AspNetCore](https://www.nuget.org/packages/NLog.Web.AspNetCore)

NB: Update the NLog package if possible

- NLog 5.x for .NET Core (Note NLog 5 is still a pre-release)
- NLog 4.x or 5.x for ASP.NET Core on the .NET full framework

### 2. Create a nlog.config file. 
Create nlog.config (lowercase all) file in the root of your project.

We use this example:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
      autoReload="true"
      internalLogLevel="Warn"
      internalLogFile="c:\temp\internal-nlog.txt">

  <!-- Load the ASP.NET Core plugin -->
  <extensions>
    <add assembly="NLog.Web.AspNetCore"/>
  </extensions>

  <!-- the targets to write to -->
  <targets>
     <!-- write logs to file -->
     <target xsi:type="File" name="allfile" fileName="c:\temp\nlog-all-${shortdate}.log"
                 layout="${longdate}|${event-properties:item=EventId.Id}|${logger}|${uppercase:${level}}|${message} ${exception}" />

   <!-- another file log, only own logs. Uses some ASP.NET core renderers -->
     <target xsi:type="File" name="ownFile-web" fileName="c:\temp\nlog-own-${shortdate}.log"
             layout="${longdate}|${event-properties:item=EventId.Id}|${logger}|${uppercase:${level}}|  ${message} ${exception}|url: ${aspnet-request-url}|action: ${aspnet-mvc-action}" />

     <!-- write to the void aka just remove -->
    <target xsi:type="Null" name="blackhole" />
  </targets>

  <!-- rules to map from logger name to target -->
  <rules>
    <!--All logs, including from Microsoft-->
    <logger name="*" minlevel="Trace" writeTo="allfile" />

    <!--Skip Microsoft logs and so log only own logs-->
    <logger name="Microsoft.*" minlevel="Trace" writeTo="blackhole" final="true" />
    <logger name="*" minlevel="Trace" writeTo="ownFile-web" />
  </rules>
</nlog>
```

More details of the config file are [here](https://github.com/NLog/NLog/wiki/Configuration-file).

If you like to include other targets or layout renderers, check the  [Platform support](platform-support) table, as there is a limited set implemented. Check the column `NetStandard  1.3`. To read more about  NetStandard, see [the docs from Microsoft](https://docs.microsoft.com/en-us/dotnet/articles/standard/library)

### 3. Enable copy to bin folder

Enable copy to bin folder for nlog.config

![image](https://cloud.githubusercontent.com/assets/5808377/25545456/394d6bda-2c5f-11e7-98d8-ccb214f795a2.png)


### 4. Update startup.cs
Add to your startup.cs

```c#
using NLog.Extensions.Logging;
using NLog.Web;

public Startup(IHostingEnvironment env)
{
    env.ConfigureNLog("nlog.config");
}

public void ConfigureServices(IServiceCollection Services)
{
    //call this in case you need aspnet-user-authtype/aspnet-user-identity
    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
}

// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{

    //add NLog to ASP.NET Core
    loggerFactory.AddNLog();

    //add NLog.Web
    app.AddNLogWeb();

   //note: remove the old loggerFactory, like loggerFactory.AddConsole and  loggerFactory.AddDebug

```

### 5. Write logs

Inject the ILogger in your controller:

```c#
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Index page says hello");
            return View();
        }

```

### 6. Example Output

When starting the ASP.NET Core website, we get two files:

#### nlog-own-2017-04-28.log

```
2017-04-28 22:00:20.6239|0|aspnet_core_example_vs2017.Controllers.HomeController|INFO|  Index page says hello |url: http://localhost/|action: Index

```

#### nlog-all-2017-04-28.log

```
2017-04-28 22:00:18.8092|3|Microsoft.AspNetCore.Hosting.Internal.WebHost|DEBUG|Hosting starting 
2017-04-28 22:00:19.0692|4|Microsoft.AspNetCore.Hosting.Internal.WebHost|DEBUG|Hosting started 
2017-04-28 22:00:19.1842|1|Microsoft.AspNetCore.Server.Kestrel|DEBUG|Connection id "0HL4EE4P3ORH2" started. 
2017-04-28 22:00:19.1842|1|Microsoft.AspNetCore.Server.Kestrel|DEBUG|Connection id "0HL4EE4P3ORH1" started. 
2017-04-28 22:00:19.4112|1|Microsoft.AspNetCore.Hosting.Internal.WebHost|INFO|Request starting HTTP/1.1 DEBUG http://localhost:54918/  0 
2017-04-28 22:00:19.4112|1|Microsoft.AspNetCore.Hosting.Internal.WebHost|INFO|Request starting HTTP/1.1 GET http://localhost:54918/   
2017-04-28 22:00:19.9028|4|Microsoft.AspNetCore.StaticFiles.StaticFileMiddleware|DEBUG|The request path / does not match a supported file type 
2017-04-28 22:00:19.9249|9|Microsoft.AspNetCore.Server.Kestrel|DEBUG|Connection id "0HL4EE4P3ORH2" completed keep alive response. 
2017-04-28 22:00:19.9909|2|Microsoft.AspNetCore.Hosting.Internal.WebHost|INFO|Request finished in 619.9536ms 200  
2017-04-28 22:00:20.3439|1|Microsoft.AspNetCore.Routing.RouteBase|DEBUG|Request successfully matched the route with name 'default' and template '{controller=Home}/{action=Index}/{id?}'. 
2017-04-28 22:00:20.5209|1|Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker|DEBUG|Executing action aspnet_core_example_vs2017.Controllers.HomeController.Index (aspnet-core-example-vs2017) 
2017-04-28 22:00:20.6129|1|Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker|INFO|Executing action method aspnet_core_example_vs2017.Controllers.HomeController.Index (aspnet-core-example-vs2017) with arguments ((null)) - ModelState is Valid 
2017-04-28 22:00:20.6239|0|aspnet_core_example_vs2017.Controllers.HomeController|INFO|Index page says hello 
2017-04-28 22:00:20.6559|2|Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker|DEBUG|Executed action method aspnet_core_example_vs2017.Controllers.HomeController.Index (aspnet-core-example-vs2017), returned result Microsoft.AspNetCore.Mvc.ViewResult. 
2017-04-28 22:00:20.8379|1|Microsoft.AspNetCore.Mvc.Razor.RazorViewEngine|DEBUG|View lookup cache miss for view 'Index' in controller 'Home'. 
2017-04-28 22:00:20.9049|1|Microsoft.AspNetCore.Mvc.Razor.Internal.RazorCompilationService|DEBUG|Code generation for the Razor file at '/Views/Home/Index.cshtml' started. 
2017-04-28 22:00:22.1469|2|Microsoft.AspNetCore.Mvc.Razor.Internal.RazorCompilationService|DEBUG|Code generation for the Razor file at '/Views/Home/Index.cshtml' completed in 1196.4305ms. 
2017-04-28 22:00:22.1739|1|Microsoft.AspNetCore.Mvc.Razor.Internal.DefaultRoslynCompilationService|DEBUG|Compilation of the generated code for the Razor file at '/Views/Home/Index.cshtml' started. 
2017-04-28 22:00:25.3189|2|Microsoft.AspNetCore.Mvc.Razor.Internal.DefaultRoslynCompilationService|DEBUG|Compilation of the generated code for the Razor file at '/Views/Home/Index.cshtml' completed in 3135.1505ms. 
2017-04-28 22:00:25.3419|1|Microsoft.AspNetCore.Mvc.Razor.Internal.RazorCompilationService|DEBUG|Code generation for the Razor file at '/Views/_ViewStart.cshtml' started. 
2017-04-28 22:00:25.3699|2|Microsoft.AspNetCore.Mvc.Razor.Internal.RazorCompilationService|DEBUG|Code generation for the Razor file at '/Views/_ViewStart.cshtml' completed in 11.4153ms. 
2017-04-28 22:00:25.3699|1|Microsoft.AspNetCore.Mvc.Razor.Internal.DefaultRoslynCompilationService|DEBUG|Compilation of the generated code for the Razor file at '/Views/_ViewStart.cshtml' started. 
2017-04-28 22:00:25.4709|2|Microsoft.AspNetCore.Mvc.Razor.Internal.DefaultRoslynCompilationService|DEBUG|Compilation of the generated code for the Razor file at '/Views/_ViewStart.cshtml' completed in 80.4722ms. 
2017-04-28 22:00:25.4879|2|Microsoft.AspNetCore.Mvc.ViewFeatures.Internal.ViewResultExecutor|DEBUG|The view 'Index' was found. 
2017-04-28 22:00:25.5029|1|Microsoft.AspNetCore.Mvc.ViewFeatures.Internal.ViewResultExecutor|INFO|Executing ViewResult, running view at path /Views/Home/Index.cshtml. 
2017-04-28 22:00:25.6519|0|Microsoft.Extensions.DependencyInjection.DataProtectionServices|INFO|User profile is available. Using 'C:\Users\j.verdurmen\AppData\Local\ASP.NET\DataProtection-Keys' as key repository and Windows DPAPI to encrypt keys at rest. 
2017-04-28 22:00:26.0065|1|Microsoft.AspNetCore.Mvc.Razor.RazorViewEngine|DEBUG|View lookup cache miss for view '_Layout' in controller 'Home'. 
2017-04-28 22:00:26.0845|1|Microsoft.AspNetCore.Mvc.Razor.Internal.RazorCompilationService|DEBUG|Code generation for the Razor file at '/Views/Shared/_Layout.cshtml' started. 
2017-04-28 22:00:26.3375|2|Microsoft.AspNetCore.Mvc.Razor.Internal.RazorCompilationService|DEBUG|Code generation for the Razor file at '/Views/Shared/_Layout.cshtml' completed in 208.8192ms. 
2017-04-28 22:00:26.3635|1|Microsoft.AspNetCore.Mvc.Razor.Internal.DefaultRoslynCompilationService|DEBUG|Compilation of the generated code for the Razor file at '/Views/Shared/_Layout.cshtml' started. 
2017-04-28 22:00:27.2465|2|Microsoft.AspNetCore.Mvc.Razor.Internal.DefaultRoslynCompilationService|DEBUG|Compilation of the generated code for the Razor file at '/Views/Shared/_Layout.cshtml' completed in 832.6316ms. 
2017-04-28 22:00:27.7265|2|Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker|INFO|Executed action aspnet_core_example_vs2017.Controllers.HomeController.Index (aspnet-core-example-vs2017) in 7197.8417ms 
2017-04-28 22:00:28.0845|9|Microsoft.AspNetCore.Server.Kestrel|DEBUG|Connection id "0HL4EE4P3ORH1" completed keep alive response. 
2017-04-28 22:00:28.1455|2|Microsoft.AspNetCore.Hosting.Internal.WebHost|INFO|Request finished in 8762.0557ms 200 text/html; charset=utf-8 


```

