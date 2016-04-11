[Understanding Action Results](http://www.asp.net/mvc/overview/older-versions-1/controllers-and-routing/aspnet-mvc-controllers-overview-cs)

A controller action returns something called an action result. An action result is what a controller action returns in response to a browser request.

The ASP.NET MVC framework supports several types of action results including:

1. ViewResult - Represents HTML and markup.
2. EmptyResult - Represents no result.
3. RedirectResult - Represents a redirection to a new URL.
4. JsonResult - Represents a JavaScript Object Notation result that can be used in an AJAX application.
5. JavaScriptResult - Represents a JavaScript script.
6. ContentResult - Represents a text result.
7. FileContentResult - Represents a downloadable file (with the binary content).
8. FilePathResult - Represents a downloadable file (with a path).
9. FileStreamResult - Represents a downloadable file (with a file stream).

All of these action results inherit from the base *ActionResult* class. 

ActionResult是一个抽象类型，最终的请求响应实现在抽象方法ExecuteResult方法中。

``` C#
public abstract class ActionResult
{    
    //其他成员
    public abstract void ExecuteResult(ControllerContext context);
}
```

When an action returns a ViewResult, HTML is returned to the browser. 
The Index() method in Listing 2 returns a view named Index to the browser.

Notice that the Index() action in Listing 2 does not return a ViewResult(). 
Instead, the View() method of the Controller base class is called. Normally, 
you do not return an action result directly. Instead, you call one of the following methods of the Controller base class:

1. View - Returns a ViewResult action result.
2. Redirect - Returns a RedirectResult action result.
3. RedirectToAction - Returns a RedirectToRouteResult action result.
4. RedirectToRoute - Returns a RedirectToRouteResult action result.
5. Json - Returns a JsonResult action result.
6. JavaScriptResult - Returns a JavaScriptResult.
7. Content - Returns a ContentResult action result.
8. File - Returns a FileContentResult, FilePathResult, or FileStreamResult depending on the parameters passed to the method.
