[Filtering in ASP.NET MVC](https://msdn.microsoft.com/en-us/library/gg416513(VS.98).aspx)

In ASP.NET MVC, controllers define action methods that usually have a one-to-one relationship with possible user interactions,
 such as clicking a link or submitting a form. For example, when the user clicks a link, 
a request is routed to the designated controller, and the corresponding action method is called.

Sometimes you want to perform logic either before an action method is called or after an 
action method runs. To support this, ASP.NET MVC provides filters. Filters are custom classes 
that provide both a declarative and programmatic means to add pre-action and post-action behavior
to controller action methods.


##ASP.NET MVC Filter Types

ASP.NET MVC supports the following types of action filters:

* Authorization filters. These implement *IAuthorizationFilter* and make security decisions about whether to execute an action method,
 such as performing authentication or validating properties of the request. The AuthorizeAttribute class and the RequireHttpsAttribute 
class are examples of an authorization filter. Authorization filters run before any other filter.

* Action filters. These implement *IActionFilter* and wrap the action method execution. The IActionFilter interface declares two methods: 
OnActionExecuting and OnActionExecuted. OnActionExecuting runs before the action method. OnActionExecuted runs after the action method
 and can perform additional processing, such as providing extra data to the action method, inspecting the return value, or canceling execution of the action method.

* Result filters. These implement *IResultFilter* and wrap execution of the ActionResult object. IResultFilter declares two methods: 
OnResultExecuting and OnResultExecuted. OnResultExecuting runs before the ActionResult object is executed. OnResultExecuted runs 
after the result and can perform additional processing of the result, such as modifying the HTTP response. The OutputCacheAttribute 
class is one example of a result filter.

* Exception filters. These implement *IExceptionFilter* and execute if there is an unhandled exception thrown during the execution 
of the ASP.NET MVC pipeline. Exception filters can be used for tasks such as logging or displaying an error page. The HandleErrorAttribute
 class is one example of an exception filter.


You can implement the following On<Filter> methods in a controller:

* OnAuthorization

* OnException

* OnActionExecuting

* OnActionExecuted

* OnResultExecuting

* OnResultExecuted

##Filters Provided in ASP.NET MVC

ASP.NET MVC includes the following filters, which are implemented as attributes. The filters can be applied at the action method, controller, or application level.

* AuthorizeAttribute. Restricts access by authentication and optionally authorization. 

* HandleErrorAttribute. Specifies how to handle an exception that is thrown by an action method. 

NoteNote:

This filter does not catch exceptions unless the customErrors element is enabled in the Web.config file.

* OutputCacheAttribute. Provides output caching.

* RequireHttpsAttribute. Forces unsecured HTTP requests to be resent over HTTPS.


##How To Create a Filter


You can create a filter in the following ways:

* Override one or more of the controller's On<Filter> methods.

* Create an attribute class that derives from ActionFilterAttribute and apply the attribute to a controller or an action method.

* Register a filter with the filter provider (the FilterProviders class).

* Register a global filter using the GlobalFilterCollection class.

Filtering in ASP.NET MVC






In ASP.NET MVC, controllers define action methods that usually have a one-to-one relationship with possible user interactions, such as clicking a link or submitting a form. For example, when the user clicks a link, a request is routed to the designated controller, and the corresponding action method is called.

Sometimes you want to perform logic either before an action method is called or after an action method runs. To support this, ASP.NET MVC provides filters. Filters are custom classes that provide both a declarative and programmatic means to add pre-action and post-action behavior to controller action methods.

A Visual Studio project with source code is available to accompany this topic: Download.



ASP.NET MVC Filter Types




ASP.NET MVC supports the following types of action filters:

•Authorization filters. These implement IAuthorizationFilter and make security decisions about whether to execute an action method, such as performing authentication or validating properties of the request. The AuthorizeAttribute class and the RequireHttpsAttribute class are examples of an authorization filter. Authorization filters run before any other filter.


•Action filters. These implement IActionFilter and wrap the action method execution. The IActionFilter interface declares two methods: OnActionExecuting and OnActionExecuted. OnActionExecuting runs before the action method. OnActionExecuted runs after the action method and can perform additional processing, such as providing extra data to the action method, inspecting the return value, or canceling execution of the action method.


•Result filters. These implement IResultFilter and wrap execution of the ActionResult object. IResultFilter declares two methods: OnResultExecuting and OnResultExecuted. OnResultExecuting runs before the ActionResult object is executed. OnResultExecuted runs after the result and can perform additional processing of the result, such as modifying the HTTP response. The OutputCacheAttribute class is one example of a result filter.


•Exception filters. These implement IExceptionFilter and execute if there is an unhandled exception thrown during the execution of the ASP.NET MVC pipeline. Exception filters can be used for tasks such as logging or displaying an error page. The HandleErrorAttribute class is one example of an exception filter.


The Controller class implements each of the filter interfaces. You can implement any of the filters for a specific controller by overriding the controller's On<Filter> method. For example, you can override the OnAuthorization method. The simple controller included in the downloadable sample overrides each of the filters and writes out diagnostic information when each filter runs. You can implement the following On<Filter> methods in a controller:

•OnAuthorization


•OnException


•OnActionExecuting


•OnActionExecuted


•OnResultExecuting


•OnResultExecuted




Filters Provided in ASP.NET MVC




ASP.NET MVC includes the following filters, which are implemented as attributes. The filters can be applied at the action method, controller, or application level.

•AuthorizeAttribute. Restricts access by authentication and optionally authorization. 


•HandleErrorAttribute. Specifies how to handle an exception that is thrown by an action method. 




NoteNote:


This filter does not catch exceptions unless the customErrors element is enabled in the Web.config file.
 


•OutputCacheAttribute. Provides output caching.


•RequireHttpsAttribute. Forces unsecured HTTP requests to be resent over HTTPS.




How To Create a Filter




You can create a filter in the following ways:

•Override one or more of the controller's On<Filter> methods.


•Create an attribute class that derives from ActionFilterAttribute and apply the attribute to a controller or an action method.


•Register a filter with the filter provider (the FilterProviders class).


•Register a global filter using the GlobalFilterCollection class.


A filter can implement the abstract ActionFilterAttribute class. Some filters, such as AuthorizeAttribute, implement the FilterAttribute class directly. Authorization filters are always called before the action method runs and called before all other filter types. Other action filters, such as OutputCacheAttribute, implement the abstract ActionFilterAttribute class, which enables the action filter to run either before or after the action method runs.

You can use the filter attribute declaratively with action methods or controllers. If the attribute marks a controller, the action filter applies to all action methods in that controller.



##Filter Providers

Multiple filter providers can be registered. Filter providers are registered using the static Providers property. 
The GetFilters(ControllerContext, ActionDescriptor) method aggregates the filters from all of the providers into a single list. 
Providers can be registered in any order; the order they are registered has no impact on the order in which the filter run.



##Filter Order

Filters run in the following order:

1. Authorization filters

2. Action filters

3. Response filters

4. Exception filters




