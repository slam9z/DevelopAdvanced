[Migrating HTTP handlers and modules to ASP.NET Core middleware](https://docs.microsoft.com/en-us/aspnet/core/migration/http-modules)

## Modules and handlers revisited

Before proceeding to ASP.NET Core middleware, let's first recap how HTTP modules and handlers work:



### Handlers are:


* Classes that implement IHttpHandler

* Used to handle requests with a given file name or extension, such as .report

* Configured in Web.config

### Modules are:


* Classes that implement IHttpModule

* Invoked for every request

* Able to short-circuit (stop further processing of a request)

* Able to add to the HTTP response, or create their own

* Configured in Web.config

### The order in which modules process incoming requests is determined by:

* The application life cycle, which is a series events fired by ASP.NET: BeginRequest, AuthenticateRequest, etc. Each module can create a handler for one or more events.

* For the same event, the order in which they are configured in Web.config.

In addition to modules, you can add handlers for the life cycle events to your Global.asax.cs file. These handlers run after the handlers in the configured modules.

## From handlers and modules to middleware

### Middleware are simpler than HTTP modules and handlers:


* Modules, handlers, Global.asax.cs, Web.config (except for IIS configuration) and the application life cycle are gone

* The roles of both modules and handlers have been taken over by middleware

* Middleware are configured using code rather than in Web.config

* Pipeline branching lets you send requests to specific middleware, based on not only the URL but also on request headers,query strings, etc.

### Middleware are very similar to modules:

* Invoked in principle for every request
* Able to short-circuit a request, by not passing the request to the next middleware
* Able to create their own HTTP response

### Middleware and modules are processed in a different order:


* Order of middleware is based on the order in which they are inserted into the request pipeline, while order of modules is mainly based on application life cycle events

* Order of middleware for responses is the reverse from that for requests, while order of modules is the same for requests and responses

* See Creating a middleware pipeline with IApplicationBuilder
