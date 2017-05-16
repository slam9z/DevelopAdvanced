[How can you access RouteData from the code-behind?](http://stackoverflow.com/questions/976855/how-can-you-access-routedata-from-the-code-behind)

##Question

When using ASP.Net routing, how can you get the RouteData from the code-behind?
I know you can get it from the GetHttpHander method of the RouteHandler (you get handed the RequestContext), but can you get
 this from the code-behind?
Is there anything like...

```cs
RequestContext.Current.RouteData.Values["whatever"];
```

...that you can access globally, like you can do with HttpContext?
Or is it that RouteData is only meant to be accessed from inside the RouteHandler?


##Answer

You can use the following:

```cs
RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
```