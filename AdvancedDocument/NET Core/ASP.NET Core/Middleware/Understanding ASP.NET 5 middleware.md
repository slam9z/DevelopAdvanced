[Understanding ASP.NET 5 middleware](https://dzimchuk.net/understanding-aspnet-5-middleware/)

We have just used the simplest overload of the Run method that accepts a RequestDelegate.

```cs
public delegate Task RequestDelegate(HttpContext context);
```