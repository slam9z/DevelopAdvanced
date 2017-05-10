[Response Caching](https://docs.microsoft.com/en-us/aspnet/core/performance/caching/response)

## What is Response Caching

Response caching adds cache-related headers to responses. These headers specify how you want client, proxy and middleware to cache responses. Response caching can reduce the number of requests a client or proxy makes to the web server. Response caching can also reduce the amount of work the web server performs to generate the response.

The primary HTTP header used for caching is `Cache-Control`. See the HTTP 1.1 Caching for more information. Common cache directives:

* public
* private
* no-cache
* Pragma
* Vary

The web server can cache responses by adding the response caching middleware. See Response caching middleware for more information.