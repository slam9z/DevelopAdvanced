[Routing in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing)

Routing functionality is responsible for mapping an incoming request to a route handler. Routes are defined in the ASP.NET app and configured when the app starts up. A route can optionally extract values from the URL contained in the request, and these values can then be used for request processing. Using route information from the ASP.NET app, the routing functionality is also able to generate URLs that map to route handlers. Therefore, routing can find a route handler based on a URL, or the URL corresponding to a given route handler based on route handler information.


## Routing basics

Routing uses routes (implementations of IRouter) to:


* map incoming requests to route handlers

* generate URLs used in responses


## Using Routing Middleware

Add the NuGet package "Microsoft.AspNetCore.Routing".

Add routing to the service container in Startup.cs:

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddRouting();
}
```

Routes must configured in the Configure method in the Startup class. The sample below uses these APIs:


* RouteBuilder

* Build

* MapGet Matches only HTTP GET requests

* UseRouter
