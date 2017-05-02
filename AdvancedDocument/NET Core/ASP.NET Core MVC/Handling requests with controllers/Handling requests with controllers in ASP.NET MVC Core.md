[Handling requests with controllers in ASP.NET MVC Core](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/actions)


Controllers, actions, and action results are a fundamental part of how developers build apps using ASP.NET MVC Core.

## What is a Controller

In ASP.NET MVC, a Controller is used to define and group a set of actions. An action (or action method) is a method on a controller that handles incoming requests. Controllers provide a logical means of grouping similar actions together, allowing common sets of rules (e.g. routing, caching, authorization) to be applied collectively. Incoming requests are mapped to actions through routing.

In ASP.NET Core MVC, a controller can be any instantiable class that ends in "Controller" or inherits from a class that ends with "Controller". Controllers should follow the Explicit Dependencies Principle and request any dependencies their actions require through their constructor using dependency injection.

By convention, controller classes:


* Are located in the root-level "Controllers" folder

* Inherit from Microsoft.AspNetCore.Mvc.Controller


These two conventions are not required.

Within the Model-View-Controller pattern, a Controller is responsible for the initial processing of the request and instantiation of the Model. Generally, business decisions should be performed within the Model.


> *Note*  The Model should be a Plain Old CLR Object (POCO), not a DbContext or database-related type.

The controller takes the result of the model's processing (if any), returns the proper view along with the associated view data. Learn more: Overview of ASP.NET Core MVC and Getting started with ASP.NET Core MVC and Visual Studio.


> *Tip*  The Controller is a UI level abstraction. Its responsibility is to ensure incoming request data is valid and to choose which view (or result for an API) should be returned. In well-factored apps it will not directly include data access or business logic, but instead will delegate to services handling these responsibilities.

## Defining Actions

Any public method on a controller type is an action. Parameters on actions are bound to request data and validated using model binding.

> *Warning*   Action methods that accept parameters should verify the ModelState.IsValid property is true.

Action methods should contain logic for mapping an incoming request to a business concern. Business concerns should typically be represented as services that your controller accesses through dependency injection. Actions then map the result of the business action to an application state. Actions can return anything, but frequently will return an instance of `IActionResult` (or `Task<IActionResult>` for async methods) that produces a response. The action method is responsible for choosing what kind of response; the action result does the responding.


## Controller Helper Methods

Although not required, most developers will want to have their controllers inherit from the base `Controller` class. Doing so provides controllers with access to many properties and helpful methods, including the following helper methods designed to assist in returning various responses: