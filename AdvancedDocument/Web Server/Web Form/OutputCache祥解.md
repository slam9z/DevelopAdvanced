[OutputCache祥解 ](http://blog.csdn.net/rewoshengqi/article/details/5748405)

[@ OutputCache](https://msdn.microsoft.com/en-us/library/hdxfb6cy(v=vs.100).aspx)


用户访问页面时，整个页面将会被服务器保存在内存中，这样就对页面进行了缓存。当用户再次访问该页，页面不会再次执行数据操作，
页面首先会检查服务器中是否存在缓存，如果缓存存在，则直接从缓存中获取页面信息，如果页面不存在，则创建缓存。

页面输出缓存适用于那些数据量较多，而不会进行过多的事件操作的页面，如果一个页面需要执行大量的事件更新，以及数据更新，则并
不能使用页面输出缓存。使用@OutputCatch指令能够声明页面输出缓存，示例代码如下所示。

```aspx
<%@ OutputCache Duration="120" VaryByParam="none" %>
```
上述代码使用@OutputCatch指令声明了页面缓存，该页面将被缓存120秒。@OutputCatch指令包括10个属性，通过这些属性能够分别为
页面的不同情况进行缓存设置，常用的属性如下所示：

* CacheProfile：获取或设置OutputCacheProfile名称。
* Duration：获取或设置缓存项需要保留在缓存中的时间。
* VaryByHeader：获取或设置用于改变缓存项的一组都好分隔的HTTP标头名称。
* Location：获取或设置一个值，该值确定缓存项的位置，包括Any、Clint、Downstream、None、Server和ServerAndClient。默认值为Any。
* VaryByControl：获取或设置一簇分好分隔的控件标识符，这些标识符包含在当前页或用户控件内，用于改变当前的缓存项。
* NoStore：获取或设置一个值，该值确定是否设置了“Http Cache-Control：no-store”指令。
* VaryByCustom：获取输出缓存用来改变缓存项的自定义字符串列表。
* Enabled：获取或设置一个值，该值指示是否对当前内容启用了输出缓存。
* VaryByParam：获取查询字符串或窗体POST参数的列表。

通过设置相应的属性，可以为页面设置相应的缓存，当需要为Default.aspx设置缓存项时，可以使用VaryByParam属性进行设置，示例代码如下所示。

```aspx
<%@ OutputCache Duration="120" VaryByParam="none" %>
```

上述代码使用了Duration属性和VarByParam属性设置了当前页的缓存属性。为一个页面进行整体的缓存设置往往是没有必要的，
常常还会造成困扰，例如Default.aspx?id=1和Default.aspx?id=100在缓存时可能呈现的页面是相同的，这往往不是开发人员所希望的。
通过配置VarByParam属性能够指定缓存参数，示例代码如下所示。

```aspx
<%@ OutputCache Duration="120" VaryByParam="id" %>
```

上述代码则通过参数id进行缓存，当id项不同时，ASP.NET所进行的页面缓存也不尽相同。这样保证了Default.aspx?id=1和
Default.aspx?id=100在缓存时所显示的页面并不一致。VarByHeader和VarByCustom主要用于根据访问页面的客户端对页面的外观
或内容进行自定义。在ASP.NET中，一个页面可能需要为PC用户和MOBILE用户呈现输出，因此可以通过客户端的版本不同来缓存不
同的数据，示例代码如下所示。

```aspx
<%@ OutputCache Duration="120" VaryByParam="none" VaryByCustom="browser" %>
```

上述代码则为每个浏览器单独设置了缓存条目。