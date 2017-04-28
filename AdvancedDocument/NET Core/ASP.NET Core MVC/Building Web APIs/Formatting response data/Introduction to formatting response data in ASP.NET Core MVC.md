[Introduction to formatting response data in ASP.NET Core MVC](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/formatting)


## Format-Specific Action Results

Some action result types are specific to a particular format, such as `JsonResult` and `ContentResult`. Actions can return specific results that are always formatted in a particular manner. For example, returning a JsonResult will return JSON-formatted data, regardless of client preferences. Likewise, returning a ContentResult will return plain-text-formatted string data (as will simply returning a string).


## Configuring Formatters