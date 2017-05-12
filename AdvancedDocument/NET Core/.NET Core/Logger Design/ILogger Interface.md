[ILogger Interface ](https://docs.microsoft.com/en-us/aspnet/core/api/microsoft.extensions.logging.ilogger#Microsoft_Extensions_Logging_ILogger_BeginScope__1___0_)

Represents a type used to perform logging.
Syntax
Declaration

public interface ILogger

Remarks
Aggregates most logging patterns to a single method.
Methods summary

> `BeginScope`头一次见到这种设计，可以结构化日志。

```
BeginScope<TState>(TState)
	Begins a logical operation scope.
IsEnabled(LogLevel)
	Checks if the given logLevel is enabled.
Log<TState>(LogLevel, EventId, TState, Exception, Func<TState, Exception, String>)
	Writes a log entry. 
```    