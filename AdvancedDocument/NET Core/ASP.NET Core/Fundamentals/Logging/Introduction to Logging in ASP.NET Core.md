[Introduction to Logging in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging)

ASP.NET Core supports a logging API that works with a variety of logging providers. Built-in providers let you send logs to one or more destinations, and you can plug in a third-party logging framework. This article shows how to use the built-in logging API and providers in your code.


> 这个log比我自己之前做的多了provider，基本都要强。


## How to add providers

Each extension method calls the `ILoggerFactory.AddProvider` method, passing in an instance of the provider. 


## Log category

```cs
public class TodoController : Controller
{
    private readonly ITodoRepository _todoRepository;
    private readonly ILogger _logger;

    public TodoController(ITodoRepository todoRepository,
        ILogger<TodoController> logger)
    {
        _todoRepository = todoRepository;
        _logger = logger;
    }
```    

## Log level


## Log event ID

Each time you write a log, you can specify an event ID. The sample app does this by using a **locally-defined LoggingEvents** class:

## Logging exceptions

## Log filtering

## Log scopes

## Third-party logging providers




