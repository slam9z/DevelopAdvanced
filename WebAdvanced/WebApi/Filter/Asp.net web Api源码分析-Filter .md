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