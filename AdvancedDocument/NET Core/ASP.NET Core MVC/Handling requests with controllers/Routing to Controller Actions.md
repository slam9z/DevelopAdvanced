[Routing to Controller Actions](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing)


## Setting up Routing Middleware


## Conventional routing

The default route:

```cs
routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
```

is an example of a conventional routing. We call this style conventional routing because it establishes a convention for URL paths:


* the first path segment maps to the controller name

* the second maps to the action name.

* the third segment is used for an optional id used to map to a model entity


## Multiple routes

### Disambiguating actions


If multiple routes match, and MVC can't find a 'best' route, it will throw an `AmbiguousActionException`.


## Token replacement in route templates ([controller], [action], [area])

For convenience, attribute routes support token replacement by enclosing a token in square-braces ([, ]]). The tokens [action], [area], and [controller] will be replaced with the values of the action name, area name, and controller name from the action where the route is defined. In this example the actions can match URL paths as described in the comments:

```cs
[Route("[controller]/[action]")]
public class ProductsController : Controller
{
    [HttpGet] // Matches '/Products/List'
    public IActionResult List() {
        // ...
    }

    [HttpGet("{id}")] // Matches '/Products/Edit/{id}'
    public IActionResult Edit(int id) {
        // ...
    }
}
```


## URL Generation

The `IUrlHelper` interface is the underlying piece of infrastructure between MVC and routing for URL generation. You'll find an instance of IUrlHelper available through the `Url` property in controllers, views, and view components.


## Areas

> 可以用这个实现V1和V2。Multiple routes就可以了吧



