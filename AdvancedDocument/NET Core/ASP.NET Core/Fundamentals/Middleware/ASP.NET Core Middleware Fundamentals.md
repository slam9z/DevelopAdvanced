[ASP.NET Core Middleware Fundamentals](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware)


## What is middleware

Middleware is software that is assembled into an application pipeline to handle requests and responses. Each component chooses whether to pass the request on to the next component in the pipeline, and can perform certain actions before and after the next component is invoked in the pipeline. Request delegates are used to build the request pipeline. The request delegates handle each HTTP request.

Request delegates are configured using Run, Map, and Use extension methods on the IApplicationBuilder instance that is passed into the Configure method in the Startup class. An individual request delegate can be specified in-line as an anonymous method (called in-line middleware), or it can be defined in a reusable class. These reusable classes and in-line anonymous methods are middleware, or middleware components. Each middleware component in the request pipeline is responsible for invoking the next component in the pipeline, or short-circuiting the chain if appropriate.


## Creating a middleware pipeline with IApplicationBuilder


### Ordering

The order that middleware components are added in the `Configure` method defines the order in which they are invoked on requests, and the reverse order for the response. This ordering is critical for security, performance, and functionality.

### Run, Map, and Use

You configure the HTTP pipeline using Run, Map, and Use. 

The Run method short-circuits the pipeline (that is, it does not call a next request delegate). Run is a convention, and some middleware components may expose Run[Middleware] methods that run at the end of the pipeline.

Map* extensions are used as a convention for branching the pipeline. Map branches the request pipeline based on matches of the given request path. If the request path starts with the given path, the branch is executed.


### Built-in middleware


## Comments

"Do not call next.Invoke after calling a write method. A middleware component either produces a response or calls next.Invoke, but not both."

> 这确实是疑问

