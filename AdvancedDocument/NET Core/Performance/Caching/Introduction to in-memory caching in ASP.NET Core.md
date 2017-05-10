[Introduction to in-memory caching in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/performance/caching/memory)

## Caching basics

Caching can significantly improve the performance and scalability of an app by reducing the work required to generate content. Caching works best with data that changes infrequently. Caching makes a copy of data that can be returned much faster than from the original source. You should write and test your app to never depend on cached data.

ASP.NET Core supports several different caches. The simplest cache is based on the IMemoryCache, which represents a cache stored in the memory of the web server. Apps which run on a server farm of multiple servers should ensure that sessions are sticky when using the in-memory cache. Sticky sessions ensure that subsequent requests from a client all go to the same server. For example, Azure Web apps use Application Request Routing (ARR) to route all subsequent requests to the same server.

Non-sticky sessions in a web farm require a distributed cache to avoid cache consistency problems. For some apps, a distributed cache can support higher scale out than an in-memory cache. Using a distributed cache offloads the cache memory to an external process.

The IMemoryCache cache will evict cache entries under memory pressure unless the cache priority is set to CacheItemPriority.NeverRemove. You can set the CacheItemPriority to adjust the priority the cache evicts items under memory pressure.

The in-memory cache can store any object; the distributed cache interface is limited to byte[].


## Using IMemoryCache

In-memory caching is a service that is referenced from your app using Dependency Injection. Call `AddMemoryCache` in ConfigureServices:

