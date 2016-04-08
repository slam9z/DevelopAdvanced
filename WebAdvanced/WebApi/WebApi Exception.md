## Action result 

###Question
http://localhost:3000/api/ActionResults/GetText/

{"Message":"An error has occurred.","ExceptionMessage":"Multiple actions were found that match the request: \r\n
Get on type WebShop.Controllers.ActionResultsController\r\n
GetText on type WebShop.Controllers.ActionResultsController\r\n
GetAccount on type WebShop.Controllers.ActionResultsController","ExceptionType":"System.InvalidOperationException","StackTrace":"   
at System.Web.Http.Controllers.ApiControllerActionSelector.ActionSelectorCacheItem.SelectAction(HttpControllerContext controllerContext)\r\n 
at System.Web.Http.Controllers.ApiControllerActionSelector.SelectAction(HttpControllerContext controllerContext)\r\n  
at System.Web.Http.ApiController.ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)\r\n   
at System.Web.Http.Dispatcher.HttpControllerDispatcher.<SendAsync>d__1.MoveNext()"}

``` C#
public IHttpActionResult GetText()
{
	return new TextResult("hello", Request);
}


public Account GetAccount()
{
	return new Account() { UserName = "Test" };
}
```


###Solution

Your route map is probably something like this:

``` C#
routes.MapHttpRoute(
name: "API Default",
routeTemplate: "api/{controller}/{id}",
defaults: new { id = RouteParameter.Optional });
```

But in order to have multiple actions with the same http method you need to provide webapi with more information via the route like so:

```C#
routes.MapHttpRoute(
name: "API Default",
routeTemplate: "api/{controller}/{action}/{id}",
defaults: new { id = RouteParameter.Optional });
```

Notice that the routeTemplate now includes an action. Lots more info here: http://www.asp.net/web-api/overview/web-api-routing-and-actions/routing-in-aspnet-web-api
