[Routing in ASP.NET Web API](http://www.asp.net/web-api/overview/web-api-routing-and-actions/routing-in-aspnet-web-api)

##Routing Tables

In ASP.NET Web API, a controller is a class that handles HTTP requests. The public methods of the controller are called 
action methods or simply actions. When the Web API framework receives a request, it routes the request to an action. 


*Note*: The reason for using "api" in the route is to avoid collisions with ASP.NET MVC routing. That way, you can have "/contacts" go to an MVC controller, 
and "/api/contacts" go to a Web API controller. Of course, if you don't like this convention, you can change the default route table.


Notice that the {id} segment of the URI, if present, is mapped to the id parameter of the action.
 In this example, the controller defines two GET methods, one with an id parameter and one with no parameters.


##Routing Variations

###HTTP Methods
HttpGet, HttpPut,  HttpPost, or HttpDelete attribute.


AcceptVerbs attribute

```C#
public class ProductsController : ApiController
{
    [AcceptVerbs("GET", "HEAD")]
    public Product FindProduct(id) { }

    // WebDAV method
    [AcceptVerbs("MKCOL")]
    public void MakeCollection() { }
}
```

###Routing by Action Name


###Non-Actions

```C#
[NonAction]  
public string GetPrivateData() { ... }
```


