[Introduction to Open Web Interface for .NET (OWIN)](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/owin)


## Running OWIN middleware in the ASP.NET pipeline

ASP.NET Core's OWIN support is deployed as part of the Microsoft.AspNetCore.Owin package. You can import OWIN support into your project by installing this package.

OWIN middleware conforms to the OWIN specification, which requires a `Func<IDictionary<string, object>, Task>` interface, and specific keys be set (such as `owin.ResponseBody`). 