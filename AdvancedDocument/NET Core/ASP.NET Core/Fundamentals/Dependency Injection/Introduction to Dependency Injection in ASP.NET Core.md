[Introduction to Dependency Injection in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)

## What is Dependency Injection?

Dependency injection (DI) is a technique for achieving loose coupling between objects and their collaborators, or dependencies. Rather than directly instantiating collaborators, or using static references, the objects a class needs in order to perform its actions are provided to the class in some fashion. Most often, classes will declare their dependencies via their constructor, allowing them to follow the Explicit Dependencies Principle. This approach is known as "constructor injection".


When a system is designed to use DI, with many classes requesting their dependencies via their constructor (or properties), it's helpful to have a class dedicated to creating these classes with their associated dependencies. These classes are referred to as containers, or more specifically, Inversion of Control (IoC) containers or Dependency Injection (DI) containers. 


## Constructor Injection Behavior

Constructors can accept arguments that are not provided by dependency injection, but these must support default values.


## Registering Your Own Services

The `AddTransient` method is used to map abstract types to concrete services that are instantiated separately for every object that requires it. 


```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase()
    );

    // Add framework services.
    services.AddMvc();

    // Register application services.
    services.AddScoped<ICharacterRepository, CharacterRepository>();
    services.AddTransient<IOperationTransient, Operation>();
    services.AddScoped<IOperationScoped, Operation>();
    services.AddSingleton<IOperationSingleton, Operation>();
    services.AddSingleton<IOperationSingletonInstance>(new Operation(Guid.Empty));
    services.AddTransient<OperationService, OperationService>();
}
```

Entity Framework contexts should be added to the services container using the `Scoped `lifetime. This is taken care of automatically if you use the helper methods as shown above. Repositories that will make use of Entity Framework should use the same lifetime.


## Service Lifetimes and Registration Options

ASP.NET services can be configured with the following lifetimes:

### Transient

Transient lifetime services are created each time they are requested. This lifetime works best for lightweight, stateless services.

### Scoped

Scoped lifetime services are created once per request.

### Singleton

Singleton lifetime services are created the first time they are requested (or when ConfigureServices is run if you specify an instance there) and then every subsequent request will use the same instance. If your application requires singleton behavior, allowing the services container to manage the service's lifetime is recommended instead of implementing the singleton design pattern and managing your object's lifetime in the class yourself.


## Replacing the default services container



## Recommendations

When working with dependency injection, keep the following recommendations in mind:


* DI is for objects that have complex dependencies. Controllers, services, adapters, and repositories are all examples of objects that might be added to DI.

* Avoid storing data and configuration directly in DI. For example, a user's shopping cart shouldn't typically be added to the services container. Configuration should use the Options Model. Similarly, avoid "data holder" objects that only exist to allow access to some other object. It's better to request the actual item needed via DI, if possible.

* Avoid static access to services.

* Avoid service location in your application code.

* Avoid static access to HttpContext.
