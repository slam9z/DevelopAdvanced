[How to map WebAPI routes correctly](http://stackoverflow.com/questions/36274985/how-to-map-webapi-routes-correctly)

## question

I'm building an API for a Twitter like site using Web API and have trouble with mapping the routes

I have the following actions for the User controller:

```cs
public User Get(string firstname, string lastname)
public User Get(Guid id)
public User Friends(Guid id)
public User Followers(Guid id)
public User Favorites(Guid id)
```

The desired routes and the generated documentation should be:

```
api/users?firstname={firstname}&lastname={lastname}
api/users/{id}
api/users/{id}/friends
api/users/{id}/followers
api/users/{id}/favorites
```

In WebApiConfig.cs I have:

```cs
config.Routes.MapHttpRoute(
    "2",
    "api/{controller}/{id}",
    new { action = "get", id = RouteParameter.Optional }
);


config.Routes.MapHttpRoute(
     "1",
     "api/{controller}/{id}/{action}"
);
```
How can I map WebAPI routes correctly?
c# asp.net asp.net-mvc asp.net-web-api asp.net-web-api-routing

## answer1

Given the flexibility you want you should take a look at

Attribute Routing in ASP.NET Web API 2

In WebApiConfig.cs enable attribute routing like

```cs
// Web API routes
config.MapHttpAttributeRoutes();
```
In UserController

Note given the names of actions Friends, Followers and Favorites they imply returning collections rather than single user

```cs
[RoutePrefix("api/users")]
public class UserController: ApiController {

    //eg: GET api/users?firstname={firstname}&lastname={lastname}
    [HttpGet]
    [Route("")]
    public User Get([FromUri]string firstname,[FromUri] string lastname) {...}

    //eg: GET api/users/{id}
    [HttpGet]
    [Route("{id:guid}")]
    public User Get(Guid id){...}

    //eg: GET api/users/{id}/friends
    [HttpGet]
    [Route("{id:guid}/friends")]
    public IEnumerable<User> Friends(Guid id){...}

    //eg: GET api/users/{id}/followers
    [HttpGet]
    [Route("{id:guid}/followers")]
    public IEnumerable<User> Followers(Guid id){...}

    //eg: GET api/users/{id}/favorites
    [HttpGet]
    [Route("{id:guid}/favorites")]
    public IEnumerable<User> Favorites(Guid id){...}
}
```

## answer2
	

Routing is order sensitive. The first match always wins. So it is important that you order you routes from most-specific to least-specific.

```cs
// All parameters are required, or it won't match.
// So it will only match URLs 4 segments in length
// starting with /api.
config.Routes.MapHttpRoute(
     "1",
     "api/{controller}/{id}/{action}"
);

// Controller is required, id is optional.
// So it will match any URL starting with
// /api that is 2 or 3 segments in length.
config.Routes.MapHttpRoute(
    "2",
    "api/{controller}/{id}",
    new { action = "get", id = RouteParameter.Optional }
);
```

When your routes are ordered this way, you will get the behavior you expect.
shareimprove this answer
	
## answer3
	

There are a variety of useful reference materials on this subject, such as:

    Routing Basics with Web-API
    Web-API Routing for multiple methods
    Routing in Asp.net Mvc 4 and Web Api

Have you had a look at these?

Update..

Its better practise to explicitly state which parameter is which, ie:

```cs
    config.Routes.MapHttpRoute(
        name: "2",
        routeTemplate: "api/{controller}/{id}",
        defaults: new { action = "Get", id = RouteParameter.Optional },
    );

    config.Routes.MapHttpRoute(
        name: "1",
        routeTemplate: "api/{controller}/{action}/{id}",
        defaults: null
    );
```
