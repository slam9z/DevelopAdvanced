[Attribute Routing in ASP.NET Web API 2](http://www.asp.net/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2)


##Why Attribute Routing?

The first release of Web API used convention-based routing. In that type of routing, you define one or more route templates,
 which are basically parameterized strings. 

Here are some other patterns that attribute routing makes easy.

* API versioning

* Overloaded URI segments

* Mulitple parameter types

##Enabling Attribute Routing

``` C#
public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        // Web API routes
        config.MapHttpAttributeRoutes();

        // Other Web API configuration not shown.
    }
}

```



##Adding Route Attributes


##Route Constraints

Route constraints let you restrict how the parameters in the route template are matched. 
The general syntax is "{parameter:constraint}". 


##Custom Route Constraints

##Optional URI Parameters and Default Values

##Route Names

 


