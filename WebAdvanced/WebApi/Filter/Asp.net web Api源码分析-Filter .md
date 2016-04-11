[Asp.net web Api源码分析-Filter ](http://www.cnblogs.com/majiang/archive/2012/12/05/2802782.html)

``` C#
public abstract class HttpActionDescriptor
    {
        protected HttpActionDescriptor()
        {
            _filterPipeline = new Lazy<Collection<FilterInfo>>(InitializeFilterPipeline);
        }
    public virtual Collection<FilterInfo> GetFilterPipeline()
        {
            return _filterPipeline.Value;
        }
        private Collection<FilterInfo> InitializeFilterPipeline()
        {
            IEnumerable<IFilterProvider> filterProviders = _configuration.Services.GetFilterProviders();

            IEnumerable<FilterInfo> filters = filterProviders.SelectMany(fp => fp.GetFilters(_configuration, this)).OrderBy(f => f, FilterInfoComparer.Instance);

            // Need to discard duplicate filters from the end, so that most specific ones get kept (Action scope) and
            // less specific ones get removed (Global)
            filters = RemoveDuplicates(filters.Reverse()).Reverse();

            return new Collection<FilterInfo>(filters.ToList());
        }
    }
```

这里主要是获取Controller和Action的Filter然后把他们合并成一个filter集合，Controller和Action的 filter的获取方式都一样，主要是获取他们的IFilter特性，然后通过该特性实例创建一个新的FilterInfo实例。这里的 FilterScope仍然是Global>Controller>Action,默认直接实现或者继承IFilter的有 IActionFilter、IAuthorizationFilter、IExceptionFilter\、FilterAttribute，实际开 发中主要的FilterAttribute有以下几个，

``` C#
public abstract class ActionFilterAttribute : FilterAttribute, IActionFilter
public abstract class AuthorizationFilterAttribute : FilterAttribute, IAuthorizationFilter
public class AuthorizeAttribute : AuthorizationFilterAttribute
public abstract class ExceptionFilterAttribute : FilterAttribute, IExceptionFilter
public abstract class ExceptionFilterAttribute : FilterAttribute, IExceptionFilter
```
