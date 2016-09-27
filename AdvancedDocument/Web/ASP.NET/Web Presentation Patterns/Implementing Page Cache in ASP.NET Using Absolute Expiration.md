[Implementing Page Cache in ASP.NET Using Absolute Expiration](https://msdn.microsoft.com/en-us/library/ff649217.aspx)


Context 
You are building a Web application in ASP.NET and you want to cache pages to improve performance. You have evaluated the alternatives presented in Page Cache and have determined that absolute expiration is an adequate strategy.
Implementation Strategy 
Page caching increases request response throughput by caching the content generated from dynamic pages. Page caching is enabled by default in ASP.NET, but output from any given response is not cached unless a valid expiration policy is defined. To define the expiration policy, you can use either the low-level OutputCache API or the high-level @OutputCache directive. 
When page caching is enabled, the first GET request to the page creates a page cache entry. The page cache entry serves subsequent GET or HEAD requests until the cached response expires. 
The page cache respects the expiration policy for pages. If a page is cached with an expiration policy of 60 seconds, the page is removed from the output cache when 60 seconds have elapsed. If the cache receives another request after that time, it executes the page code and refreshes the cache. This type of expiration policy is called absolute expiration, which means that a page is valid until a certain time. 
The following example demonstrates a way to use the @OutputCache directive to cache responses:
 

<%@ OutputCache Duration="60" VaryByParam="none" %>

<html>
  <script language="C#" runat="server">
    void Page_Load(Object sender, EventArgs e) 
    {
        TimeMsg.Text = DateTime.Now.ToString("G");
    }
  </script>

  <body>
    <h3>Using the Output Cache</h3>

    <p>Last generated on: <asp:label id="TimeMsg" runat="server"/>
  </body>
</html> 
The example displays the time when the response was generated. To see output caching in action, invoke the page and note the time at which the response was generated. Then refresh the page and note that the time has not changed, indicating that the second response is being served from the cache. The following line activates page caching on the response:
 

<%@ OutputCache Duration="60" VaryByParam="none" %>
 
This directive simply indicates that the page should be cached for 60 seconds and that the page does not vary according to any GET or POST parameters. Requests received in the first 60 seconds are satisfied from the cache. After 60 seconds, the page is removed from the cache; the next request caches the page again. 
Testing Considerations 
The caching of pages makes testing more difficult. For example, if you change a page and then view it in the browser, you may not see the updated page because the browser displays the page from the cache, rather than a newly generated page. Ideally, you can turn off the caching of pages and run tests that do not require caching. After these tests run successfully, you can enable caching and then run the tests that require caching. 
Resulting Context 
Using absolute expiration to implement Page Cache in ASP.NET results in the following benefits and liabilities:
Benefits 

This is by far the simplest method of caching pages in ASP.NET. Absolute expiration may be sufficient in many cases and is clearly an excellent place to begin, provided that you analyze the usage patterns of the Web application to determine which pages you cache. Also consider the volatility of the dynamic content on the page. For example, a weather page may have an expiration policy of 60 minutes, because the weather does not change very quickly. However, a Web page that displays a stock quote may not be cacheable at all. To determine the correct expiration time, you must know the most frequently viewed pages and understand the volatility of the data the pages contain.
You can set different expiration policies for different pages. Doing so enables you to cache only frequently accessed pages and not waste cache space on pages that are accessed infrequently. It also allows you to refresh pages containing volatile data more often than others.
Liabilities 

Dynamic content on cached pages may become invalid. This is because the page expiration is based on time rather than content. In the example described previously, the time is displayed on the page in seconds. Because the page is built every 60 seconds, the seconds field is invalid immediately after the page is built. The ramifications of the invalid data in this example are small. If you are displaying a time-sensitive financial quote, for example, and extreme accuracy is required, consider a caching strategy that ensures that you never display invalid data. (See Page Data Caching.)
This strategy does not accommodate passing parameters to the page. Dynamic pages are often parameterized. For example, a weather page may be parameterized by postal code. Unless you want to create separate pages and URLs for thousands of postal codes (42,000 in the United States, for example), you cannot use absolute expiration to cache this page. Vary-By-ParameterCaching resolves this issue.
Absolute expiration works well only if the whole page stays the same. In many applications, large portions of a page change rarely (great candidates for caching), but are coupled with other sections that change frequently (cannot be cached). Because absolute expiration caches only whole pages, it cannot take advantage of localized changes such as this. Page Fragment Caching may be a better choice in these circumstances, because it can cache portions of a page. HTML frames provide another option to simulate fragments of pages. However, frames have known issues in Web browsers, such as navigation and printing problems. 
There is no way to flush the cached pages. The pages remain in the cache until they expire or the server is restarted. This makes testing problematic. It can also be difficult in situations where data changes rarely, but if a change occurs you cannot afford a delay. For example, updating the weather forecast every two hours is probably sufficient in most cases. However, if a hurricane is approaching, you may not want to wait two hours before updating the weather forecast.
You must alter the code in each page to change the expiration policy. Because the expiration policy can only be changed in the code, there is no mechanism to turn off caching for the entire application. 
Storing pages in a cache requires disk space on the server. In the example described earlier, the small page would not require much disk space. As the content on each page and the number of pages in the cache increase, however, the demands for disk space on the Web server will also increase. 
Variants 
The following patterns explain alternate implementations of Page Cache:
Vary-By-Parameter Caching
Sliding Expiration Caching
Related Patterns 
For related page cache designs and implementation strategies, see the following patterns:
Page Data Caching
Page Fragment Caching