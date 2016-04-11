[Routing and Action Selection in ASP.NET Web API](http://www.asp.net/web-api/overview/web-api-routing-and-actions/routing-and-action-selection)

Routing has three main phases:

1. Matching the URI to a route template.
2. Selecting a controller.
3. Selecting an action.

##Route Templates

A route template looks similar to a URI path, but it can have placeholder values, indicated with curly braces:

``` C#
"api/{controller}/public/{category}/{id}"
```

When you create a route, you can provide default values for some or all of the placeholders:

``` C#
defaults: new { category = "all" }
```

You can also provide constraints, which restrict how a URI segment can match a placeholder:

``` C#
constraints: new { id = @"\d+" }   // Only matches if "id" is one or more digits.
```

There are two special placeholders: "{controller}" and "{action}".

* "{controller}" provides the name of the controller.
* "{action}" provides the name of the action. In Web API, the usual convention is to omit "{action}".

##Defaults


##Route Dictionary

If the framework finds a match for a URI, it creates a dictionary that contains the value for each placeholder. 
The keys are the placeholder names, not including the curly braces. The values are taken from the URI path or from the defaults. 
The dictionary is stored in the IHttpRouteData object.




##Selecting a Controller

Controller selection is handled by the IHttpControllerSelector.SelectController method. This method takes an HttpRequestMessage 
instance and returns an HttpControllerDescriptor. The default implementation is provided by the DefaultHttpControllerSelector 
class. This class uses a straightforward algorithm: 

1. Look in the route dictionary for the key "controller". 
2. Take the value for this key and append the string "Controller" to get the controller type name.
3. Look for a Web API controller with this type name. 



##Action Selection

After selecting the controller, the framework selects the action by calling the IHttpActionSelector.SelectAction method.
 This method takes an HttpControllerContext and returns an HttpActionDescriptor.

The default implementation is provided by the ApiControllerActionSelector class. To select an action, it looks at the following:

* The HTTP method of the request.
* The "{action}" placeholder in the route template, if present.
* The parameters of the actions on the controller.


