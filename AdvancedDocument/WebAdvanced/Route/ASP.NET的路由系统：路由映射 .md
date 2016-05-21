[ASP.NET的路由系统：路由映射](http://www.cnblogs.com/artech/archive/2012/03/20/aspnet-routing-02.html)

总的来说，我们可以通过RouteTable的静态属性Routes得到一个基于应用的全局路由表，通过上面的介绍我们知道这是一个类型的RouteCollection的集合对象，
我们可以通过调用它的MapPageRoute进行路由映射，即注册URL模板与某个物理文件的匹配关系。路由注册的核心就是在全局路由表中添加一个Route对象，
该对象的绝大部分属性都可以通过MapPageRoute方法的相关参数来指定。


##一、变量默认值

##二、约束

##三、对现有文件的路由

##四、注册路由忽略地址

``` C#
RouteTable.Routes.RouteExistingFiles = true;
RouteTable.Routes.Ignore("{filename}.js/{*pathInfo}");
```



##五、直接添加路由对象

``` C#

var defaults = new RouteValueDictionary { { "areacode", "010" }, { "days", 2 }};
    var constaints = new RouteValueDictionary { { "areacode", @"0\d{2,3}" }, { "days", @"[1-3]{1}" } };
    var dataTokens = new RouteValueDictionary { { "defaultCity", "BeiJing" }, { "defaultDays", 2 } };

    //路由注册方式1
RouteTable.Routes.MapPageRoute("default", "{areacode}/{days}", "~/weather.aspx", false, defaults, constaints, dataTokens);

//路由注册方式2
Route route = new Route("{areacode}/{days}", defaults, constaints, dataTokens, new PageRouteHandler("~/weather.aspx", false));
RouteTable.Routes.Add("default", route);


```



