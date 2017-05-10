[Dependency injection into views](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/dependency-injection)


ASP.NET Core supports dependency injection into views. This can be useful for view-specific services, such as localization or data required only for populating view elements. You should try to maintain separation of concerns between your controllers and views. Most of the data your views display should be passed in from the controller.

## A Simple Example

You can inject a service into a view using the @inject directive. You can think of @inject as adding a property to your view, and populating the property using DI.

The syntax for `@inject: @inject <type> <name>`

An example of @inject in action:

