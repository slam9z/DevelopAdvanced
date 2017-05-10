[Working with a distributed cache](https://docs.microsoft.com/en-us/aspnet/core/performance/caching/distributed)

Distributed caches can improve the performance and scalability of ASP.NET Core apps, especially when hosted in a cloud or server farm environment. This article explains how to work with ASP.NET Core's built-in distributed cache abstractions and implementations.


## What is a Distributed Cache

A distributed cache is shared by multiple app servers (see Caching Basics). The information in the cache is not stored in the memory of individual web servers, and the cached data is available to all of the app's servers. This provides several advantages:

* Cached data is coherent on all web servers. Users don't see different results depending on which web server handles their request

* Cached data survives web server restarts and deployments. Individual web servers can be removed or added without impacting the cache.

* The source data store has fewer requests made to it (than with multiple in-memory caches or no cache at all).




Like any cache, a distributed cache can dramatically improve an app's responsiveness, since typically data can be retrieved from the cache much faster than from a relational database (or web service).

Cache configuration is implementation specific. This article describes how to configure both Redis and SQL Server distributed caches. Regardless of which implementation is selected, the app interacts with the cache using a common IDistributedCache interface.

## The IDistributedCache Interface

The IDistributedCache interface includes synchronous and asynchronous methods. The interface allows items to be added, retrieved, and removed from the distributed cache implementation. The IDistributedCache interface includes the following methods:

Get, GetAsync

Takes a string key and retrieves a cached item as a byte[] if found in the cache.

Set, SetAsync

Adds an item (as byte[]) to the cache using a string key.

Refresh, RefreshAsync

Refreshes an item in the cache based on its key, resetting its sliding expiration timeout (if any).

Remove, RemoveAsync

Removes a cache entry based on its key.

To use the IDistributedCache interface:

    Add the required NuGet packages to your project file.

    Configure the specific implementation of IDistributedCache in your Startup class's ConfigureServices method, and add it to the container there.

    From the app's `Middleware or MVC controller classes, request an instance of IDistributedCache from the constructor. The instance will be provided by Dependency Injection (DI).


## Using a Redis Distributed Cache

Redis is an open source in-memory data store, which is often used as a distributed cache. You can use it locally, and you can configure an Azure Redis Cache for your Azure-hosted ASP.NET Core apps. Your ASP.NET Core app configures the cache implementation using a RedisDistributedCache instance.


## Using a SQL Server Distributed Cache

The SqlServerCache implementation allows the distributed cache to use a SQL Server database as its backing store. To create SQL Server table you can use sql-cache tool, the tool creates a table with the name and schema you specify.
