[View components](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components)

## Introducing view components

New to ASP.NET Core MVC, view components are similar to partial views, but they are much more powerful. View components don’t use model binding, and only depend on the data you provide when calling into it. A view component:


* Renders a chunk rather than a whole response

* Includes the same separation-of-concerns and testability benefits found between a controller and view

* Can have parameters and business logic

* Is typically invoked from a layout page

View components are intended anywhere you have reusable rendering logic that is too complex for a partial view, such as:


* Dynamic navigation menus

* Tag cloud (where it queries the database)

* Login panel

* Shopping cart

* Recently published articles

* Sidebar content on a typical blog

* A login panel that would be rendered on every page and show either the links to log out or log in, depending on the
* log in state of the user


A view component consists of two parts: the class (typically derived from ViewComponent) and the result it returns (typically a view). Like controllers, a view component can be a POCO, but most developers will want to take advantage of the methods and properties available by deriving from ViewComponent.

## Creating a view component


### Create the view component Razor view