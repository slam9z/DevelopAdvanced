[Creating Custom Action Filters](https://msdn.microsoft.com/en-us/library/dd381609(v=vs.98).aspx)

##Implementing a Custom Action Filter

``` C#
public class LoggingFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        filterContext.HttpContext.Trace.Write("(Logging Filter)Action Executing: " +
            filterContext.ActionDescriptor.ActionName);

        base.OnActionExecuting(filterContext);
    }

    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        if (filterContext.Exception != null)
            filterContext.HttpContext.Trace.Write("(Logging Filter)Exception thrown");

        base.OnActionExecuted(filterContext);
    }
}

```

##Action Filter Context


##Marking an Action Method with a Filter Attribute

##Executing Code Before and After an Action from Within a Controller


##Order of Execution for Action Filters

Each action filter has an Order property, which is used to determine the order that filters are executed in the scope of the filter. 
The Order property takes an integer value that must be 0 (default) or greater (with one exception). 

