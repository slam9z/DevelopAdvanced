```cs 
System.InvalidOperationException occurred
  HResult=0x80131509
  Message=No storage mechanism for clients specified. Use the '' extension method to register a development version.
  Source=<Cannot evaluate the exception source>
  StackTrace:
   at Microsoft.AspNetCore.Builder.IdentityServerApplicationBuilderExtensions.TestService(IServiceProvider serviceProvider, Type service, ILogger logger, String message, Boolean doThrow)
   at Microsoft.AspNetCore.Builder.IdentityServerApplicationBuilderExtensions.Validate(IApplicationBuilder app)
   at Microsoft.AspNetCore.Builder.IdentityServerApplicationBuilderExtensions.UseIdentityServer(IApplicationBuilder app)
   at Rwby.Global.Api.Startup.Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) in D:\Source\MyGithub\Rwby\src\Rwby.Global\Rwby.Global.Api\Startup.cs:line 72
```