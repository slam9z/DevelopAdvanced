[Dependency injection into controllers](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/dependency-injection)


ASP.NET Core MVC controllers should request their dependencies explicitly via their constructors. In some instances, individual controller actions may require a service, and it may not make sense to request at the controller level. In this case, you can also choose to inject a service as a parameter on the action method.


## Constructor Injection


## Action Injection with FromServices

Sometimes you don't need a service for more than one action within your controller. In this case, it may make sense to inject the service as a parameter to the action method. This is done by marking the parameter with the attribute  `[FromServices]` as shown here:


## Accessing Settings from a Controller

Accessing application or configuration settings from within a controller is a common pattern. This access should use the Options pattern described in configuration. You generally should not request settings directly from your controller using dependency injection. A better approach is to request an   `IOptions<T>` instance, where T is the configuration class you need.


Following the Options pattern allows settings and configuration to be decoupled from one another, and ensures the controller is following separation of concerns, since it doesn't need to know how or where to find the settings information. It also makes the controller easier to unit test Testing Controller Logic, since there is no static cling or direct instantiation of settings classes within the controller class.
